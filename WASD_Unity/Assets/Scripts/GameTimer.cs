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

    //    // timespan값으로 변경합니다.
    //    timespan seconds = timespan.fromseconds(timelimit);

    //    // 04:29와 같은 형태로 출력
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

        // 현재 시간에서 이전 시간을 빼서 경과시간을 구합니다.
        TimeSpan elapsed = (DateTime.Now - prevTime );

        //if( elapsed.TotalSeconds >= 1.0f )
        //{
            // 시간값을 현재 시간으로 갱신합니다.
            //prevTime = DateTime.Now;

            // 1초 감소합니다.
            //--timeLimit;

            // TimeSpan값으로 변경합니다. 
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
