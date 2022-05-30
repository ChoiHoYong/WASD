using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvilityManager : MonoBehaviour
{
    [SerializeField]
    public Sprite[] avilities = new Sprite[10];

    private GameObject Icon1;
    private GameObject Icon2;
    private GameObject Icon3;

    private Sprite image1;
    private Sprite image2;
    private Sprite image3;

    void Start()
    {
        avility_choice();
    }

    
    void Update()
    {
        
    }

    public void avility_choice()
    {
        //아이콘 지정.
        Icon1 = GameObject.Find("Avility_list_icon").transform.GetChild(0).gameObject;
        Icon2 = GameObject.Find("Avility_list_icon").transform.GetChild(1).gameObject;
        Icon3 = GameObject.Find("Avility_list_icon").transform.GetChild(2).gameObject;

        //이미지 지정.
        image1 = Icon1.GetComponent<Sprite>();
        image2 = Icon2.GetComponent<Sprite>();
        image3 = Icon3.GetComponent<Sprite>();

        if (GameData.avility_list[0] == "Attack")        
            image1 = avilities[0];       
        else if (GameData.avility_list[0] == "Hp")
            image1 = avilities[1];
        else if (GameData.avility_list[0] == "Speed")
            image1 = avilities[2];

        //if (GameData.avility_list[1] == "Attack")
        //    image2 = avilities[0];
        //else if (GameData.avility_list[1] == "Hp")
        //    image2 = avilities[1];
        //else if (GameData.avility_list[1] == "Speed")
        //    image2 = avilities[2];

    }
}
