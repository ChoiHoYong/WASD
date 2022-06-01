using System;

using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI;



public class RankingManager : MonoBehaviour

{

    //Sprite �ɷ� ����

    [SerializeField]

    public Sprite[] avilities = new Sprite[3];



    //�ð� ����

    private GameObject time_go;

    private Text time_text;



    //�г��� ����

    private GameObject nickname_go;

    private Text nickname_text;



    //�ɷ� ����

    private GameObject avility_go1;

    private Image avility_img1;

    private GameObject avility_go2;

    private Image avility_img2;

    private GameObject avility_go3;

    private Image avility_img3;



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



        //�г��� ��������

        nickname_go = GameObject.Find("Rank").transform.GetChild(1).transform.GetChild(2).gameObject;

        nickname_text = nickname_go.GetComponent<Text>();

        nickname_text.text = PlayerPrefs.GetString("NickName");





        //�ɷ� ��������

        avility_go1 = GameObject.Find("Rank").transform.GetChild(1).transform.GetChild(4).transform.GetChild(0).gameObject;

        avility_img1 = avility_go1.GetComponent<Image>();

        if (PlayerPrefs.GetString("avility1") == "attack")

            avility_img1.sprite = avilities[0];

        else if (PlayerPrefs.GetString("avility1") == "hp")

            avility_img1.sprite = avilities[1];

        else if (PlayerPrefs.GetString("avility1") == "speed")

            avility_img1.sprite = avilities[2];



        avility_go2 = GameObject.Find("Rank").transform.GetChild(1).transform.GetChild(4).transform.GetChild(1).gameObject;

        avility_img2 = avility_go2.GetComponent<Image>();

        if (PlayerPrefs.GetString("avility2") == "attack")

            avility_img2.sprite = avilities[0];

        else if (PlayerPrefs.GetString("avility2") == "hp")

            avility_img2.sprite = avilities[1];

        else if (PlayerPrefs.GetString("avility2") == "speed")

            avility_img2.sprite = avilities[2];



        avility_go3 = GameObject.Find("Rank").transform.GetChild(1).transform.GetChild(4).transform.GetChild(2).gameObject;

        avility_img3 = avility_go3.GetComponent<Image>();

        if (PlayerPrefs.GetString("avility3") == "attack")

            avility_img3.sprite = avilities[0];

        else if (PlayerPrefs.GetString("avility3") == "hp")

            avility_img3.sprite = avilities[1];

        else if (PlayerPrefs.GetString("avility3") == "speed")

            avility_img3.sprite = avilities[2];

    }

}