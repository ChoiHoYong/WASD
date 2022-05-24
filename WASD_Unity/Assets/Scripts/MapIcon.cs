using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapIcon : MonoBehaviour
{
    // ui아이콘이 참고할 3d 오브젝트
    public Transform target;
    public Image icon;
    // 3차원 맵의 사이즈
    //public float worldWidth;
    //public float worldDepth;
    // Start is called before the first frame update
    public void Init()
    {
        icon = GetComponent<Image>();
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    // Update is called once per frame
    void Update()
    {
        Minimap minimap = GameObject.FindObjectOfType<Minimap>();

        Vector3 uiPos = UIHelper.WorldPosToMapPos(target.position, minimap.worldWidth, minimap.worldDepth, minimap.uiMapWidth, minimap.uiMapHeight);
        transform.localPosition = uiPos;
    }
}
