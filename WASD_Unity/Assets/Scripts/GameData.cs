using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int currStage = 1;
    public static int maxStage = 4;
    //public static List<string> avility_list = new List<string>();
    // ������ Ŭ���� �ϱ� ���� �ɸ� �ð�
    public static DateTime clearTime;
    // �� ���������� Ŭ���� �ϱ� ���� �ɸ� �ð�
    public static int elapsed;
    public static bool isClear = false;
    public static bool isReady = false;
    public static int Damage = 1;
    public static float speed = 2.5f;
    public static int HP = -1;
}
