using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnList : MonoBehaviour
{
    private void OnGUI()
    {
        if(GUI.Button(new Rect(0,0,100,100),"����"))
        {
            SpawnPoint[] spawnArray = GetComponentsInChildren<SpawnPoint>();
            foreach(var spawn in spawnArray)
            {
                spawn.Create();
            }
        }
    }
}
