using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script demonstrates on how to use FPS_Radar.cs
/// This is just an example integration with the most common examples
/// For more functionality, read the documentation.pdf included in the "AdvancedFPSRadar/Documentation" folder
/// </summary>
public class RadarDemo : MonoBehaviour
{
    public AdvancedFPSRadar fpsRadar;       //References to the AdvancedFPSRadar script

    public Camera localPlayerCamera;        //Assign local player Camera(to register local player on the radar)

    public int highLightPlayerId = 3;       //Highlight specific player on the radar

    public int highLightObjectiveId = 6;    //Highlight specific objective on the radar

    public int directionalObjectiveId = 6;  //Draw a directional line from local player to the objective

    public int killPlayerById = 6;          //Kill specific player by Id on the radar

    bool isPlayerAlive = false;             //Toggle player alive state(alive/dead)

    bool isPlayerHighLightActive = false;   //Toggle player highlight on/off

    bool isObjectiveHighLightActive = false;//Toggle player highlight on/off

    bool isObjectiveLineActive = false;     //Toggle objective directional line on/off

    //--------------------------------------------------
    //Note: All the players and objectives MUST have unique Id-s
    //--------------------------------------------------

    public GameObject[] friendlyGO;         //Assign friendly player GameObject 
    public int[] friendlyPlayerId;          //Assign unique friendly player ID-s

    public GameObject[] enemyGO;            //Assign enemy player GameObject 
    public int[] enemyPlayerId;             //Assign unique enemy player ID-s

    public GameObject[] objectives;         //Assign list of objectives(For example Flags)
    public int[] objectiveId;               //Assign unique objective ID-s

    public GameObject areaScanGO;           //Assign area scan game=Object


    void Start()
    {
        //--------------------------------------------------
        //FIRST -> Register players for the radar display

        fpsRadar.RegisterLocalPlayer(localPlayerCamera);


        //Note: You will need to manage player teams and unique ID-s in your project
        //to assign & update player icons on the radar
        //--------------------------------------------------

        //--------------------------------------------------
        //SECOND -> Register players for the radar display
        //--------------------------------------------------

        //Loop thrue all the friendly team players
        for (int id = 0; id < friendlyGO.Length; id++)
        {
            //Add friendly player to radar
            fpsRadar.RegisterRadarPlayerObject(friendlyGO[id], false, friendlyPlayerId[id]);
        }

        //Loop thrue all the enemy team players
        for (int id = 0; id < enemyGO.Length; id++)
        {
            //Add an enemy player to radar
            fpsRadar.RegisterRadarPlayerObject(enemyGO[id], true, enemyPlayerId[id]);
        }

        //--------------------------------------------------
        //THIRD -> Register objectives for the radar display
        //--------------------------------------------------

        //Loop thrue all the radar objectives
        for (int id = 0; id < objectives.Length; id++)
        {
            //Add objective to the radar
            fpsRadar.RegisterRadarObjective(objectives[id], fpsRadar.radarIconObjectives[id], objectiveId[id]);
        }

        //--------------------------------------------------
        //NOTE:
        //--------------------------------------------------
        // Unique ID-s need to be assigned for FPS_Radar functionality like:
        // 1. Spotting enemy players (based on playerId)
        // 2. Updating player icon colors based on team changes 
        // 3. Updating player icons based on alive-states(different icon for dead players)
        // 4. Removing specific players or objectives from the minimap/radar
    }

