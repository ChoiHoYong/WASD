using System;

using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI;

public class RankingManager : MonoBehaviour

{
    //Sprite 능력 모음
    [SerializeField]
    public Sprite[] avilities = new Sprite[3];

    

    //시간 선언
    private GameObject time_go;
    private Text time_text;

    //닉네임 선언
    private GameObject nickname_go;
    private Text nickname_text;

    //능력 선언
    private GameObject avility_go1;
    private Image avility_img1;
    private GameObject avility_go2;
    private Image avility_img2;
    private GameObject avility_go3;
    private Image avility_img3;

    //랭킹 관리 리스트
    private List<String> rank_list1 = new List<string>();
    private List<String> rank_list2 = new List<string>();
    private List<String> rank_list3 = new List<string>();
    private List<String> rank_list4 = new List<string>();
    private List<String> rank_list5 = new List<string>();
    private List<String> rank_temp = new List<string>();

    //템프 시간 합 계산 변수
    string stime = "";
    string[] stime_sp;
    int ntime1 = 0;
    int ntime2 = 0;

    //시발
    private List<int> int_list1 = new List<int>();
    private List<int> int_list2 = new List<int>();
    private List<int> int_list3 = new List<int>();
    private List<int> int_list4 = new List<int>();
    private List<int> int_list5 = new List<int>();
    private List<int> int_temp = new List<int>();

    int nrank = 0;

    void Start()
    {
        Rank();
        Rank_invite();

        // 타이틀 화면을 구성한 뒤에 옮겨질 내용입니다.
        Fade fade = GameObject.FindObjectOfType<Fade>();
        if (fade == null)
        {
            fade = Instantiate(Resources.Load<Fade>("Prefabs/Fade"));
            fade.Init();
            // 신이 변경되더라도 파괴되지 않도록 처리합니다.
            DontDestroyOnLoad(fade.gameObject);
        }
        //PlayerPrefs.DeleteAll();
        Fade.Instance.FadeIn();
    }

    void Update()
    {

    }

