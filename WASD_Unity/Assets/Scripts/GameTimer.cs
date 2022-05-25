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
    private bool end = false;

    // Start is called before the first frame update
    public void Init()
    {
        timeText = GetComponent<TMP_Text>();
        startTime = DateTime.Now;
        prevTime = startTime;
    }

    //public void execute(int minute)
    //{
    //    end = false;
    //    timelimit = minute * 60;

    //    // timespan������ �����մϴ�.
    //    timespan seconds = timespan.fromseconds(timelimit);

    //    // 04:29�� ���� ���·� ���
    //    if(timetext != null)
    //        timetext.text = seconds.tostring($"mm\\:ss");
    //}

    // Update is called once per frame
    void Update()
    {
        if (timeText == null)
            return;
        if( end )
            return;

        // ���� �ð����� ���� �ð��� ���� ����ð��� ���մϴ�.
        TimeSpan elapsed = (DateTime.Now - prevTime );

        //if( elapsed.TotalSeconds >= 1.0f )
        //{
            // �ð����� ���� �ð����� �����մϴ�.
            //prevTime = DateTime.Now;

            // 1�� �����մϴ�.
            //--timeLimit;

            // TimeSpan������ �����մϴ�. 
            //TimeSpan seconds = TimeSpan.FromSeconds(timeLimit);

            // 04:29
            timeText.text = elapsed.ToString($"mm\\:ss");

            //if( timeLimit <= 0 )
            //{
            //    end = true;
            //    Controller controller = GameObject.FindObjectOfType<Controller>();
            //    if( controller != null )
            //    {
            //        controller.SendMessage("Die", SendMessageOptions.DontRequireReceiver );
            //    }
            //}

        //}

    }
}
