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
// ���͸� �����Ҷ��� �ð��� ���� �����ϰų�, ������ �����ؼ� ��ϵ� ������ ��������
[CreateAssetMenu(fileName = "Spawn", menuName = "ScriptableObjects/Spawn", order = 1)]
public class SpawnScriptableObject : ScriptableObject
{
    public int spawnCount = 3;
    public List<SpawnInfo> spawnList = new List<SpawnInfo>();
}