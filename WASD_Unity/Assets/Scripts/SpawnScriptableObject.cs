using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    Warrior, // 0
    Mage, // 1
    Archer, // 2
    King // 3
}

[System.Serializable]
public class SpawnInfo
{
    public float spawnTime;

    public int spawnIndex;

    public MonsterType monsterType;
}
// 몬스터를 생성할때는 시간에 따라서 생성하거나, 개수를 설정해서 등록된 정보를 바탕으로
[CreateAssetMenu(fileName = "Spawn", menuName = "ScriptableObjects/Spawn", order = 1)]
public class SpawnScriptableObject : ScriptableObject
{
    public int spawnCount = 3;
    public List<SpawnInfo> spawnList = new List<SpawnInfo>();
}