    void Rank()
    {
        //if (PlayerPrefs.HasKey("1Time"))
        //{
        //    string name1 = PlayerPrefs.GetString("1Name");
        //    int time1 = PlayerPrefs.GetInt("1Time");
        //    string avility1_1 = PlayerPrefs.GetString("1Avility1");
        //    string avility1_2 = PlayerPrefs.GetString("1Avility2");
        //    string avility1_3 = PlayerPrefs.GetString("1Avility3");
        //    rank_list1.Add(name1);
        //    rank_list1.Add(time1 / 60 + " : " + time1 % 60);
        //    rank_list1.Add(avility1_1);
        //    rank_list1.Add(avility1_2);
        //    rank_list1.Add(avility1_3);
        //}
        //else
        //{
        //    rank_list1.Add("asd");
        //    rank_list1.Add("03:00");
        //    rank_list1.Add("speed");
        //    rank_list1.Add("attack");
        //    rank_list1.Add("hp");
        //}
        //if (PlayerPrefs.HasKey("2Time"))
        //{
        //    string name2 = PlayerPrefs.GetString("2Name");
        //    int time2 = PlayerPrefs.GetInt("2Time");
        //    string avility2_1 = PlayerPrefs.GetString("2Avility1");
        //    string avility2_2 = PlayerPrefs.GetString("2Avility2");
        //    string avility2_3 = PlayerPrefs.GetString("2Avility3");

        //    rank_list2.Add(name2);
        //    rank_list2.Add(time2 / 60 + " : " + time2 % 60);
        //    rank_list2.Add(avility2_1);
        //    rank_list2.Add(avility2_2);
        //    rank_list2.Add(avility2_3);
        //}
        ////int_list2.Add(02);
        ////int_list2.Add(00);
        //else
        //{
        //    rank_list2.Add("asd");
        //    rank_list2.Add("03:00");
        //    rank_list2.Add("speed");
        //    rank_list2.Add("attack");
        //    rank_list2.Add("hp");
        //}
        //if (PlayerPrefs.HasKey("3Time"))
        //{
        //    string name3 = PlayerPrefs.GetString("3Name");
        //    int time3 = PlayerPrefs.GetInt("3Time");
        //    string avility3_1 = PlayerPrefs.GetString("3Avility1");
        //    string avility3_2 = PlayerPrefs.GetString("3Avility2");
        //    string avility3_3 = PlayerPrefs.GetString("3Avility3");

        //    rank_list2.Add(name3);
        //    rank_list2.Add(time3 / 60 + " : " + time3 % 60);
        //    rank_list2.Add(avility3_1);
        //    rank_list2.Add(avility3_2);
        //    rank_list2.Add(avility3_3);
        //}
        //else
        //{
        //    rank_list3.Add("asd");
        //    rank_list3.Add("03:00");
        //    rank_list3.Add("speed");
        //    rank_list3.Add("attack");
        //    rank_list3.Add("hp");
        //    //int_list3.Add(03);
        //    //int_list3.Add(00);
        //}
        //if (PlayerPrefs.HasKey("4Time"))
        //{
        //    string name4 = PlayerPrefs.GetString("4Name");
        //    int time4 = PlayerPrefs.GetInt("4Time");
        //    string avility4_1 = PlayerPrefs.GetString("4Avility1");
        //    string avility4_2 = PlayerPrefs.GetString("4Avility2");
        //    string avility4_3 = PlayerPrefs.GetString("4Avility3");

        //    rank_list2.Add(name4);
        //    rank_list2.Add(time4 / 60 + " : " + time4 % 60);
        //    rank_list2.Add(avility4_1);
        //    rank_list2.Add(avility4_2);
        //    rank_list2.Add(avility4_3);
        //}
        //else
        //{
        //    rank_list4.Add("qwer");
        //    rank_list4.Add("04:00");
        //    rank_list4.Add("hp");
        //    rank_list4.Add("hp");
        //    rank_list4.Add("speed");
        //    //int_list4.Add(04);
        //    //int_list4.Add(00);
        //}
        //if (PlayerPrefs.HasKey("5Time"))
        //{
        //    string name5 = PlayerPrefs.GetString("5Name");
        //    int time5 = PlayerPrefs.GetInt("5Time");
        //    string avility5_1 = PlayerPrefs.GetString("5Avility1");
        //    string avility5_2 = PlayerPrefs.GetString("5Avility2");
        //    string avility5_3 = PlayerPrefs.GetString("5Avility3");

        //    rank_list2.Add(name5);
        //    rank_list2.Add(time5 / 60 + " : " + time5 % 60);
        //    rank_list2.Add(avility5_1);
        //    rank_list2.Add(avility5_2);
        //    rank_list2.Add(avility5_3);

        //}
        //else
        //{
        //    rank_list5.Add("Hog");
        //    rank_list5.Add("05:00");
        //    rank_list5.Add("attack");
        //    rank_list5.Add("hp");
        //    rank_list5.Add("speed");
        //    //int_list5.Add(05);
        //    //int_list5.Add(00);
        //}
        //문자열 자르기 (시간)
        //stime = PlayerPrefs.GetString("Time");
        //stime_sp = stime.Split(":");
        //try
        //{
        //    ntime1 = Int32.Parse(stime_sp[0]);
        //}
        //catch (Exception Ex)
        //{
        //    //Display some message, that the conversion has failed.         
        //}
        //try
        //{
            
        //    ntime2 = Int32.Parse(stime_sp[1]);
        //}
        //catch (Exception Ex)
        //{
        //    //Display some message, that the conversion has failed.         
        //}


        //결과값 temp 담기
        //rank_temp.Add(PlayerPrefs.GetString("NickName"));
        //rank_temp.Add(PlayerPrefs.GetString("Time"));
        //rank_temp.Add(PlayerPrefs.GetString("avility1"));
        //rank_temp.Add(PlayerPrefs.GetString("avility2"));
        //rank_temp.Add(PlayerPrefs.GetString("avility3"));
        //int_temp.Add(ntime1);
        //int_temp.Add(ntime2);

        //temp 검사
        //if (ntime1 == int_list4[0]) // 4분대
        //{
        //    nrank = 5;
        //}
        //else if (ntime1 == int_list3[0]) //3분대
        //{
        //    nrank = 4;
        //}
        //else if (ntime1 == int_list2[0]) // 2분대
        //{
        //    nrank = 3;
        //}
        //else if (ntime1 == int_list1[0]) // 1분대
        //{
        //    nrank = 2;
        //}
        //else if (ntime1 < int_list1[0]) // 1분 미만
        //{
        //    nrank = 1;
        //}

        //temp 넣기
        //switch (nrank)
        //{
        //    case 1:
        //        rank_list1[0] = rank_temp[0];
        //        rank_list1[1] = rank_temp[1];
        //        rank_list1[2] = rank_temp[2];
        //        rank_list1[3] = rank_temp[3];
        //        rank_list1[4] = rank_temp[4];
        //        int_list1[0] = ntime1;
        //        int_list1[1] = ntime2;
        //        break;
        //    case 2:
        //        rank_list2[0] = rank_temp[0];
        //        rank_list2[1] = rank_temp[1];
        //        rank_list2[2] = rank_temp[2];
        //        rank_list2[3] = rank_temp[3];
        //        rank_list2[4] = rank_temp[4];
        //        int_list2[0] = ntime1;
        //        int_list2[1] = ntime2;
        //        break;
        //    case 3:
        //        rank_list3[0] = rank_temp[0];
        //        rank_list3[1] = rank_temp[1];
        //        rank_list3[2] = rank_temp[2];
        //        rank_list3[3] = rank_temp[3];
        //        rank_list3[4] = rank_temp[4];
        //        int_list3[0] = ntime1;
        //        int_list3[1] = ntime2;
        //        break;
        //    case 4:
        //        break;
        //    case 5:
        //        break;
        //    default:
        //        break;
        //}
    }

