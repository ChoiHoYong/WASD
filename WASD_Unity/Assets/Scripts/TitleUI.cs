using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//1. 눌렀을때 처리 방식은 Ui 이벤트 방식을 사용할 수 있습니다.
//2. Input 클래스의 함수 또는 속성값을 사용할 수 있습니다.
public class TitleUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.anyKeyDown)
        {
            // 씬을 이동해주는 함수입니다.
            SceneManager.LoadScene("TownScene");
            //SceneManager.LoadSceneAsync("TownScene"); 동기
        }
    }
}
