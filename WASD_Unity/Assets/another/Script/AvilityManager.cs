using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvilityManager : MonoBehaviour
{
    private GameObject Icon1;
    private GameObject Icon2;
    private GameObject Icon3;

    //private Image image1;
    //private Image image2;
    //private Image image3;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void avility_choice()
    {
        //������ ����.
        Icon1 = GameObject.Find("Avility_list_icon").transform.GetChild(0).gameObject;
        Icon2 = GameObject.Find("Avility_list_icon").transform.GetChild(1).gameObject;
        Icon3 = GameObject.Find("Avility_list_icon").transform.GetChild(2).gameObject;

        //�̹��� ����.
        
    }
}
