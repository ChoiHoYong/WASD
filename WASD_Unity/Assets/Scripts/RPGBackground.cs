using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGBackground : MonoBehaviour
{
    public Transform player;
    public float worldWidth;
    public float worldDepth;
    public Vector2 uiSize;

    public Vector2 SizeDelta
    {
        get
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            if (rectTransform != null)
                return rectTransform.sizeDelta;
            return Vector2.zero;
        }
    }

    public void Init()
    {
        uiSize = SizeDelta;
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    public void SetInfo(float map3DWidth,float map3DDepth)
    {
        worldWidth = map3DWidth;
        worldDepth = map3DDepth;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;
        // 3D 좌표상의 원점을 기준으로 배치를 하였습니다.
        transform.localPosition = UIHelper.WorldPosToMapPos(player.position, worldWidth, worldDepth, uiSize.x, uiSize.y) * -1;
    }
}
