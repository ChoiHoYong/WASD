using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int currStage = 1;
    public static int maxStage = 4;
    //public static List<string> avility_list = new List<string>();
    // 게임을 클리어 하기 까지 걸린 시간
    public static DateTime clearTime;
    // 각 스테이지를 클리어 하기 까지 걸린 시간
    public static int elapsed;
    public static bool isClear = false;
    public static bool isReady = false;
    public static int Damage = 1;
    public static float speed = 2.5f;
    public static int HP = -1;
}
