using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownUI : MonoBehaviour
{
    delegate void Func();

    Func func;
    public void OnClickStart()
    {
        print("��ư Ŭ��");
    }
    // Start is called before the first frame update
    void Start()
    {
        // �Լ��� ȣ���ϴ� �ΰ��� ���
        //func = OnClickStart;
        //1.func();
        //if(func != null)
        //{
        //2.func.Invoke();
        //}

        //func += OnClickStart;
        //func += Test;
        //func.Invoke(); ���� �ΰ��� ����ȣ��

/*        Transform t = transform.Find("Start");
        if(t != null)
        {
            Button btn = t.GetComponent<Button>();
            btn.onClick.AddListener(OnClickStart);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
