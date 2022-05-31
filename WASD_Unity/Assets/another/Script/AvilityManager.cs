using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvilityManager : MonoBehaviour
{
    //Sprite 능력 모음
    [SerializeField]
    public Sprite[] avilities = new Sprite[3];
    
    //게임오브젝트 변수
    private GameObject Icon1;
    private GameObject Icon2;
    private GameObject Icon3;
    
    //게임오브젝트의 이미지 변수
    private Image image1;
    private Image image2;
    private Image image3;

    //
    private GameObject text;
    private Text clear_text;
    private DateTime total;

    void Start()
    {
        avility_choice();
        
        clear_time();
    }

    public void clear_time()
    {
        text = GameObject.Find("ClearTime_text");
        clear_text = text.GetComponent<Text>();
        clear_text.text = GameData.clearTime.ToString($"mm\\:ss");
    }

    public void avility_choice()
    {
        //아이콘 지정.
        Icon1 = GameObject.Find("Avility_list_icon").transform.GetChild(0).gameObject;
        Icon2 = GameObject.Find("Avility_list_icon").transform.GetChild(1).gameObject;
        Icon3 = GameObject.Find("Avility_list_icon").transform.GetChild(2).gameObject;

        //아이콘의 이미지 지정.
        image1 = Icon1.GetComponent<Image>();
        image2 = Icon2.GetComponent<Image>();
        image3 = Icon3.GetComponent<Image>();

        //GameData.avility_list.Add("Hp");
        //GameData.avility_list.Add("Hp");

        // 어빌리티 1번 찾고 넣기
        if (PlayerPrefs.GetString("avility1") == "attack")
        {
            image1.sprite = avilities[0];
        }
        else if (PlayerPrefs.GetString("avility1") == "hp")
        {
            image1.sprite = avilities[1];
        }
        else if (PlayerPrefs.GetString("avility1") == "speed")
        {
            image1.sprite = avilities[2];
        }

        // 어빌리티 2번 찾고 넣기
        if (PlayerPrefs.GetString("avility2") == "attack")
        {
            image2.sprite = avilities[0];
        }
        else if (PlayerPrefs.GetString("avility2") == "hp")
        {
            image2.sprite = avilities[1];
        }
        else if (PlayerPrefs.GetString("avility2") == "speed")
        {
            image2.sprite = avilities[2];
        }

        // 어빌리티 3번 찾고 넣기
        if (PlayerPrefs.GetString("avility3") == "attack")
        {
            image3.sprite = avilities[0];
        }
        else if (PlayerPrefs.GetString("avility3") == "hp")
        {
            image3.sprite = avilities[1];
        }
        else if (PlayerPrefs.GetString("avility1") == "speed")
        {
            image3.sprite = avilities[2];
        }

        //if (GameData.avility_list[0] == "Attack")
        //    image1.sprite = avilities[0];
        //else if (GameData.avility_list[0] == "Hp")
        //    image1.sprite = avilities[1];
        //else if (GameData.avility_list[0] == "Speed")
        //    image1.sprite = avilities[2];

        //if (GameData.avility_list[1] == "Attack")
        //    image2.sprite = avilities[0];
        //else if (GameData.avility_list[1] == "Hp")
        //    image2.sprite = avilities[1];
        //else if (GameData.avility_list[1] == "Speed")
        //    image2.sprite = avilities[2];
        
    }
}
