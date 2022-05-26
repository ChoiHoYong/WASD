using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    //카드 선택했는지
    bool IsChoice = false;
    //private GameObject Card;

    //public void CardChoice()
    //{
    //    Card = GameObject.Find("Card").transform.GetChild(0).gameObject;
    //    Card.SetActive(true);
    //}

    public void Carrrrrd()
    {
        //커서 잠금을 푼다.
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
