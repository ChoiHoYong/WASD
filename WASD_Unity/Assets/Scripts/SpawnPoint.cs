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

        // ĳ���͸� �����Ͽ��ٸ� �̴ϸʿ� ��ϵ� �� �ֵ��� ó���մϴ�.
        Minimap minimap = GameObject.FindObjectOfType<Minimap>();
        if (minimap != null)
            minimap.AddIcon(newChar);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
