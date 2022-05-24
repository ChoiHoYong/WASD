using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnList : MonoBehaviour
{
    void OnGUI() //에디터용 GUI, 이벤트 함수
    {
        if (GUI.Button(new Rect(0,0,100,100), "생성"))
        {
            SpawnPoint[] spawnArray = GetComponentsInChildren<SpawnPoint>();
            foreach( var spawn in spawnArray)
            {
                spawn.Create();
            }
        }
    }
}
