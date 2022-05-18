using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;


[System.Serializable]
public class RadarObject
{
    public int radarItemId;                                 //Unique radar item Id(each radar entity must have a unique Id)
    public Transform RadarItemTransform { get; set; }       //Root transform of radar item(the icon on the radar)
    public RadarItem RadarItemComponent { get; set; }       //Radar item component(on the actual player/objective Icon prefab)
    public GameObject OwnerGameobject { get; set; }         //Root gameobject of the trackable radar entity(Actual player or objective)
    public bool IsObjective { get; set; }                   //Check if its assigned as player or objective
    public bool IsHighlighted { get; set; }                 //Check if the radar item is currently being highlighted
    public bool isEnemy { get; set; }                       //Check if the radar item is enemy player or friendly
    public IEnumerator EnemySpottedCoroutine { get; set; }  //Save enemy spotting coroutine(every player has separate coroutine)
}

public class AdvancedFPSRadar : MonoBehaviour
{
    //General variables
    [Header("General Settings")]

    public Camera localPlayerCamera;                    //LocalPlayer camera(Assign in editor or using RegisterLocalPlayer() function in your project)
    public float mapZoom = 3.0f;                        //Zoom of the radar(adjust accordindly if used with custom map image)
    public bool IconsStickToRadarBorder = true;         //Icons stick to radar border if they are out of range
    public bool ShowLocalFieldOfView = true;            //Display dynamic field of view(based on localplayer camera fov)

    public enum MapMode
    {
        Rotational,                                     // == 0 rotates the map around localplayer icon
        Static                                          // == 1 rotates localplayer icon in the center, map is static
    }
    public MapMode mapMode;

    public enum RadarShape
    {
        Circle,                                         // == 0 round/circular radar shape
        Square                                          // == 1 square radar shape
    }
    public RadarShape radarShape;

    [Header("Icon Size Settings")]
    public float PlayerIconSize = 15f;                  //Size(px) of player icons on the radar
    public float ObjectiveIconSize = 20f;               //Size(px) of objective icons on the radar

    [Header("Icon Fade Settings")]
    //Deathicon fade controls
    public float deathIcon_fade_delay = 2f;            //How many seconds does the deathicon stay active before starting to fade out?
    public float deathIcon_fade_out_time = 2f;         //How many seconds does the deathicon fade out?

    //Enemy spotting fade controls
    public float enemySpotted_fade_delay = 2f;         //How many seconds does the enemy spot stay active before starting to fade out?
    public float enemySpotted_fade_out_time = 2f;      //How many seconds does the enemy spot fade out(from fully visible to completely invisible)?

    [Header("Prefabs")]
    //Radar Icon Prefabs
    public GameObject GO_PlayerIconPrefab;              //Player Icon PREFAB on Radar
    public GameObject GO_ObjectiveIconPrefab;           //Objective Icon PREFAB on Radar
    public GameObject GO_AreaScanIconPrefab;            //Area scan Icon PREFAB on Radar

    [Header("Transforms")]
    //Parent transforms on the radar
    public Transform radar_2Dcardinal_direction;        //North, East, South, West text on the radar
    public Transform custom_map_parent;                 //Parent object of the custom map image(so we can rotate it)
    public RectTransform custom_map_rectransform;
    public Transform centerOfMapTransform;              //This transform represents the center of the map, so we can calculate custom map image position relative to local player
    public Transform radar2DIconsParent;                //Holds all Radar Icons as a child
    public RectTransform radarRectTransform;            //For easy access of radar width/height
    public RectTransform radar_localPlayerIcon;         //Local player icon in the center of the radar
    public RectTransform radar_localPlayerFieldOfView;  //Field of view for localPlayer camera
    public RectTransform radar_directionalLineIndicator;//A directional line from localPlayer(center of radar) to objective
    public RectTransform radar_UAVScanOverlay;          //UAV scan overlay image


    [Header("Sprites/Icons")]
    //2D Sprites/icons
    public Sprite radarIconEnemy;                       //Different icon for enemy 
    public Sprite radarIconFriendly;                    //Different icon for friendly 
    public Sprite radarIconDeadPlayer;                  //Different icon for dead players 
    public Sprite[] radarIconObjectives;                //Different icons for objectives 
    public Sprite radarCustomMapImage;                  //Custom map background for the radar

    [Header("Icon Colors")]
    //Colors for icons
    public Color enemyIconColor;                        //Different icon color for enemies 
    public Color friendlyIconColor;                     //Different icon color for friendly 
    public Color objectiveIconColor;                    //Different icon color for objectives 
    public Color objectiveIconHighlightColor;           //Different icon color for objectives highlights(circle around the main objective icon) 
    public Color playerIconHighlightColor;              //Different icon color for player highlights(circle around the main player icon) 

    [Header("Private")]
    //Private variables
    private Transform localPlayerTransform;             //LocalPlayer transform(Assigned automatically in the RegisterLocalPlayer function)
    private int directionalLineObjectiveId;             //This value gets updated to hold Objective Id, which we use to render the direction line on the radar
    private float originalMapScale;                     //Holds the original scale/zoom of the radar(we use this for toggling zoom on key press)
    private bool zoomIn = false;                        //Toggle map zoom functionality 
    private float radar_radius = 0;                     //Radius of the minimap/radar (used for clamping radar icons to the border)
    private int areaScanObjectiveId = 10011;            //Save Unique ID for Area Scan Objective only(Because it works differently from other objectives)
    private IEnumerator currentAreaScanCoroutine;       //Save Area scan coroutine to check if its running or not
    private IEnumerator currentUAVScanCoroutine;        //Save UAV scan coroutine to check if its running or not
                                                        //Radar objects list

    //List of All Radar items(Players & Objectives)
    private List<RadarObject> radar2DObjects = new List<RadarObject>(); 

