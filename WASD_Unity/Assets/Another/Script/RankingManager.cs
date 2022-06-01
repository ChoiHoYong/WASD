using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    //�ð� ����
    private GameObject time_go;
    private Text time_text;

    //�г��� ����
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
        //�ð� ��������
        time_go = GameObject.Find("Rank").transform.GetChild(1).transform.GetChild(3).gameObject;
        time_text = time_go.GetComponent<Text>();
        time_text.text = PlayerPrefs.GetString("Time");

        //�г��� �޾ƿ���
        nickname_go = GameObject.Find("Rank").transform.GetChild(1).transform.GetChild(2).gameObject;
        nickname_text = nickname_go.GetComponent<Text>();
        nickname_text.text = PlayerPrefs.GetString("NickName");

    }
}
