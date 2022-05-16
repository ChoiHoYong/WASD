using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnCard : MonoBehaviour
{
    public Sprite spriteCardFront;
    public Sprite spriteCardBack;

    static bool isFront = true; 
    static float speed = 4.0f;

    static bool choice = false;

    RectTransform rectTransform;
    Image cardImage;
    public Sprite[] cards;//스프라이트 배열 생성

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        cardImage = GetComponent<Image>();
        
    }

    private void Start()
    {
        //spriteCardFront = GetComponent<Sprite>();
        
        if (isFront)
        {
            cardImage.sprite = spriteCardFront;
        }
        
        else
        {
            cardImage.sprite = spriteCardBack;
        }
        
    }

    public void StartTurn()
    {
       
            StartCoroutine(Turn());
    }

    IEnumerator Turn()
    {
        float tick = 0f;

        Vector3 startScale = new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 endScale = new Vector3(0f, 1.0f, 1.0f);

        Vector3 localScale = new Vector3();


        while (tick < 1.0f)
        {
            tick += Time.deltaTime * speed;

            localScale = Vector3.Lerp(startScale, endScale, tick);

            rectTransform.localScale = localScale;

            yield return null;
        }

        if (choice == false)
        {
            cardImage.sprite = cards[Random.Range(0, 3)];//0~2번째 배열에 들어있는 사진으로 바꾸기
            //cardImage.sprite = spriteCardBack;
            choice = true;
        }
        isFront = !isFront;

        tick = 0f;

        while (tick < 1.0f)
        {
            tick += Time.deltaTime * speed;

            localScale = Vector3.Lerp(endScale, startScale, tick);

            rectTransform.localScale = localScale;

            yield return null;
        }
    }
}