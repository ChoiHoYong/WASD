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

    StageManager stageManager;

    RectTransform rectTransform;
    Image cardImage;
    public Sprite[] cards;//��������Ʈ �迭 ����

    public int randomtemp = -1;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        cardImage = GetComponent<Image>();
        stageManager = GetComponent<StageManager>();
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

    public void IsTurn()
    {
        if(choice == true)
        {
            //3�� �ִٰ� ���� �ٲ�.
            Invoke("SceneChange", 2);
            choice = false;
        }
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
            //���� �ɷ� ����
            randomtemp = Random.Range(0, 3);
            //0~2��° �迭�� ����ִ� �������� �ٲٱ�
            cardImage.sprite = cards[randomtemp];
            //�ɷ� ����Ʈ�� �ְ�, �ɷ� ����
            switch (randomtemp)
            {
                case 0:
                    GameData.avility_list.Add("Attack");
                    ++GameData.Damage;
                    Debug.Log("���� ����");
                    break;
                case 1:
                    GameData.avility_list.Add("Hp");
                    Debug.Log("ü�� ����");
                    ++GameData.HP;
                    break;
                case 2:
                    GameData.avility_list.Add("Speed");
                    Debug.Log("�̼� ����");
                    ++GameData.speed;
                    break;
                default:
                    break;
            }
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