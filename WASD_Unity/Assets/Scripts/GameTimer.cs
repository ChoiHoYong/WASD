using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    // 초단위 제한 시간
    //private int timeLimit = 300;
    private DateTime startTime;
    private DateTime prevTime;
    private TMP_Text timeText;
    private TimeSpan elapsed;
    private DateTime total;

    public void Init()
    {
        timeText = GetComponent<TMP_Text>();
        prevTime = DateTime.Now;
    }
    void Update()
    {
        if (timeText == null)
            return;
        if(GameData.isClear == false)
        {
            // TimeSpan - 두 날짜 간의 차이값을 나타냄
            // 현재 시간에서 이전 시간을 빼서 경과시간을 구합니다.
            elapsed = (DateTime.Now - prevTime);
            total = GameData.clearTime.AddSeconds(elapsed.TotalSeconds);
            timeText.text = total.ToString($"mm\\:ss");
        }


    }
}
