using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    public float worldWidth;
    public float worldDepth;
    public float uiMapWidth;
    public float uiMapHeight;
    public RPGBackground background;
    public List<MapIcon> mapIcons = new List<MapIcon>();
    public PlayerIcon playerIcon;

    public void AddIcon( Transform target )
    {
        MapIcon mapIcon = Resources.Load<MapIcon>("Prefabs/MapIcon");
        if( mapIcon != null )
        {
            mapIcon = Instantiate(mapIcon, background.transform);
            mapIcon.Init();
            mapIcon.SetTarget(target);
            mapIcons.Add(mapIcon);
        }
    }

    // Start is called before the first frame update
    public void Init()
    {
        GameObject min = GameObject.Find("Min");
        GameObject max = GameObject.Find("Max");
        // Mathf.Abs�� ���밪�� ���ϴ� �Լ��Դϴ�.
        //Mathf.Abs(min.transform.position.x);

        // Max�Լ��� �� ���߿��� ū ���� �Ѱ��ִ� �Լ��Դϴ�.
        float maxX = Mathf.Max(min.transform.position.x, max.transform.position.x);
        // Min�Լ��� �� ���߿��� ���� ���� �Ѱ��ִ� �Լ��Դϴ�.
        float minX = Mathf.Min(min.transform.position.x, max.transform.position.x);

        float maxZ = Mathf.Max(min.transform.position.z, max.transform.position.z);
        float minZ = Mathf.Min(min.transform.position.z, max.transform.position.z);

        worldWidth = maxX - minX;
        worldDepth = maxZ - minZ;

        Transform t = transform.Find("Mask/Background");
        if( t != null )
        {
            background = t.GetComponent<RPGBackground>();
            background.Init();
            background.SetInfo(worldWidth, worldDepth);
            Vector2 uisize = background.SizeDelta;
            uiMapWidth = uisize.x;
            uiMapHeight = uisize.y;
        }

        playerIcon = GetComponentInChildren<PlayerIcon>();
        if (playerIcon != null)
            playerIcon.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
