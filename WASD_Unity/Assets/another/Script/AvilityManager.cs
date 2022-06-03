using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvilityManager : MonoBehaviour
{
    //Sprite �ɷ� ����
    [SerializeField]
    public Sprite[] avilities = new Sprite[3];
    
    //�ɷ� 
    private GameObject Icon1;
    private GameObject Icon2;
    private GameObject Icon3;
    
    private Image image1;
    private Image image2;
    private Image image3;

    //�ؽ�Ʈ 
    private GameObject text;
    private Text clear_text;

    //�г���
    private GameObject nickname_go;
    private Text nickname_text;

    //�ð�
    string[] stime_sp;
    string stime;
    int ntime1 = 0;
    int ntime2 = 0;

    void Start()
    {
        //Ŀ��
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        avility_choice();
        
        clear_time();

    }

    public void Input_Text()
    {
        nickname_go = GameObject.Find("NickName_input").transform.GetChild(2).gameObject;
        nickname_text = nickname_go.GetComponent<Text>();
        PlayerPrefs.SetString("NickName", nickname_text.text.ToString());
    }

    public void clear_time()
    {
        text = GameObject.Find("ClearTime_text");
        clear_text = text.GetComponent<Text>();
        clear_text.text = GameData.clearTime.ToString($"mm\\:ss");
        PlayerPrefs.SetString("Time", GameData.clearTime.ToString($"mm\\:ss"));
        
        stime = PlayerPrefs.GetString("Time");
        stime_sp = stime.Split(":");
        ntime1 = Int32.Parse(stime_sp[0]);
        ntime2 = Int32.Parse(stime_sp[1]);
        PlayerPrefs.SetInt("Clear_front", ntime1);
        PlayerPrefs.SetInt("Clear_back", ntime2);
    }

    public void avility_choice()
    {
        //������ ����.
        Icon1 = GameObject.Find("Avility_list_icon").transform.GetChild(0).gameObject;
        Icon2 = GameObject.Find("Avility_list_icon").transform.GetChild(1).gameObject;
        Icon3 = GameObject.Find("Avility_list_icon").transform.GetChild(2).gameObject;

        //�������� �̹��� ����.
        image1 = Icon1.GetComponent<Image>();
        image2 = Icon2.GetComponent<Image>();
        image3 = Icon3.GetComponent<Image>();

        //GameData.avility_list.Add("Hp");
        //GameData.avility_list.Add("Hp");

        // �����Ƽ 1�� ã�� �ֱ�
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

        // �����Ƽ 2�� ã�� �ֱ�
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

        // �����Ƽ 3�� ã�� �ֱ�
        if (PlayerPrefs.GetString("avility3") == "attack")
        {
            image3.sprite = avilities[0];
        }
        else if (PlayerPrefs.GetString("avility3") == "hp")
        {
            image3.sprite = avilities[1];
        }
        else if (PlayerPrefs.GetString("avility3") == "speed")
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
