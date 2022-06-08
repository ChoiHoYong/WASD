using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvilityManager : MonoBehaviour
{
    GameObject audio;
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

    string avility1;
    string avility2;
    string avility3;

    void Start()
    {
        //Ŀ��
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        avility_choice();
        
        clear_time();
        audio = GameObject.Find("GameAudioManager");
        if (audio != null)
            Destroy(audio);
    }

    public void Input_Text()
    {
        nickname_go = GameObject.Find("NickName_input").transform.GetChild(2).gameObject;
        nickname_text = nickname_go.GetComponent<Text>();
        PlayerPrefs.SetString("NickName", nickname_text.text.ToString());
        bool isRecord = false;
        for (int i = 1; i <= 5; i++)
        {
            if (PlayerPrefs.HasKey(i + "Time"))
            {
                int time = PlayerPrefs.GetInt(i + "Time");
                if (time > GameData.clearTime.Minute * 60 + GameData.clearTime.Second)
                {
                    switch (i)
                    {
                        case 1:
                            for (int j = 5; j > 1; j--)
                            {
                                if (PlayerPrefs.HasKey((j - 1) + "Time"))
                                {
                                    PlayerPrefs.SetString(j + "Name", PlayerPrefs.GetString((j - 1) + "Name"));
                                    PlayerPrefs.SetInt(j + "Time", PlayerPrefs.GetInt((j - 1) + "Time"));
                                    PlayerPrefs.SetString(j + "Avility1", PlayerPrefs.GetString((j - 1) + "Avility1"));
                                    PlayerPrefs.SetString(j + "Avility2", PlayerPrefs.GetString((j - 1) + "Avility2"));
                                    PlayerPrefs.SetString(j + "Avility3", PlayerPrefs.GetString((j - 1) + "Avility3"));
                                }
                            }
                            break;
                        case 2:
                            for (int j = 5; j > 2; j--)
                            {
                                if (PlayerPrefs.HasKey((j - 1) + "Time"))
                                {
                                    PlayerPrefs.SetString(j + "Name", PlayerPrefs.GetString((j - 1) + "Name"));
                                    PlayerPrefs.SetInt(j + "Time", PlayerPrefs.GetInt((j - 1) + "Time"));
                                    PlayerPrefs.SetString(j + "Avility1", PlayerPrefs.GetString((j - 1) + "Avility1"));
                                    PlayerPrefs.SetString(j + "Avility2", PlayerPrefs.GetString((j - 1) + "Avility2"));
                                    PlayerPrefs.SetString(j + "Avility3", PlayerPrefs.GetString((j - 1) + "Avility3"));
                                }
                            }
                            break;
                        case 3:
                            for (int j = 5; j > 3; j--)
                            {
                                if (PlayerPrefs.HasKey((j - 1) + "Time"))
                                {
                                    PlayerPrefs.SetString(j + "Name", PlayerPrefs.GetString((j - 1) + "Name"));
                                    PlayerPrefs.SetInt(j + "Time", PlayerPrefs.GetInt((j - 1) + "Time"));
                                    PlayerPrefs.SetString(j + "Avility1", PlayerPrefs.GetString((j - 1) + "Avility1"));
                                    PlayerPrefs.SetString(j + "Avility2", PlayerPrefs.GetString((j - 1) + "Avility2"));
                                    PlayerPrefs.SetString(j + "Avility3", PlayerPrefs.GetString((j - 1) + "Avility3"));
                                }
                            }
                            break;
                        case 4:
                            for (int j = 5; j > 4; j--)
                            {
                                if (PlayerPrefs.HasKey((j - 1) + "Time"))
                                {
                                    PlayerPrefs.SetString(j + "Name", PlayerPrefs.GetString((j - 1) + "Name"));
                                    PlayerPrefs.SetInt(j + "Time", PlayerPrefs.GetInt((j - 1) + "Time"));
                                    PlayerPrefs.SetString(j + "Avility1", PlayerPrefs.GetString((j - 1) + "Avility1"));
                                    PlayerPrefs.SetString(j + "Avility2", PlayerPrefs.GetString((j - 1) + "Avility2"));
                                    PlayerPrefs.SetString(j + "Avility3", PlayerPrefs.GetString((j - 1) + "Avility3"));
                                }
                            }
                            break;
                        default:
                            break;
                    }

                    // �̸� ����
                    PlayerPrefs.SetString(i + "Name", nickname_text.text.ToString());
                    // Ŭ����ð� ����
                    PlayerPrefs.SetInt(i + "Time", GameData.clearTime.Minute * 60 + GameData.clearTime.Second);
                    // �����ߴ� �ɷ� ����
                    PlayerPrefs.SetString(i + "Avility1", avility1);
                    PlayerPrefs.SetString(i + "Avility2", avility2);
                    PlayerPrefs.SetString(i + "Avility3", avility3);
                    isRecord = true;
                }
                else
                    continue;
            }
            else
            {
                // �̸� ����
                PlayerPrefs.SetString(i + "Name", nickname_text.text.ToString());
                // Ŭ����ð� ����
                PlayerPrefs.SetInt(i + "Time", GameData.clearTime.Minute * 60 + GameData.clearTime.Second);
                // �����ߴ� �ɷ� ����
                PlayerPrefs.SetString(i + "Avility1", avility1);
                PlayerPrefs.SetString(i + "Avility2", avility2);
                PlayerPrefs.SetString(i + "Avility3", avility3);
                isRecord = true;
            }
            if(isRecord == true)
                break;
        }
        PlayerPrefs.DeleteKey("avility1");
        PlayerPrefs.DeleteKey("avility2");
        PlayerPrefs.DeleteKey("avility3");
        GameData.currStage = 1;
        GameData.clearTime = new DateTime(0);
        GameData.elapsed = 0;
        GameData.Damage = 1;
        GameData.speed = 2.5f;
        GameData.HP = -1;
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
            avility1 = "attack";
        }
        else if (PlayerPrefs.GetString("avility1") == "hp")
        {
            image1.sprite = avilities[1];
            avility1 = "hp";
        }
        else if (PlayerPrefs.GetString("avility1") == "speed")
        {
            image1.sprite = avilities[2];
            avility1 = "speed";
        }

        // �����Ƽ 2�� ã�� �ֱ�
        if (PlayerPrefs.GetString("avility2") == "attack")
        {
            image2.sprite = avilities[0];
            avility2 = "attack";
        }
        else if (PlayerPrefs.GetString("avility2") == "hp")
        {
            image2.sprite = avilities[1];
            avility2 = "hp";
        }
        else if (PlayerPrefs.GetString("avility2") == "speed")
        {
            image2.sprite = avilities[2];
            avility2 = "speed";
        }

        // �����Ƽ 3�� ã�� �ֱ�
        if (PlayerPrefs.GetString("avility3") == "attack")
        {
            image3.sprite = avilities[0];
            avility3 = "attack";
        }
        else if (PlayerPrefs.GetString("avility3") == "hp")
        {
            image3.sprite = avilities[1];
            avility3 = "hp";
        }
        else if (PlayerPrefs.GetString("avility3") == "speed")
        {
            image3.sprite = avilities[2];
            avility3 = "speed";
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