    private void Start()
    {
        //Set the original map scale to assigned value in the inspector
        originalMapScale = mapZoom;

        //Calculate the map radius from the width of the radar
        radar_radius = radarRectTransform.sizeDelta.x / 2f;

        //Disable directional objective indicator lines
        SetDirectionalIndicatorAtObjective(false, -1);

        //Enable/disable fiew of view marker based on editor settings
        radar_localPlayerFieldOfView.gameObject.SetActive(ShowLocalFieldOfView);

        if(localPlayerCamera != null)
            RegisterLocalPlayer(localPlayerCamera);

        //Check if custom map image has been assigned 
        if (radarCustomMapImage != null)
        {
            Image customMapImg = custom_map_rectransform.GetComponent<Image>();

            customMapImg.enabled = true;

            //Set the custom map image on the radar
            customMapImg.sprite = radarCustomMapImage;
        }
            

        //Check for required references and log potential errors
        //CheckForReferences();
    }

    void Update()
    {
        //If keyboard key down
        if (Input.GetKeyDown(KeyCode.N))
        {
            ToggleMapZoom();//Toggle map zoom
        }

        if (ShowLocalFieldOfView == true)
        {
            //Draw dynamic field of view marker in the center of radar
            DrawLocalPlayerDynamicFieldOfView(localPlayerCamera);
        }

        //Draw all radar icons on the minimap/radar
        DrawRadarEntities();
    }

    //REGISTER LOCAL PLAYER--------------------------------------
    /// <summary>
    /// Assign local player camera.
    /// </summary>Vector2.Distance
    /// <param name="_localPlayer"></param>
    public void RegisterLocalPlayer(Camera _localPlayerCamera)
    {
        //Assign local player transform
        localPlayerTransform = _localPlayerCamera.transform;

        //Assign local player camera reference
        localPlayerCamera = _localPlayerCamera;
    }

    //ZOOM------------------------------------------------------
    /// <summary>
    /// Toggle map zoom 
    /// </summary>
    public void ToggleMapZoom()
    {
        //Flip the zoom switch
        zoomIn = !zoomIn;

        //If zoom is active
        if (zoomIn)
        {
            //Set the map scale smaller
            SetMapZoom(originalMapScale / 2f);
        }
        else
        {
            //if zoom out, reset the map scale to the original assigned value
            SetMapZoom(originalMapScale);
        }
    }

    /// <summary>
    /// Update/set radar zoom
    /// </summary>
    /// <param name="_newZoom"></param>
    public void SetMapZoom(float _newZoom)
    {
        mapZoom = _newZoom;
    }

    //PLAYERS---------------------------------------------------
    /// <summary>
    /// Add new player object and assign player Id
    /// </summary>
    /// <param name="_playerGameObject"></param>
    /// <param name="_playerId"></param>
    public void RegisterRadarPlayerObject(GameObject _playerGameObject, bool _isEnemy, int _playerId)
    {
        //Instantiate new radar icon prefab
        GameObject radarItemPrefab = Instantiate(GO_PlayerIconPrefab);

        //Get the RadarItem Component on the prefab
        RadarItem radarItemComponent = radarItemPrefab.GetComponent<RadarItem>();

        //Set radar player icon size
        radarItemComponent.rct.sizeDelta = new Vector2(PlayerIconSize, PlayerIconSize);

        //Set the objective icon highlight(circle around main icon) color
        radarItemComponent.highlightBorder.color = playerIconHighlightColor;

        //Check if deathIcon has been assigned to the radarItem
        if (radarItemComponent.deadIcon != null)
            radarItemComponent.deadIcon.enabled = false; //Disable death icon

        //Check if default icon has been assigned to the radarItem
        if (radarItemComponent.defaultIcon != null)
            radarItemComponent.defaultIcon.enabled = true;//Enable default radar icon

        //Add player to radarObjects list
        radar2DObjects.Add(new RadarObject() { radarItemId = _playerId, OwnerGameobject = _playerGameObject, RadarItemTransform = radarItemPrefab.transform, RadarItemComponent = radarItemComponent, IsObjective = false, isEnemy = _isEnemy });

        //Make radarItem prefab parent of the transform 
        radarItemPrefab.transform.SetParent(radar2DIconsParent, false);

        //Set the correct radar icon for friendly/enemy team player
        UpdateRadarPlayerIconByTeam(_playerId, _isEnemy);

        //Update player icon color based on the team(friendly/enemy)
        if (_isEnemy)
        {
            //Set player icon color to enemy colors
            UpdateRadarPlayerIconColor(_playerId, new Color(enemyIconColor.r, enemyIconColor.g, enemyIconColor.b, 0));
        }
        else
        {
            //Set player icon color to friendly colors
            UpdateRadarPlayerIconColor(_playerId, friendlyIconColor);
        }
    }

    /// <summary>
    /// Update player icon on the radar( useful if existing player switches teams in multiplayer match and you need to swap out player icon(friendly, enemy)
    /// </summary>
    /// <param name="_playerId"></param>
    /// <param name="_isEnemy"></param>
    public void UpdateRadarPlayerIconByTeam(int _playerId, bool _isEnemy)
    {
        //Check if radar objects list contains any entities
        if (radar2DObjects.Count == 0)
            return;

        //Loop thrue the list of all radar objects(players and objectives)
        for (int i = 0; i < radar2DObjects.Count; i++)
        {
            //Find the player by Id and make sure its not the objective
            if (radar2DObjects[i].radarItemId == _playerId && radar2DObjects[i].IsObjective == false)
            {
                if (_isEnemy)
                {
                    //Set the default icon color for enemy players
                    radar2DObjects[i].RadarItemComponent.defaultIcon.sprite = radarIconEnemy;
                }
                else
                {
                    //Set the default icon color for enemy players
                    radar2DObjects[i].RadarItemComponent.defaultIcon.sprite = radarIconFriendly;
                }

                //Stop the loop, since we found the  player Id
                break;
            }
        }
    }

    /// <summary>
    /// Update players icon size
    /// </summary>
    /// <param name="_sizeInPixels"></param>
    public void UpdatePlayerIconSize(float _sizeInPixels)
    {
        //Check if radar objects list contains any entities
        if (radar2DObjects.Count == 0)
            return;

        //Loop thrue all the radar items
        for (int i = 0; i < radar2DObjects.Count; i++)
        {
            //Make sure its players only
            if (radar2DObjects[i].IsObjective == false)
            {
                //Set radar player icon size
                radar2DObjects[i].RadarItemComponent.rct.sizeDelta = new Vector2(_sizeInPixels, _sizeInPixels);
            }
        }
    }

