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
        print("버튼 클릭");
    }
    // Start is called before the first frame update
    void Start()
    {
        // 함수를 호출하는 두가지 방법
        //func = OnClickStart;
        //1.func();
        //if(func != null)
        //{
        //2.func.Invoke();
        //}

        //func += OnClickStart;
        //func += Test;
        //func.Invoke(); 위의 두개를 같이호출

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
