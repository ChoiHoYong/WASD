using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    // �ʴ��� ���� �ð�
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
            // TimeSpan - �� ��¥ ���� ���̰��� ��Ÿ��
            // ���� �ð����� ���� �ð��� ���� ����ð��� ���մϴ�.
            elapsed = (DateTime.Now - prevTime);
            total = GameData.clearTime.AddSeconds(elapsed.TotalSeconds);
            timeText.text = total.ToString($"mm\\:ss");
        }


    }
}
