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
        if( fade == null )
        {
            fade = Instantiate(Resources.Load<Fade>("Prefabs/Fade"));
            fade.Init();
            // 신이 변경되더라도 파괴되지 않도록 처리합니다.
            DontDestroyOnLoad(fade.gameObject);
        }
        //PlayerPrefs.DeleteAll();
    }

    void Update()
    {

    }

    void Rank()
    {
        if (PlayerPrefs.HasKey("1Time"))
        {
            string name1 = PlayerPrefs.GetString("1Name");
            int time1 = PlayerPrefs.GetInt("1Time");
            string avility1_1 = PlayerPrefs.GetString("1Avility1");
            string avility1_2 = PlayerPrefs.GetString("1Avility2");
            string avility1_3 = PlayerPrefs.GetString("1Avility3");
            rank_list1.Add(name1);
            rank_list1.Add(time1 / 60 + " : " + time1 % 60);
            rank_list1.Add(avility1_1);
            rank_list1.Add(avility1_2);
            rank_list1.Add(avility1_3);
        }
        else
        {
            rank_list1.Add("asd");
            rank_list1.Add("03:00");
            rank_list1.Add("speed");
            rank_list1.Add("attack");
            rank_list1.Add("hp");
        }
        if (PlayerPrefs.HasKey("2Time"))
        {
            string name2 = PlayerPrefs.GetString("2Name");
            int time2 = PlayerPrefs.GetInt("2Time");
            string avility2_1 = PlayerPrefs.GetString("2Avility1");
            string avility2_2 = PlayerPrefs.GetString("2Avility2");
            string avility2_3 = PlayerPrefs.GetString("2Avility3");

            rank_list2.Add(name2);
            rank_list2.Add(time2 / 60 + " : " + time2 % 60);
            rank_list2.Add(avility2_1);
            rank_list2.Add(avility2_2);
            rank_list2.Add(avility2_3);
        }
        //int_list2.Add(02);
        //int_list2.Add(00);
        else
        {
            rank_list2.Add("asd");
            rank_list2.Add("03:00");
            rank_list2.Add("speed");
            rank_list2.Add("attack");
            rank_list2.Add("hp");
        }
        rank_list3.Add("asd");
        rank_list3.Add("03:00");
        rank_list3.Add("speed");
        rank_list3.Add("attack");
        rank_list3.Add("hp");
        //int_list3.Add(03);
        //int_list3.Add(00);

        rank_list4.Add("qwer");
        rank_list4.Add("04:00");
        rank_list4.Add("hp");
        rank_list4.Add("hp");
        rank_list4.Add("speed");
        //int_list4.Add(04);
        //int_list4.Add(00);

        rank_list5.Add("Hog");
        rank_list5.Add("05:00");
        rank_list5.Add("attack");
        rank_list5.Add("hp");
        rank_list5.Add("speed");
        //int_list5.Add(05);
        //int_list5.Add(00);

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
        rank_temp.Add(PlayerPrefs.GetString("NickName"));
        rank_temp.Add(PlayerPrefs.GetString("Time"));
        rank_temp.Add(PlayerPrefs.GetString("avility1"));
        rank_temp.Add(PlayerPrefs.GetString("avility2"));
        rank_temp.Add(PlayerPrefs.GetString("avility3"));
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
        switch (nrank)
        {
            case 1:
                rank_list1[0] = rank_temp[0];
                rank_list1[1] = rank_temp[1];
                rank_list1[2] = rank_temp[2];
                rank_list1[3] = rank_temp[3];
                rank_list1[4] = rank_temp[4];
                int_list1[0] = ntime1;
                int_list1[1] = ntime2;
                break;
            case 2:
                rank_list2[0] = rank_temp[0];
                rank_list2[1] = rank_temp[1];
                rank_list2[2] = rank_temp[2];
                rank_list2[3] = rank_temp[3];
                rank_list2[4] = rank_temp[4];
                int_list2[0] = ntime1;
                int_list2[1] = ntime2;
                break;
            case 3:
                rank_list3[0] = rank_temp[0];
                rank_list3[1] = rank_temp[1];
                rank_list3[2] = rank_temp[2];
                rank_list3[3] = rank_temp[3];
                rank_list3[4] = rank_temp[4];
                int_list3[0] = ntime1;
                int_list3[1] = ntime2;
                break;
            case 4:
                break;
            case 5:
                break;
            default:
                break;
        }
    }

    void Rank_invite()
    {
        

        //1순위 넣기
        nickname_go = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(1).transform.GetChild(2).gameObject;
        nickname_text = nickname_go.GetComponent<Text>();
        nickname_text.text = rank_list1[0];

        time_go = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(1).transform.GetChild(3).gameObject;
        time_text = time_go.GetComponent<Text>();
        time_text.text = rank_list1[1];

        avility_go1 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(1).transform.GetChild(4).transform.GetChild(0).gameObject;
        avility_img1 = avility_go1.GetComponent<Image>();
        
        if (rank_list1[2] == "attack")
            avility_img1.sprite = avilities[0];
        else if (rank_list1[2] == "hp")
            avility_img1.sprite = avilities[1];
        else if (rank_list1[2] == "speed")
            avility_img1.sprite = avilities[2];

        avility_go2 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(1).transform.GetChild(4).transform.GetChild(1).gameObject;
        avility_img2 = avility_go2.GetComponent<Image>();

        if (rank_list1[3] == "attack")
            avility_img2.sprite = avilities[0];
        else if (rank_list1[3] == "hp")
            avility_img2.sprite = avilities[1];
        else if (rank_list1[3] == "speed")
            avility_img2.sprite = avilities[2];

        avility_go3 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(1).transform.GetChild(4).transform.GetChild(2).gameObject;
        avility_img3 = avility_go3.GetComponent<Image>();

        if (rank_list1[4] == "attack")
            avility_img3.sprite = avilities[0];
        else if (rank_list1[4] == "hp")
            avility_img3.sprite = avilities[1];
        else if (rank_list1[4] == "speed")
            avility_img3.sprite = avilities[2];

        //2순위 넣기
        nickname_go = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(2).transform.GetChild(2).gameObject;
        nickname_text = nickname_go.GetComponent<Text>();
        nickname_text.text = rank_list2[0];

        time_go = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(2).transform.GetChild(3).gameObject;
        time_text = time_go.GetComponent<Text>();
        time_text.text = rank_list2[1];

        avility_go1 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(2).transform.GetChild(4).transform.GetChild(0).gameObject;
        avility_img1 = avility_go1.GetComponent<Image>();

        if (rank_list2[2] == "attack")
            avility_img1.sprite = avilities[0];
        else if (rank_list2[2] == "hp")
            avility_img1.sprite = avilities[1];
        else if (rank_list2[2] == "speed")
            avility_img1.sprite = avilities[2];

        avility_go2 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(2).transform.GetChild(4).transform.GetChild(1).gameObject;
        avility_img2 = avility_go2.GetComponent<Image>();

        if (rank_list2[3] == "attack")
            avility_img2.sprite = avilities[0];
        else if (rank_list2[3] == "hp")
            avility_img2.sprite = avilities[1];
        else if (rank_list2[3] == "speed")
            avility_img2.sprite = avilities[2];

        avility_go3 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(2).transform.GetChild(4).transform.GetChild(2).gameObject;
        avility_img3 = avility_go3.GetComponent<Image>();

        if (rank_list2[4] == "attack")
            avility_img3.sprite = avilities[0];
        else if (rank_list2[4] == "hp")
            avility_img3.sprite = avilities[1];
        else if (rank_list2[4] == "speed")
            avility_img3.sprite = avilities[2];

        //3순위 넣기
        nickname_go = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(3).transform.GetChild(2).gameObject;
        nickname_text = nickname_go.GetComponent<Text>();
        nickname_text.text = rank_list3[0];

        time_go = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(3).transform.GetChild(3).gameObject;
        time_text = time_go.GetComponent<Text>();
        time_text.text = rank_list3[1];

        avility_go1 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(3).transform.GetChild(4).transform.GetChild(0).gameObject;
        avility_img1 = avility_go1.GetComponent<Image>();

        if (rank_list3[2] == "attack")
            avility_img1.sprite = avilities[0];
        else if (rank_list3[2] == "hp")
            avility_img1.sprite = avilities[1];
        else if (rank_list3[2] == "speed")
            avility_img1.sprite = avilities[2];

        avility_go2 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(3).transform.GetChild(4).transform.GetChild(1).gameObject;
        avility_img2 = avility_go2.GetComponent<Image>();

        if (rank_list3[3] == "attack")
            avility_img2.sprite = avilities[0];
        else if (rank_list3[3] == "hp")
            avility_img2.sprite = avilities[1];
        else if (rank_list3[3] == "speed")
            avility_img2.sprite = avilities[2];

        avility_go3 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(3).transform.GetChild(4).transform.GetChild(2).gameObject;
        avility_img3 = avility_go3.GetComponent<Image>();

        if (rank_list3[4] == "attack")
            avility_img3.sprite = avilities[0];
        else if (rank_list3[4] == "hp")
            avility_img3.sprite = avilities[1];
        else if (rank_list3[4] == "speed")
            avility_img3.sprite = avilities[2];

        //4순위 넣기
        nickname_go = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(4).transform.GetChild(2).gameObject;
        nickname_text = nickname_go.GetComponent<Text>();
        nickname_text.text = rank_list4[0];

        time_go = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(4).transform.GetChild(3).gameObject;
        time_text = time_go.GetComponent<Text>();
        time_text.text = rank_list4[1];

        avility_go1 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(4).transform.GetChild(4).transform.GetChild(0).gameObject;
        avility_img1 = avility_go1.GetComponent<Image>();

        if (rank_list4[2] == "attack")
            avility_img1.sprite = avilities[0];
        else if (rank_list4[2] == "hp")
            avility_img1.sprite = avilities[1];
        else if (rank_list4[2] == "speed")
            avility_img1.sprite = avilities[2];

        avility_go2 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(4).transform.GetChild(4).transform.GetChild(1).gameObject;
        avility_img2 = avility_go2.GetComponent<Image>();

        if (rank_list4[3] == "attack")
            avility_img2.sprite = avilities[0];
        else if (rank_list4[3] == "hp")
            avility_img2.sprite = avilities[1];
        else if (rank_list4[3] == "speed")
            avility_img2.sprite = avilities[2];

        avility_go3 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(4).transform.GetChild(4).transform.GetChild(2).gameObject;
        avility_img3 = avility_go3.GetComponent<Image>();

        if (rank_list4[4] == "attack")
            avility_img3.sprite = avilities[0];
        else if (rank_list4[4] == "hp")
            avility_img3.sprite = avilities[1];
        else if (rank_list4[4] == "speed")
            avility_img3.sprite = avilities[2];

        //5순위 넣기
        nickname_go = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(5).transform.GetChild(2).gameObject;
        nickname_text = nickname_go.GetComponent<Text>();
        nickname_text.text = rank_list5[0];

        time_go = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(5).transform.GetChild(3).gameObject;
        time_text = time_go.GetComponent<Text>();
        time_text.text = rank_list5[1];

        avility_go1 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(5).transform.GetChild(4).transform.GetChild(0).gameObject;
        avility_img1 = avility_go1.GetComponent<Image>();

        if (rank_list5[2] == "attack")
            avility_img1.sprite = avilities[0];
        else if (rank_list5[2] == "hp")
            avility_img1.sprite = avilities[1];
        else if (rank_list5[2] == "speed")
            avility_img1.sprite = avilities[2];

        avility_go2 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(5).transform.GetChild(4).transform.GetChild(1).gameObject;
        avility_img2 = avility_go2.GetComponent<Image>();

        if (rank_list5[3] == "attack")
            avility_img2.sprite = avilities[0];
        else if (rank_list5[3] == "hp")
            avility_img2.sprite = avilities[1];
        else if (rank_list5[3] == "speed")
            avility_img2.sprite = avilities[2];

        avility_go3 = GameObject.Find("Rank").transform.GetChild(0).transform.GetChild(5).transform.GetChild(4).transform.GetChild(2).gameObject;
        avility_img3 = avility_go3.GetComponent<Image>();

        if (rank_list5[4] == "attack")
            avility_img3.sprite = avilities[0];
        else if (rank_list5[4] == "hp")
            avility_img3.sprite = avilities[1];
        else if (rank_list5[4] == "speed")
            avility_img3.sprite = avilities[2];

        




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