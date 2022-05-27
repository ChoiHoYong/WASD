using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    private DateTime prevTime;
    private TMP_Text timeText;
    private TimeSpan elapsed;
    private DateTime total;
    private int second;

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
            second = elapsed.Seconds;
            GameData.elapsed = second;
            total = GameData.clearTime + elapsed;
            timeText.text = total.ToString($"mm\\:ss");
        }


    }
}
