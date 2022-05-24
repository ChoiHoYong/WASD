using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapIcon : MonoBehaviour
{
    // ui�������� ������ 3d ������Ʈ
    public Transform target;
    public Image icon;
    // 3���� ���� ������
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