    /// <summary>
    /// Update player icon color on the radar
    /// </summary>
    /// <param name="_playerId"></param>
    /// <param name="_iconColor"></param>
    public void UpdateRadarPlayerIconColor(int _playerId, Color _iconColor)
    {
        //Check if radar objects list contains any entities
        if (radar2DObjects.Count == 0)
            return;

        //Loop thrue the list of all radar objects(players and objectives)
        for (int i = 0; i < radar2DObjects.Count; i++)
        {
            //Find the player by Id and make sure its not the objective
            if (radar2DObjects[i].radarItemId == _playerId && radar2DObjects[i].IsObjective == false)
            {
                //Set the player icon color
                radar2DObjects[i].RadarItemComponent.defaultIcon.color = _iconColor;

                break;
            }
        }
    }
   
    /// <summary>
    /// Highlight the player icon on the radar with circular border
    /// </summary>
    /// <param name="_playerId"></param>
    /// <param name="highLightActive"></param>
    public void HighLightPlayer(int _playerId, bool highLightActive)
    {
        //Check if radar objects list contains any entities
        if (radar2DObjects.Count == 0)
            return;

        //Loop thrue all the radar objects in the list
        for (int i = 0; i < radar2DObjects.Count; i++)
        {
            //Find player by player ID
            if (radar2DObjects[i].IsObjective == false && radar2DObjects[i].radarItemId == _playerId)
            {
                //Mark this radar item as highlighted
                radar2DObjects[i].IsHighlighted = highLightActive;

                //Set radar border gameObject activ for highlight
                radar2DObjects[i].RadarItemComponent.highlightBorder.gameObject.SetActive(highLightActive);

                //Check if the main icon is visible
                if (radar2DObjects[i].RadarItemComponent.defaultIcon.color.a > 0.1f)
                {
                    //Set the highlight border color(fully visible)
                    radar2DObjects[i].RadarItemComponent.highlightBorder.color = playerIconHighlightColor;
                }
                else//If main icon is not visible, dont show the highlight border
                {
                    //Set the highlight border color alpha value to 0(invisible)
                    radar2DObjects[i].RadarItemComponent.highlightBorder.color = Color.clear;
                }
                //Found the player Id, exit loop
                return;
            }
        }
    }