    void Rank_invite()
    {
        if (PlayerPrefs.HasKey("1Time"))
        {

            //1순위 넣기
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
            //2순위 넣기
            nickname_go = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(2).transform.GetChild(2).gameObject;
            nickname_text = nickname_go.GetComponent<Text>();
            nickname_text.text = PlayerPrefs.GetString("2Name");

            time_go = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(2).transform.GetChild(3).gameObject;
            time_text = time_go.GetComponent<Text>();
            int time2 = PlayerPrefs.GetInt("1Time");
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
            //3순위 넣기
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
            //4순위 넣기
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
            //5순위 넣기
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





        ////시간 가져오기
        //time_go = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(1).transform.GetChild(3).gameObject;
        //time_text = time_go.GetComponent<Text>();
        //time_text.text = PlayerPrefs.GetString("Time");

        ////닉네임 가져오기
        //nickname_go = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(1).transform.GetChild(2).gameObject;
        //nickname_text = nickname_go.GetComponent<Text>();
        //nickname_text.text = PlayerPrefs.GetString("NickName");

        ////능력 가져오기
        //avility_go1 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(1).transform.GetChild(4).transform.GetChild(0).gameObject;
        //avility_img1 = avility_go1.GetComponent<Image>();

        //if (PlayerPrefs.GetString("avility1") == "attack")
        //    avility_img1.sprite = avilities[0];
        //else if (PlayerPrefs.GetString("avility1") == "hp")
        //    avility_img1.sprite = avilities[1];
        //else if (PlayerPrefs.GetString("avility1") == "speed")
        //    avility_img1.sprite = avilities[2];

        //avility_go2 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(1).transform.GetChild(4).transform.GetChild(1).gameObject;
        //avility_img2 = avility_go2.GetComponent<Image>();

        //if (PlayerPrefs.GetString("avility2") == "attack")
        //    avility_img2.sprite = avilities[0];
        //else if (PlayerPrefs.GetString("avility2") == "hp")
        //    avility_img2.sprite = avilities[1];
        //else if (PlayerPrefs.GetString("avility2") == "speed")
        //    avility_img2.sprite = avilities[2];

        //avility_go3 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(1).transform.GetChild(4).transform.GetChild(2).gameObject;
        //avility_img3 = avility_go3.GetComponent<Image>();

        //if (PlayerPrefs.GetString("avility3") == "attack")
        //    avility_img3.sprite = avilities[0];
        //else if (PlayerPrefs.GetString("avility3") == "hp")
        //    avility_img3.sprite = avilities[1];
        //else if (PlayerPrefs.GetString("avility3") == "speed")
        //    avility_img3.sprite = avilities[2];
    }
}