using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    // �ʴ��� ���� �ð�
    private int timeLimit = 300;
    private DateTime startTime;
    private DateTime prevTime;
    private TMP_Text timeText;
    private bool end = false;

    // Start is called before the first frame update
    public void Init()
    {
        timeText = GetComponent<TMP_Text>();
        startTime = DateTime.Now;
        prevTime = startTime;
    }

    public void Execute(int minute)
    {
        end = false;
        timeLimit = minute * 60;

        // TimeSpan������ �����մϴ�.
        TimeSpan seconds = TimeSpan.FromSeconds(timeLimit);

        // 04:29�� ���� ���·� ���
        if(timeText != null)
            timeText.text = seconds.ToString($"mm\\:ss");
    }

    // Update is called once per frame
    void Update()
    {
        if (timeText == null)
            return;
        if( end )
            return;

        // ���� �ð����� ���� �ð��� ���� ����ð��� ���մϴ�.
        TimeSpan elapsed = (DateTime.Now - prevTime );

        if( elapsed.TotalSeconds >= 1.0f )
        {
            // �ð����� ���� �ð����� �����մϴ�.
            prevTime = DateTime.Now;

            // 1�� �����մϴ�.
            --timeLimit;

            // TimeSpan������ �����մϴ�. 
            TimeSpan seconds = TimeSpan.FromSeconds(timeLimit);

            // 04:29
            timeText.text = seconds.ToString($"mm\\:ss");

            if( timeLimit <= 0 )
            {
                end = true;
                // ���� ���� �ڵ尡 �ۼ��� �����Դϴ�.!
                Controller controller = GameObject.FindObjectOfType<Controller>();
                if( controller != null )
                {
                    controller.SendMessage("Die", SendMessageOptions.DontRequireReceiver );
                }
            }

        }

    }
}
