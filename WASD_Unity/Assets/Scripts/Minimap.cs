using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    public float worldWidth;
    public float worldDepth;
    public float uiMapWidth;
    public float uiMapHeigth;
    public RPGBackground background;
    public List<MapIcon> mapIcons = new List<MapIcon>();
    public PlayerIcon playerIcon;

    public void AddIcon(Transform target)
    {
        MapIcon mapIcon = Resources.Load<MapIcon>("Prefabs/MapIcon");
        if(mapIcon != null)
        {
            Instantiate(mapIcon, background.transform);
            mapIcon.Init();
            mapIcon.SetTarget(target);
            mapIcons.Add(mapIcon);
        }
    }
    
    public void Init()
    {
        GameObject min = GameObject.Find("Min");
        GameObject max = GameObject.Find("Max");

        //Mathf.Abs(min.transform.position.x);
        // Max�Լ��� �� ���߿��� ū ���� �Ѱ��ִ� �Լ��Դϴ�.
        float maxX = Mathf.Max(min.transform.position.x, max.transform.position.x);
        // Min�Լ��� �� ���߿��� ���� ���� �Ѱ��ִ� �Լ��Դϴ�.
        float minX = Mathf.Min(min.transform.position.x, max.transform.position.x);
        // Max�Լ��� �� ���߿��� ū ���� �Ѱ��ִ� �Լ��Դϴ�.
        float maxZ = Mathf.Max(min.transform.position.z, max.transform.position.z);
        // Min�Լ��� �� ���߿��� ���� ���� �Ѱ��ִ� �Լ��Դϴ�.
        float minZ = Mathf.Min(min.transform.position.z, max.transform.position.z);

        worldWidth = maxX - minX;
        worldDepth = maxZ - minZ;

        Transform t = transform.Find("Mask/Background");
        if(t != null)
        {
            background = t.GetComponent<RPGBackground>();
            background.Init();
            background.SetInfo(worldWidth, worldDepth);
            Vector2 uiSize = background.SizeDelta;
            uiMapWidth = uiSize.x;
            uiMapHeigth = uiSize.y;
        }

        playerIcon = GetComponentInChildren<PlayerIcon>();
        if (playerIcon != null)
            playerIcon.Init();
        //print(worldWidth);
        //print(worldDepth);
        //print(uiMapWidth);
        //print(uiMapHeigth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
