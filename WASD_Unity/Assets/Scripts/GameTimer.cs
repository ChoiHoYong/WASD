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
            // TimeSpan - �� ��¥ ���� ���̰��� ��Ÿ��
            // ���� �ð����� ���� �ð��� ���� ����ð��� ���մϴ�.
            elapsed = (DateTime.Now - prevTime);
            second = elapsed.Seconds;
            GameData.elapsed = second;
            total = GameData.clearTime + elapsed;
            timeText.text = total.ToString($"mm\\:ss");
        }


    }
}