    /// <summary>
    /// Update player isAlive state on the radar
    /// </summary>
    /// <param name="_playerId"></param>
    /// <param name="_isAlive"></param>
    /// <param name="_deathIconFade"></param>
    public void SetPlayerAliveOnRadar(int _playerId, bool _isAlive, bool _deathIconFade)
    {
        //Check if radar objects list contains any entities
        if (radar2DObjects.Count == 0)
            return;

        //Loop thrue all the radar objects
        for (int i = 0; i < radar2DObjects.Count; i++)
        {
            //Find player by id and check if its actually a player object(isObjective = false)
            if (radar2DObjects[i].radarItemId == _playerId && radar2DObjects[i].IsObjective == false)
            {
                //Disable spotted enemy player on the radar(if its actually being spotted)
                DisableSpottedEnemyOnRadar(radar2DObjects[i]);

                //For Alive players, disable the death icon and activate default player icon
                if (_isAlive == true)
                {
                    radar2DObjects[i].RadarItemComponent.deadIcon.enabled = false;
                    radar2DObjects[i].RadarItemComponent.defaultIcon.enabled = true;
                }
                else //For Dead players, enable death icon and activate default player icon
                {
                    //Death icon fade is active(meaning we slowly fade out icon, rather than instantly disabling it)
                    if (_deathIconFade)
                    {
                        //Enable death icon and assign the correct sprite, disable the default player icon
                        radar2DObjects[i].RadarItemComponent.deadIcon.sprite = radarIconDeadPlayer;
                        radar2DObjects[i].RadarItemComponent.deadIcon.enabled = true;
                        radar2DObjects[i].RadarItemComponent.defaultIcon.enabled = false;

                        //Check if its enemy or friendly player
                        if (radar2DObjects[i].isEnemy)
                        {
                            //Enemy player death icon fade starts from the color of the enemy players default icon
                            StartCoroutine(FadeOutPlayerDeathIcon(radar2DObjects[i].RadarItemComponent.deadIcon, enemyIconColor, deathIcon_fade_delay, deathIcon_fade_out_time));
                        }
                        else
                        {
                            //Friendly player death icon fade starts from the color of the friendly players default icon
                            StartCoroutine(FadeOutPlayerDeathIcon(radar2DObjects[i].RadarItemComponent.deadIcon, friendlyIconColor, deathIcon_fade_delay, deathIcon_fade_out_time));
                        }
                    }
                    else//Instantly disable death icon(without fading effect)
                    {
                        radar2DObjects[i].RadarItemComponent.deadIcon.enabled = false;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Spot an enemy on the radar based on playerId
    /// </summary>
    /// <param name="_spottedPlayerId"></param>
    public void SpotEnemyOnRadar(int _spottedPlayerId)
    {
        //Check if radar objects list contains any entities
        if (radar2DObjects.Count == 0)
            return;

        //Loop thrue all the radar objects in the list
        for (int i = 0; i < radar2DObjects.Count; i++)
        {
            //Find spotted player by player ID
            if (radar2DObjects[i].radarItemId == _spottedPlayerId && radar2DObjects[i].isEnemy)
            {
                //Check if enemy spotted coroutine is still active
                if (radar2DObjects[i].EnemySpottedCoroutine != null)
                {
                    //Stop the active coroutine
                    StopCoroutine(radar2DObjects[i].EnemySpottedCoroutine);
                }

                //Assign new enemy spotted coroutine to the player object
                radar2DObjects[i].EnemySpottedCoroutine = EnemySpottedIconFadeOut(radar2DObjects[i], enemySpotted_fade_delay, enemySpotted_fade_out_time);

                //Start the enemy spotting coroutine
                StartCoroutine(radar2DObjects[i].EnemySpottedCoroutine);
            }
        }
    }

    /// <summary>
    /// Remove player icon from the radar 
    /// </summary>
    /// <param name="_playerId"></param>
    public void RemoveRadarIconByPlayerId(int _playerId)
    {
        //Check if radar objects list contains any entities
        if (radar2DObjects.Count == 0)
            return;

        //Make a new list of radar objects
        List<RadarObject> newList = new List<RadarObject>();

        //Loop thrue all the current radar objects
        for (int i = 0; i < radar2DObjects.Count; i++)
        {
            //Find player by id and check if its actually a player object(isObjective = false)
            if (radar2DObjects[i].radarItemId == _playerId && radar2DObjects[i].IsObjective == false)
            {
                //Disable the player on radar(if it is being spotted)
                DisableSpottedEnemyOnRadar(radar2DObjects[i]);
                //Destroy the player radar component
                Destroy(radar2DObjects[i].RadarItemComponent.gameObject);
                //Destroy the player radar item
                Destroy(radar2DObjects[i].RadarItemTransform.gameObject);
            }
            else
            {
                //Add all the other radar objects to the new list
                newList.Add(radar2DObjects[i]);
            }
        }

        //Add updated player list(with playerId removed) to the original list
        radar2DObjects = newList;
    }

    //OBJECTIVES---------------------------------------------------
    /// <summary>
    /// Add new radar objective and assign the objective id
    /// </summary>
    /// <param name="_objectiveGameObj"></param>
    /// <param name="objectiveName"></param>
    public void RegisterRadarObjective(GameObject _objectiveGameObj, Sprite _objectiveIcon, int _objectiveId)
    {
        //Instantiate a new radar item prefab
        GameObject radarItemPrefab = Instantiate(GO_ObjectiveIconPrefab);

        //Get the RadarItem component on the instantiated prefab
        RadarItem radarItemComponent = radarItemPrefab.GetComponent<RadarItem>();

        //Set the objective icon size
        radarItemComponent.rct.sizeDelta = new Vector2(ObjectiveIconSize, ObjectiveIconSize);

        //Set the objective icon highlight(circle around main icon) color
        radarItemComponent.highlightBorder.color = objectiveIconHighlightColor;

        //Add objective to radarObjectives list
        radar2DObjects.Add(new RadarObject() { radarItemId = _objectiveId, OwnerGameobject = _objectiveGameObj, RadarItemTransform = radarItemPrefab.transform, RadarItemComponent = radarItemComponent, IsObjective = true, isEnemy = false });

        //Parent the radarItemPrefab under the radar UI
        radarItemPrefab.transform.SetParent(radar2DIconsParent, false);

        //Set the radar objective icons to render before the player icons
        radarItemPrefab.transform.SetSiblingIndex(0);

        //Set the radar objective icon/sprite
        UpdateRadarObjectiveIcon(_objectiveId, _objectiveIcon);

        //Set radar objective icon/sprite color
        UpdateRadarObjectiveIconColor(_objectiveId, objectiveIconColor);
    }

    /// <summary>
    /// Update radar objective icon color (for example when objective state changes)
    /// </summary>
    /// <param name="_objectiveId"></param>
    /// <param name="_objectiveIconColor"></param>
    public void UpdateRadarObjectiveIconColor(int _objectiveId, Color _objectiveIconColor)
    {
        //Check if radar objects list contains any entities
        if (radar2DObjects.Count == 0)
            return;

        //Loop thrue all the radar items
        for (int i = 0; i < radar2DObjects.Count; i++)
        {
            //Find the objective Id
            if (radar2DObjects[i].radarItemId == _objectiveId && radar2DObjects[i].IsObjective == true)
            {
                //Change radar icon/sprite color
                radar2DObjects[i].RadarItemComponent.defaultIcon.color = _objectiveIconColor;

                //Stop the loop
                break;
            }
        }
    }

    /// <summary>
    ///  Highlight the objective icon on the radar with circular border
    /// </summary>
    /// <param name="_objectiveId"></param>
    /// <param name="highLightActive"></param>
    public void HighLightObjective(int _objectiveId, bool highLightActive)
    {
        //Check if radar objects list contains any entities
        if (radar2DObjects.Count == 0)
            return;

        //Loop thrue all the radar objects in the list
        for (int i = 0; i < radar2DObjects.Count; i++)
        {
            //Find objective by objective ID
            if (radar2DObjects[i].IsObjective == true && radar2DObjects[i].radarItemId == _objectiveId)
            {
                //Mark this radar item as highlighted
                radar2DObjects[i].IsHighlighted = highLightActive;

                //Set radar border gameObject active
                radar2DObjects[i].RadarItemComponent.highlightBorder.gameObject.SetActive(highLightActive);
            }
        }
    }

    /// <summary>
    /// Update radar objective icon/sprite
    /// </summary>
    /// <param name="_objectiveId"></param>
    /// <param name="_objectiveIcon"></param>
    public void UpdateRadarObjectiveIcon(int _objectiveId, Sprite _objectiveIcon)
    {
        //Check if radar objects list contains any entities
        if (radar2DObjects.Count == 0)
            return;

        //Loop thrue all the radar items
        for (int i = 0; i < radar2DObjects.Count; i++)
        {
            //Find the objective Id
            if (radar2DObjects[i].radarItemId == _objectiveId && radar2DObjects[i].IsObjective == true)
            {
                //Change radar icon/sprite 
                radar2DObjects[i].RadarItemComponent.defaultIcon.sprite = _objectiveIcon;

                //Stop the loop
                break;
            }
        }
    }

    /// <summary>
    /// Update objectives icon size
    /// </summary>
    /// <param name="_sizeInPixels"></param>
    public void UpdateObjectiveIconSize(float _sizeInPixels)
    {
        //Check if radar objects list contains any entities
        if (radar2DObjects.Count == 0)
            return;

        //Loop thrue all the radar items
        for (int i = 0; i < radar2DObjects.Count; i++)
        {
            //Make sure its players only
            if (radar2DObjects[i].IsObjective == true)
            {
                //Set radar player icon size
                radar2DObjects[i].RadarItemComponent.rct.sizeDelta = new Vector2(_sizeInPixels, _sizeInPixels);
            }
        }
    }

    /// <summary>
    /// Activate or disable the directional line of the objective on the minimap
    /// </summary>
    /// <param name="_active"></param>
    /// <param name="_objectiveId"></param>
    public void SetDirectionalIndicatorAtObjective(bool _active, int _objectiveId)
    {
        if (_active)
        {
            radar_directionalLineIndicator.gameObject.SetActive(true);
            directionalLineObjectiveId = _objectiveId;
        }
        else
        {
            radar_directionalLineIndicator.gameObject.SetActive(false);
            directionalLineObjectiveId = -1;
        }
    }

    //ENEMY AREA/UAV SCAN----------------------------------------------
    /// <summary>
    /// Start UAV scan of the entire map for enemy players
    /// </summary>
    /// <param name="_scanLengthInSeconds"></param>
    public void StartUAVScan(float _scanLengthInSeconds)
    {
        //Check if area scan coroutine is still active
        if (currentUAVScanCoroutine != null)
        {
            return;
        }

        //Assign new enemy area scanning coroutine 
        currentUAVScanCoroutine = UAVScanCoroutine(_scanLengthInSeconds);

        //Start coroutine which visualize the scan area and detects enemy players on the radar
        StartCoroutine(currentUAVScanCoroutine);
    }

    /// <summary>
    /// Starts a circular area scan for enemies at a specified world position
    /// </summary>
    /// <param name="_AreaScannerGameObj"></param>
    /// <param name="_scanLength"></param>
    /// <param name="_scanCircleRadius"></param>
    public void StartEnemyAreaScanAtPosition(GameObject _AreaScannerGameObj, float _scanLength, float _scanCircleRadius)
    {
        //Check if radar item for area scans already exist in the list of radar objects
        RadarObject radObj = radar2DObjects.Find(x => x.radarItemId == areaScanObjectiveId);

        //If not NULL, it means area scan object already exist
        if (radObj != null)
        {
            //Destroy the old prefab of the radar area scanner
            Destroy(radObj.RadarItemComponent.gameObject);

            //Remove the previous scanner object from the list of radar items
            radar2DObjects.Remove(radObj);
        }

        //Instantiate a new area scan item prefab
        GameObject radarItemPrefab = Instantiate(GO_AreaScanIconPrefab);

        //Get the RadarItem component on the instantiated prefab
        RadarItem radarItemComponent = radarItemPrefab.GetComponent<RadarItem>();

        //Parent the radarItemPrefab under the radar UI
        radarItemPrefab.transform.SetParent(radar2DIconsParent, false);

        //Activate the area scan image on the radar 
        radarItemComponent.gameObject.SetActive(true);

        //Add Area Scan to radar objects list
        radar2DObjects.Add(new RadarObject() { radarItemId = areaScanObjectiveId, OwnerGameobject = _AreaScannerGameObj, RadarItemTransform = radarItemComponent.transform, RadarItemComponent = radarItemComponent, IsObjective = true, isEnemy = false });

        //Check if area scan coroutine is still active
        if (currentAreaScanCoroutine != null)
        {
            //Stop the active coroutine
            StopCoroutine(currentAreaScanCoroutine);
        }

        //Assign new enemy area scanning coroutine 
        currentAreaScanCoroutine = AreaScanCoroutine(_scanLength, _scanCircleRadius, radarItemComponent);

        //Start coroutine which visualize the scan area and detects enemy players on the radar
        StartCoroutine(currentAreaScanCoroutine);
    }

    //PRIVATE FUNCTIONS------------------------------------------------

    /// <summary>
    /// Start fading out player death icon
    /// </summary>
    /// <param name="_deathIcon"></param> 
    /// <param name="_waitToStartFading"></param>
    /// <param name="_fadeTime"></param>
    /// <returns></returns>
    private IEnumerator FadeOutPlayerDeathIcon(Image _deathIcon, Color _deathIconColor, float _waitToStartFading, float _fadeTime)
    {
        //Set death icon default color(set different colors based on team)
        _deathIcon.color = _deathIconColor;

        //Wait before starting to fade out death icon
        yield return new WaitForSeconds(_waitToStartFading);

        //set current fade time
        float ElapsedFadeTime = 0.0f;

        //Fade loop
        while (ElapsedFadeTime < _fadeTime && _deathIcon != null)
        {
            //Add seconds to elapsed fade time
            ElapsedFadeTime += Time.deltaTime;

            //Fade the deathicon(by lerping its Alpha value)
            _deathIcon.color = Color.Lerp(_deathIconColor, Color.clear, (ElapsedFadeTime / _fadeTime));

            yield return null;
        }

        //Check if deathIcon is assigned
        if (_deathIcon != null)
        {
            _deathIcon.enabled = false; //Disable deathIcon once the fade out loop has finished
        }
    }

    /// <summary>
    /// Disable spotted enemy on the radar
    /// </summary>
    /// <param name="_radarObject"></param>
    private void DisableSpottedEnemyOnRadar(RadarObject _radarObject)
    {
        //Check if enemy spotting coroutine is running
        if (_radarObject.EnemySpottedCoroutine != null)
        {
            //Stop the enemy spotting coroutine
            StopCoroutine(_radarObject.EnemySpottedCoroutine);

            //Reset the enemy icon color Alpha value to 0 (meaning the icon will be completely invisible)
            _radarObject.RadarItemComponent.defaultIcon.color = new Color(_radarObject.RadarItemComponent.defaultIcon.color.r, _radarObject.RadarItemComponent.defaultIcon.color.g, _radarObject.RadarItemComponent.defaultIcon.color.b, 0f);
        }
    }

    /// <summary>
    /// Fade out spotted enemy icons
    /// </summary>
    /// <param name="_radarObject"></param>
    /// <param name="_waitToStartFading"></param>
    /// <param name="_fadeOutTime"></param>
    /// <returns></returns>
    private IEnumerator EnemySpottedIconFadeOut(RadarObject _radarObject, float _waitToStartFading, float _fadeOutTime)
    {
        float timeElapsed = 0f;

        Color startColor = new Color(
            _radarObject.RadarItemComponent.defaultIcon.color.r,
            _radarObject.RadarItemComponent.defaultIcon.color.g,
            _radarObject.RadarItemComponent.defaultIcon.color.b,
            1f); //Start With Alpha 1 *Visible Icon*

        Color endColor = new Color(
            _radarObject.RadarItemComponent.defaultIcon.color.r,
            _radarObject.RadarItemComponent.defaultIcon.color.g,
            _radarObject.RadarItemComponent.defaultIcon.color.b,
            0f); //End With Alpha 0 *Disabled Icon*;

        //Make enemy icon is fully visible(alpha value 1)
        _radarObject.RadarItemComponent.defaultIcon.color = startColor;

        //Make enemy highlight icon fully visible
        _radarObject.RadarItemComponent.highlightBorder.color = playerIconHighlightColor;

        //Wait a sec before starting to fade out
        yield return new WaitForSeconds(_waitToStartFading);

        //Fading loop 
        while (timeElapsed < _fadeOutTime)
        {
            //Add seconds to the elapsed fade time
            timeElapsed += Time.deltaTime;

            //Check if radarItem has player icon assigned
            if (_radarObject.RadarItemComponent.defaultIcon)
            {
                //Lerp the player icon alpha value from 1(fully visible) to 0(invisible)
                _radarObject.RadarItemComponent.defaultIcon.color = Color.Lerp(startColor, endColor, timeElapsed / _fadeOutTime);

                //Lerp the highlight icon alpha value from 1(fully visible) to 0(invisible)
                _radarObject.RadarItemComponent.highlightBorder.color = Color.Lerp(playerIconHighlightColor, endColor, timeElapsed / _fadeOutTime);
            }

            yield return null;
        }

        //Set the coroutine on this radar item to zero
        _radarObject.EnemySpottedCoroutine = null;
    }

    /// <summary>
    /// Check if icon position on the radar is within a circle radius
    /// </summary>
    /// <param name="_center"></param>
    /// <param name="_point"></param>
    /// <param name="_radius"></param>
    /// <returns></returns>
    private bool PointInsideCircle(Vector2 _center, Vector2 _point, float _radius)
    {
        // Compare radius of circle with distance  
        // of its center from given point 
        float dist = Vector2.Distance(_point, _center);

        if (dist < _radius)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Scale local player field of view based on main camera
    /// </summary>
    private void DrawLocalPlayerDynamicFieldOfView(Camera _localPlayerCamera)
    {
        radar_localPlayerFieldOfView.localScale = new Vector3(_localPlayerCamera.fieldOfView * 0.02f, radar_localPlayerFieldOfView.localScale.y, radar_localPlayerFieldOfView.localScale.z);
    }

    /// <summary>
    /// Area scan of the radar to spot all enemies within the bounds of the circular area
    /// </summary>
    /// <param name="_scanLengthInSeconds"></param>
    /// <param name="_scanAreaSizeInWorldUnits"></param>
    /// <param name="_radarItem"></param>
    /// <returns></returns>
    private IEnumerator AreaScanCoroutine(float _scanLengthInSeconds, float _scanAreaSizeInWorldUnits, RadarItem _radarItem)
    {
        //set current fade time
        float ElapsedRadarScanTime = 0.0f;
        float _scanTime = _scanLengthInSeconds;

        //Radar loop
        while (ElapsedRadarScanTime < _scanTime)
        {
            //Add seconds to elapsed fade time
            ElapsedRadarScanTime += Time.deltaTime;

            //Calculate the speed of ping pong scaling effect based on the scan length and zoom level
            float scalingSpeed = Time.time * ((mapZoom) / _scanLengthInSeconds);

            //Size of the entire game world(in units)
            float mapSizeInUnits = 100.0f;                

            //Calculate area scan size/scale based on the size of the game world and zoom level
            float scale = Mathf.PingPong(Time.time * 0.3f, (_scanAreaSizeInWorldUnits / mapSizeInUnits) * (mapZoom/2)); 

            //Ping pong effect on scale of the area scan
            _radarItem.transform.localScale = new Vector3(scale, scale, scale);

            //Ping pong effect on the alpha value of the area scan
            _radarItem.defaultIcon.color = new Color(_radarItem.defaultIcon.color.r, _radarItem.defaultIcon.color.g, _radarItem.defaultIcon.color.b, scale);

            //Loop thrue all the radar objects
            foreach (RadarObject radObject in radar2DObjects)
            {
                //Check if enemy player
                if (radObject.IsObjective == false && radObject.isEnemy == true)
                {
                    //Check if player icon is inside the enemy scan area
                    if (PointInsideCircle(_radarItem.rct.anchoredPosition, radObject.RadarItemComponent.rct.anchoredPosition, radar_radius * _radarItem.transform.localScale.x))
                    {
                        //Spot the detected enemy in the scan area
                        SpotEnemyOnRadar(radObject.radarItemId);
                    }
                }
            }

            yield return null;
        }

        //Area scan finished, disable the scan area object on radar
        _radarItem.gameObject.SetActive(false);
    }

    /// <summary>
    /// UAV Scan of the entire map to spot all enemies on the radar
    /// </summary>
    /// <param name="_scanLengthInSeconds"></param>
    /// <returns></returns>
    private IEnumerator UAVScanCoroutine(float _scanLengthInSeconds)
    {
        //set elapsed UAV scan time
        float ElapsedRadarScanTime = 0.0f;

        //Enable the UAV object on the radar
        radar_UAVScanOverlay.gameObject.SetActive(true);

        //Set the correct width of the UAV Scan( same width as the entire Radar )
        radar_UAVScanOverlay.sizeDelta = new Vector2(radar_radius * 2f, radar_UAVScanOverlay.sizeDelta.y);

        //Save the start position of the UAV scan overlay
        float startingPositionY = radar_radius + (radar_UAVScanOverlay.sizeDelta.y / 2f);

        //Set the starting position
        radar_UAVScanOverlay.anchoredPosition = new Vector2(0, startingPositionY);

        //Set the end position of the UAV scan overlay
        float endPositionY = -radar_radius - (radar_UAVScanOverlay.sizeDelta.y / 2f);

        //Calculate the distance between the starting point and end position of the UAV scan overlay
        float distanceToMove = (radar_radius * 2f) + (radar_UAVScanOverlay.sizeDelta.y / 2f);

        Debug.Log(distanceToMove);

        //Calculate speed to move the UAV Scan over a distance 
        float speed = distanceToMove / _scanLengthInSeconds;

        Debug.Log(speed);

        //UAV Scan loop
        while (ElapsedRadarScanTime < _scanLengthInSeconds)
        {
            //Add seconds to elapsed fade time
            ElapsedRadarScanTime += Time.deltaTime;

            //Calculate the movement of UAV scan from top of the radar to the bottom(vertically down)
            float posY = Mathf.Lerp(startingPositionY, endPositionY, ElapsedRadarScanTime / _scanLengthInSeconds);

            //set the vertical position of the UAV scan
            radar_UAVScanOverlay.anchoredPosition = new Vector3(0, posY, 0);

            //Loop thrue all the radar objects
            foreach (RadarObject radObject in radar2DObjects)
            {
                //Check if enemy player and spotting coroutine is not already active/running
                if (radObject.EnemySpottedCoroutine == null && radObject.isEnemy == true)
                {
                    //Check if enemy player icon is inside the UAV scan rectangle
                    if (IsPointInsideRectangle(radObject.RadarItemComponent.rct.anchoredPosition, radar_UAVScanOverlay))
                    {
                        //Spot the detected enemy in the radar
                        SpotEnemyOnRadar(radObject.radarItemId);
                    }
                }
            }

            yield return null;
        }

        //Set the coroutine to null, so we can run it again
        currentUAVScanCoroutine = null;

        //Reset the original position of the scane area
        radar_UAVScanOverlay.anchoredPosition = new Vector3(0, startingPositionY, 0);

        //UAV scan finished, disable the scan area object on radar
        radar_UAVScanOverlay.gameObject.SetActive(false);
    }

    /// Clamps any angle to a value between 0 and 360 (to avoid gimbal-lock on euler angle rotations)
    /// We use this for rotating local player icon for Static map mode.
    /// </summary>
    private float ClampAngle(float _Angle)
    {
        float ReturnAngle = _Angle;

        if (_Angle < 0f)
            ReturnAngle = (_Angle + (360f * ((_Angle / 360f) + 1)));

        else if (_Angle > 360f)
            ReturnAngle = (_Angle - (360f * (_Angle / 360f)));

        else if (ReturnAngle == 360) //Never use 360, only go from 0 to 359
            ReturnAngle = 0;

        return ReturnAngle;
    }

    /// <summary>
    /// Check if  Vecto2 point is inside the bounds of the rectangle. 
    /// We use this function inside the UAV Scan to detect enemies
    /// </summary>
    /// <param name="_point"></param>
    /// <param name="_rectTransform"></param>
    /// <returns></returns>
    private bool IsPointInsideRectangle(Vector2 _point, RectTransform _rectTransform)
    {
        // Get the rectangular bounding box of your UI element
        Rect rect = _rectTransform.rect;

        // Get the left, right, top, and bottom boundaries of the rect
        float leftSide = _rectTransform.anchoredPosition.x - rect.width / 2;
        float rightSide = _rectTransform.anchoredPosition.x + rect.width / 2;
        float topSide = _rectTransform.anchoredPosition.y + rect.height / 2;
        float bottomSide = _rectTransform.anchoredPosition.y - rect.height / 2;

        // Check to see if the point is in the calculated bounds
        if (_point.x >= leftSide &&
            _point.x <= rightSide &&
            _point.y >= bottomSide &&
            _point.y <= topSide)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Debug check for missing references
    /// </summary>
    private void CheckForReferences()
    {
        if (localPlayerCamera == null)
        {
            Debug.LogError("FPS_Radar: local player Camera has not been assigned. Please use RegisterLocalPlayer() function to assign a Camera");
            return;
        }

        if (localPlayerTransform == null)
        {
            Debug.LogError("FPS_Radar: local player transform has not been assigned. Please use RegisterLocalPlayer() function to assign a Transform");
            return;
        }

        if (GO_PlayerIconPrefab == null)
        {
            Debug.LogError("FPS_Radar: player icon prefab has not been assigned.");
            return;
        }

        if (GO_ObjectiveIconPrefab == null)
        {
            Debug.LogError("FPS_Radar: objective icon prefab has not been assigned.");
            return;
        }

        if (radar_2Dcardinal_direction == null)
        {
            Debug.LogError("FPS_Radar: cardinal direction transform has not been assigned.");
            return;
        }

        if (radar2DIconsParent == null)
        {
            Debug.LogError("FPS_Radar: icons parent transform has not been assigned.");
            return;
        }

        if (radarRectTransform == null)
        {
            Debug.LogError("FPS_Radar: rect transform has not been assigned.");
            return;
        }

        if (radar_localPlayerIcon == null)
        {
            Debug.LogError("FPS_Radar: local player icon recTransform has not been assigned.");
            return;
        }

        if (radar_localPlayerFieldOfView == null)
        {
            Debug.LogError("FPS_Radar: local player field of view rectTransform has not been assigned.");
            return;
        }

        if (radar_directionalLineIndicator == null)
        {
            Debug.LogError("FPS_Radar: directional line indicator rectTransform has not been assigned.");
            return;
        }

        if (custom_map_parent == null)
        {
            Debug.LogError("FPS_Radar:custom map parent transform has not been assigned.");
            return;
        }
    }

    /// <summary>
    /// Responsible for positioning all player and objective icons on the radar/minimap
    /// </summary>
    private void DrawRadarEntities()
    {
        //Check if parent of radar icons is enabled / active
        if (radar2DIconsParent.gameObject.activeInHierarchy)
        {
            //Check if radar objects list contains any entities
            if (radar2DObjects.Count == 0)
                return;

            //Loop thrue all the radar objects in the list( player and objective )
            foreach (RadarObject radObject in radar2DObjects)
            {
                //Calculate the difference between local player and radar objects(enemy, friendly and objective icons)
                Vector3 radarPos = (radObject.OwnerGameobject.transform.position - localPlayerTransform.position);

                //Calculate the distance between localPlayer and radar icons
                float distToObject = Vector3.Distance(localPlayerTransform.position, radObject.OwnerGameobject.transform.position) * mapZoom;


                //delta angle of radar objects
                float deltay = 0f;

                if (mapMode == MapMode.Rotational)
                {
                    //Calculate delta angle for rotational radar
                    deltay = Mathf.Atan2(radarPos.x, radarPos.z) * Mathf.Rad2Deg - 270 - localPlayerTransform.eulerAngles.y;
                }

                if (mapMode == MapMode.Static)
                {
                    //Calculate delta angle for static radar
                    deltay = Mathf.Atan2(radarPos.x, radarPos.z) * Mathf.Rad2Deg - 270;
                }


                //Make player and objective icons stick to the border of the radar
                if (IconsStickToRadarBorder == true)
                {
                    if (radarShape == RadarShape.Circle)
                    {
                        //Check for area scan Id, because area scan should never stick to the border of the radar
                        if (radObject.radarItemId != areaScanObjectiveId)
                        {
                            //Clamp icon distance to the radius of the radar
                            distToObject = Mathf.Clamp(distToObject, 0, radar_radius);
                        }
                    }
                }

                //Calculate radar icon final position on the minimap/radar
                radarPos.x = distToObject * Mathf.Cos(deltay * Mathf.Deg2Rad) * -1f;
                radarPos.z = distToObject * Mathf.Sin(deltay * Mathf.Deg2Rad);


                //Make player and objective icons stick to the border of the radar
                if (IconsStickToRadarBorder == true)
                {
                    if (radarShape == RadarShape.Square)
                    {
                        //Check for area scan Id, because area scan should never stick to the border of the radar
                        if (radObject.radarItemId != areaScanObjectiveId)
                        {
                            //Clamp icon position to the rectangular area of the radar
                            radarPos.x = Mathf.Clamp(radarPos.x, -radar_radius, radar_radius);
                            radarPos.z = Mathf.Clamp(radarPos.z, -radar_radius, radar_radius);
                        }
                    }
                }

                //Set radar icon position on the minimap/radar
                radObject.RadarItemTransform.position = new Vector3(radarPos.x, radarPos.z, 0) + radar2DIconsParent.position;

                //Calculate the angle between localPlayer camera and other player forward directions relative to world up 
                float angle = Vector3.SignedAngle(radObject.OwnerGameobject.transform.forward, localPlayerTransform.forward, Vector3.up);

                //Calculate angle of the local player view
                float localPlayerAngle = ClampAngle(localPlayerTransform.eulerAngles.y);


                //Check if radar icon is an objective
                if (radObject.IsObjective == true)
                {
                    //Check if the Id match the directiona line Objective Id (-1 means it`s not active/assigned)
                    if (radObject.radarItemId == directionalLineObjectiveId && directionalLineObjectiveId != -1)
                    {
                        //Set the directional line starting point to center of the map
                        radar_directionalLineIndicator.anchoredPosition = Vector3.zero;

                        //Radar item position on the minimap
                        Vector3 p1 = radObject.RadarItemComponent.rct.anchoredPosition;

                        //Local player position on the minimap
                        Vector3 p2 = radar_localPlayerIcon.anchoredPosition;

                        //Calculate the length of the line from localPlayer(center of radar) to objective position(also on the minimap)
                        float directionalLineLengthX = Vector2.Distance(Vector2.zero, radObject.RadarItemComponent.rct.anchoredPosition);

                        //Calculate the rotational angle of the directional line
                        float angle2 = Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * Mathf.Rad2Deg;

                        //Apply the rotation in degress to the line
                        radar_directionalLineIndicator.eulerAngles = new Vector3(0, 0, angle2);

                        //Apply the previously calculated length of the line on the scale of X-axis
                        radar_directionalLineIndicator.localScale = new Vector3(directionalLineLengthX * 0.004f, radar_directionalLineIndicator.localScale.y, radar_directionalLineIndicator.localScale.z);
                    }
                }
                else //Player icons
                {
                    //Map rotates around the player
                    if (mapMode == MapMode.Rotational)
                    {
                        //We dont want to rotate localPlayer arrow, so we center it at zero
                        radar_localPlayerIcon.eulerAngles = Vector3.zero;

                        //Rotate radar cardinal directions based on localPlayer viewangle
                        radar_2Dcardinal_direction.eulerAngles = new Vector3(0, 0, localPlayerAngle);

                        //Set player icon rotation
                        radObject.RadarItemTransform.eulerAngles = new Vector3(0, 0, localPlayerAngle + (angle - localPlayerAngle));

                        //Check if custom map has been assigned
                        if (radarCustomMapImage != null)
                        {
                            //Position custom map image
                            Vector3 customMapPos = centerOfMapTransform.position - localPlayerTransform.position;

                            //Scale custom map position by zoom level
                            customMapPos *= mapZoom;

                            //Set the position of custom map image
                            custom_map_rectransform.anchoredPosition = new Vector2(customMapPos.x, customMapPos.z);

                            custom_map_parent.eulerAngles = new Vector3(0, 0, localPlayerAngle);

                            //Calculate the local scale of custom map parent
                            float mapScale = mapZoom / 20f;

                            //Set the local scale of custom map parent
                            custom_map_parent.GetChild(0).transform.localScale = new Vector3(mapScale, mapScale, mapScale);
                        }
                    }

                    //Local player rotates in the center, map is static
                    if (mapMode == MapMode.Static)
                    {
                        //Set cardinal direction indicators at default position(Noth = Upwards)
                        radar_2Dcardinal_direction.eulerAngles = Vector3.zero;

                        //Set localplayer icon rotation
                        radar_localPlayerIcon.eulerAngles = new Vector3(0, 0, -localPlayerAngle);

                        //Set other players icon rotation
                        radObject.RadarItemTransform.eulerAngles = new Vector3(0, 0, angle - localPlayerAngle);

                        //Check if custom map has been assigned
                        if (radarCustomMapImage != null)
                        {
                            //Position custom map image
                            Vector3 customMapPos = centerOfMapTransform.position - localPlayerTransform.position;

                            //Scale custom map position by zoom level
                            customMapPos *= mapZoom;

                            //Set the position of custom map image
                            custom_map_rectransform.anchoredPosition = new Vector2(customMapPos.x, customMapPos.z);

                            //custom_map_parent.eulerAngles = new Vector3(0, 0, localPlayerAngle);

                            //Calculate the local scale of custom map parent
                            float mapScale = mapZoom / 20f;

                            //Set the local scale of custom map parent
                            custom_map_parent.GetChild(0).transform.localScale = new Vector3(mapScale, mapScale, mapScale);
                        }
                    }
                }
            }
        }
    }
}