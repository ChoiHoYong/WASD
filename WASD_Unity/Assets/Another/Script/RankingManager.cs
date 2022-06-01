using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    //시간 선언
    private GameObject time_go;
    private Text time_text;

    //닉네임 선언
    private GameObject nickname_go;
    private Text nickname_text;

    void Start()
    {
        Rank();
    }

    void Update()
    {
        
    }

    void Rank()
    {
        //시간 가져오기
        time_go = GameObject.Find("Rank").transform.GetChild(1).transform.GetChild(3).gameObject;
        time_text = time_go.GetComponent<Text>();
        time_text.text = PlayerPrefs.GetString("Time");

        //닉네임 받아오기
        nickname_go = GameObject.Find("Rank").transform.GetChild(1).transform.GetChild(2).gameObject;
        nickname_text = nickname_go.GetComponent<Text>();
        nickname_text.text = PlayerPrefs.GetString("NickName");

    }
}