    void Update()
    {
        // Here we simulate a common FPS functionality 
        // where users can aim their crosshair 
        // and click the left mouse button to spot enemies
        //Note: Commonly this is done by holding down a keyboard key 
        // and clicking on enemy player to spot him. 
        // For simplicity of this example, we just use left mouse click

        //Primary(left) mouse click
        if (Input.GetMouseButtonDown (0))
        {
            //Ray from the center of screen
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            //Save raycast hit
            RaycastHit hit;

            //Start raycast
            if (Physics.Raycast(ray, out hit, 100))
            {
                //Holds player Id
                int playerId;

                //Get the unique player Id(in this case from tag of the player gameObject)
                int.TryParse(hit.transform.gameObject.tag, out playerId);

                //Check if raycast hit player
                if (hit.transform.gameObject.layer == 8)
                {
                    //Spot the player on the radar by Id
                    fpsRadar.SpotEnemyOnRadar(playerId);
                }

                //Check if raycast hit objective layer
                if (hit.transform.gameObject.layer == 9)
                {
                    //Set the directional line from localPlayer(center of radar icon) to the objective icon
                    fpsRadar.SetDirectionalIndicatorAtObjective(true, playerId);
                }

                //Check if raycast hit area scan layer
                if (hit.transform.gameObject.layer == 10)
                {
                    //Start area scan at the position of hit gameobject
                    //Note: Are scan needs an actual gameObject inside the game world/scene
                    fpsRadar.StartEnemyAreaScanAtPosition(hit.transform.gameObject, 10f, 10f);
                }
            }
        }

        //Start UAV Scan on key press
        if(Input.GetKeyDown(KeyCode.U))
        {
            //Start the scan for enemies
            StartUAVScan();
        }
    }

    //Highlight player displays a circular border around the default player icon on the radar
    public void ToggleHighLightPlayerOnRadar()
    {
        //Toggle the player highlight switch on/off
        isPlayerHighLightActive =! isPlayerHighLightActive;

        //Highlight player by Id on the radar
        fpsRadar.HighLightPlayer(highLightPlayerId, isPlayerHighLightActive);
    }

    //Highlight objective displays a circular border around the default player icon on the radar
    public void ToggleHighLightObjectiveOnRadar()
    {
        //Toggle the objective highlight switch on/off
        isObjectiveHighLightActive = !isObjectiveHighLightActive;

        //Highlight player by Id on the radar
        fpsRadar.HighLightObjective(highLightObjectiveId, isObjectiveHighLightActive);
    }

    public void StartUAVScan()
    {
        //Start UAV Scan of the entire radar for enemies, default: 5 seconds length
        fpsRadar.StartUAVScan(5f);
    }

    public void StartAreaScan()
    {
        //Start enemy Area Scan for enemies, default: 10 seconds length and 10 meter/unit radius
        fpsRadar.StartEnemyAreaScanAtPosition(areaScanGO, 10f, 10f);
    }

    public void SpotEnemy()
    {
        //Spot a specific enemy player on the radar by Id(current list of enemy Ids: 4, 5, 6)
        fpsRadar.SpotEnemyOnRadar(6);
    }

    public void ToggleIconsStickToBorder()
    {
        //Toggle option to stick icons on the border of the radar
        fpsRadar.IconsStickToRadarBorder =!fpsRadar.IconsStickToRadarBorder;
    }

    //Toggle player alive state on the radar(show death icon vs alive icon)
    public void KillPlayerOnRadar()
    {
        isPlayerAlive = !isPlayerAlive;

        fpsRadar.SetPlayerAliveOnRadar(killPlayerById, isPlayerAlive, true);
    }

    //Set the square size of all player icons on the radar(in pixels)
    public void SetPlayersIconSizes(float _newIconSize)
    {
        fpsRadar.UpdatePlayerIconSize(_newIconSize);
    }

    //Set the square size of all objective icons on the radar(in pixels)
    public void SetObjectivesIconSizes(float _newIconSize)
    {
        fpsRadar.UpdateObjectiveIconSize(_newIconSize);
    }

    //Draw directional line to objective
    public void DrawDirectionalLineToObjective()
    {
        //Toggle directional line switch on/off
        isObjectiveLineActive = !isObjectiveLineActive;

        //Set directional line to objective by objective Id
        fpsRadar.SetDirectionalIndicatorAtObjective(isObjectiveLineActive, directionalObjectiveId);
    }
}
