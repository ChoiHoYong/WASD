using System;

using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    //����ȭ�� ����� ���̱�
    GameObject audio;

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
        //����ȭ�� ����� ���̱�
        audio = GameObject.Find("GameAudioManager");
        if (audio != null)
            Destroy(audio);

        Rank_invite();

        // Ÿ��Ʋ ȭ���� ������ �ڿ� �Ű��� �����Դϴ�.
        Fade fade = GameObject.FindObjectOfType<Fade>();
        if (fade == null)
        {
            fade = Instantiate(Resources.Load<Fade>("Prefabs/Fade"));
            fade.Init();
            // ���� ����Ǵ��� �ı����� �ʵ��� ó���մϴ�.
            DontDestroyOnLoad(fade.gameObject);
        }
        //PlayerPrefs.DeleteAll();
        Fade.Instance.FadeIn();
    }
    void Rank_invite()
    {
        if (PlayerPrefs.HasKey("1Time"))
        {

            //1���� �ֱ�
            nickname_go = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(1).transform.GetChild(2).gameObject;
            nickname_text = nickname_go.GetComponent<Text>();
            nickname_text.text = PlayerPrefs.GetString("1Name");

            time_go = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(1).transform.GetChild(3).gameObject;
            time_text = time_go.GetComponent<Text>();
            int time1 = PlayerPrefs.GetInt("1Time");
            time_text.text = (time1 / 60 + " : " + time1 % 60);
            string avility1_1 = PlayerPrefs.GetString("1Avility1");
            string avility1_2 = PlayerPrefs.GetString("1Avility2");
            string avility1_3 = PlayerPrefs.GetString("1Avility3");

            avility_go1 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(1).transform.GetChild(4).transform.GetChild(0).gameObject;
            avility_img1 = avility_go1.GetComponent<Image>();

            if (avility1_1 == "attack")
                avility_img1.sprite = avilities[0];
            else if (avility1_1 == "hp")
                avility_img1.sprite = avilities[1];
            else if (avility1_1 == "speed")
                avility_img1.sprite = avilities[2];

            avility_go2 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(1).transform.GetChild(4).transform.GetChild(1).gameObject;
            avility_img2 = avility_go2.GetComponent<Image>();

            if (avility1_2 == "attack")
                avility_img2.sprite = avilities[0];
            else if (avility1_2 == "hp")
                avility_img2.sprite = avilities[1];
            else if (avility1_2 == "speed")
                avility_img2.sprite = avilities[2];

            avility_go3 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(1).transform.GetChild(4).transform.GetChild(2).gameObject;
            avility_img3 = avility_go3.GetComponent<Image>();

            if (avility1_3 == "attack")
                avility_img3.sprite = avilities[0];
            else if (avility1_3 == "hp")
                avility_img3.sprite = avilities[1];
            else if (avility1_3 == "speed")
                avility_img3.sprite = avilities[2];
        }

        if (PlayerPrefs.HasKey("2Time"))
        {
            //2���� �ֱ�
            nickname_go = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(2).transform.GetChild(2).gameObject;
            nickname_text = nickname_go.GetComponent<Text>();
            nickname_text.text = PlayerPrefs.GetString("2Name");

            time_go = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(2).transform.GetChild(3).gameObject;
            time_text = time_go.GetComponent<Text>();
            int time2 = PlayerPrefs.GetInt("2Time");
            time_text.text = (time2 / 60 + " : " + time2 % 60);
            string avility2_1 = PlayerPrefs.GetString("2Avility1");
            string avility2_2 = PlayerPrefs.GetString("2Avility2");
            string avility2_3 = PlayerPrefs.GetString("2Avility3");

            avility_go1 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(2).transform.GetChild(4).transform.GetChild(0).gameObject;
            avility_img1 = avility_go1.GetComponent<Image>();

            if (avility2_1 == "attack")
                avility_img1.sprite = avilities[0];
            else if (avility2_1 == "hp")
                avility_img1.sprite = avilities[1];
            else if (avility2_1 == "speed")
                avility_img1.sprite = avilities[2];

            avility_go2 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(2).transform.GetChild(4).transform.GetChild(1).gameObject;
            avility_img2 = avility_go2.GetComponent<Image>();

            if (avility2_2 == "attack")
                avility_img2.sprite = avilities[0];
            else if (avility2_2 == "hp")
                avility_img2.sprite = avilities[1];
            else if (avility2_2 == "speed")
                avility_img2.sprite = avilities[2];

            avility_go3 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(2).transform.GetChild(4).transform.GetChild(2).gameObject;
            avility_img3 = avility_go3.GetComponent<Image>();

            if (avility2_3 == "attack")
                avility_img3.sprite = avilities[0];
            else if (avility2_3 == "hp")
                avility_img3.sprite = avilities[1];
            else if (avility2_3 == "speed")
                avility_img3.sprite = avilities[2];
        }

        if (PlayerPrefs.HasKey("3Time"))
        {
            //3���� �ֱ�
            nickname_go = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(3).transform.GetChild(2).gameObject;
            nickname_text = nickname_go.GetComponent<Text>();
            nickname_text.text = PlayerPrefs.GetString("3Name");

            time_go = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(3).transform.GetChild(3).gameObject;
            time_text = time_go.GetComponent<Text>();
            int time3 = PlayerPrefs.GetInt("3Time");
            time_text.text = (time3 / 60 + " : " + time3 % 60);
            string avility3_1 = PlayerPrefs.GetString("3Avility1");
            string avility3_2 = PlayerPrefs.GetString("3Avility2");
            string avility3_3 = PlayerPrefs.GetString("3Avility3");

            avility_go1 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(3).transform.GetChild(4).transform.GetChild(0).gameObject;
            avility_img1 = avility_go1.GetComponent<Image>();

            if (avility3_1 == "attack")
                avility_img1.sprite = avilities[0];
            else if (avility3_1 == "hp")
                avility_img1.sprite = avilities[1];
            else if (avility3_1 == "speed")
                avility_img1.sprite = avilities[2];

            avility_go2 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(3).transform.GetChild(4).transform.GetChild(1).gameObject;
            avility_img2 = avility_go2.GetComponent<Image>();

            if (avility3_2 == "attack")
                avility_img2.sprite = avilities[0];
            else if (avility3_2 == "hp")
                avility_img2.sprite = avilities[1];
            else if (avility3_2 == "speed")
                avility_img2.sprite = avilities[2];

            avility_go3 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(3).transform.GetChild(4).transform.GetChild(2).gameObject;
            avility_img3 = avility_go3.GetComponent<Image>();

            if (avility3_3 == "attack")
                avility_img3.sprite = avilities[0];
            else if (avility3_3 == "hp")
                avility_img3.sprite = avilities[1];
            else if (avility3_3 == "speed")
                avility_img3.sprite = avilities[2];
        }

        if (PlayerPrefs.HasKey("4Time"))
        {
            //4���� �ֱ�
            nickname_go = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(4).transform.GetChild(2).gameObject;
            nickname_text = nickname_go.GetComponent<Text>();
            nickname_text.text = PlayerPrefs.GetString("4Name");

            time_go = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(4).transform.GetChild(3).gameObject;
            time_text = time_go.GetComponent<Text>();
            int time4 = PlayerPrefs.GetInt("4Time");
            time_text.text = (time4 / 60 + " : " + time4 % 60);
            string avility4_1 = PlayerPrefs.GetString("4Avility1");
            string avility4_2 = PlayerPrefs.GetString("4Avility2");
            string avility4_3 = PlayerPrefs.GetString("4Avility3");

            avility_go1 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(4).transform.GetChild(4).transform.GetChild(0).gameObject;
            avility_img1 = avility_go1.GetComponent<Image>();

            if (avility4_1 == "attack")
                avility_img1.sprite = avilities[0];
            else if (avility4_1 == "hp")
                avility_img1.sprite = avilities[1];
            else if (avility4_1 == "speed")
                avility_img1.sprite = avilities[2];

            avility_go2 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(4).transform.GetChild(4).transform.GetChild(1).gameObject;
            avility_img2 = avility_go2.GetComponent<Image>();

            if (avility4_2 == "attack")
                avility_img2.sprite = avilities[0];
            else if (avility4_2 == "hp")
                avility_img2.sprite = avilities[1];
            else if (avility4_2 == "speed")
                avility_img2.sprite = avilities[2];

            avility_go3 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(4).transform.GetChild(4).transform.GetChild(2).gameObject;
            avility_img3 = avility_go3.GetComponent<Image>();

            if (avility4_3 == "attack")
                avility_img3.sprite = avilities[0];
            else if (avility4_3 == "hp")
                avility_img3.sprite = avilities[1];
            else if (avility4_3 == "speed")
                avility_img3.sprite = avilities[2];
        }

        if (PlayerPrefs.HasKey("5Time"))
        {
            //5���� �ֱ�
            nickname_go = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(5).transform.GetChild(2).gameObject;
            nickname_text = nickname_go.GetComponent<Text>();
            nickname_text.text = PlayerPrefs.GetString("5Name");

            time_go = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(5).transform.GetChild(3).gameObject;
            time_text = time_go.GetComponent<Text>();
            int time5 = PlayerPrefs.GetInt("5Time");
            time_text.text = (time5 / 60 + " : " + time5 % 60);
            string avility5_1 = PlayerPrefs.GetString("5Avility1");
            string avility5_2 = PlayerPrefs.GetString("5Avility2");
            string avility5_3 = PlayerPrefs.GetString("5Avility3");

            avility_go1 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(5).transform.GetChild(4).transform.GetChild(0).gameObject;
            avility_img1 = avility_go1.GetComponent<Image>();

            if (avility5_1 == "attack")
                avility_img1.sprite = avilities[0];
            else if (avility5_1 == "hp")
                avility_img1.sprite = avilities[1];
            else if (avility5_1 == "speed")
                avility_img1.sprite = avilities[2];

            avility_go2 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(5).transform.GetChild(4).transform.GetChild(1).gameObject;
            avility_img2 = avility_go2.GetComponent<Image>();

            if (avility5_2 == "attack")
                avility_img2.sprite = avilities[0];
            else if (avility5_2 == "hp")
                avility_img2.sprite = avilities[1];
            else if (avility5_2 == "speed")
                avility_img2.sprite = avilities[2];

            avility_go3 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(5).transform.GetChild(4).transform.GetChild(2).gameObject;
            avility_img3 = avility_go3.GetComponent<Image>();

            if (avility5_3 == "attack")
                avility_img3.sprite = avilities[0];
            else if (avility5_3 == "hp")
                avility_img3.sprite = avilities[1];
            else if (avility5_3 == "speed")
                avility_img3.sprite = avilities[2];
        }
    }
}