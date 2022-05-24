using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public string prefabName = string.Empty;

    public void Create()
    {
        Transform newChar =
        Instantiate(Resources.Load<Transform>("Prefabs/" + prefabName),
                    transform.position,
                    transform.rotation);

        // 캐릭터를 생성하였다면 미니맵에 등록될 수 있도록 처리합니다.
        Minimap minimap = GameObject.FindObjectOfType<Minimap>();
        if (minimap != null)
            minimap.AddIcon(newChar);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
