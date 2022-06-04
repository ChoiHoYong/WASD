using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//1. 눌렀을때 처리 방식은 Ui 이벤트 방식을 사용할 수 있습니다.
//2. Input 클래스의 함수 또는 속성값을 사용할 수 있습니다.
public class TitleUI : MonoBehaviour
{
    void SceneChange()
    {
        SceneManager.LoadScene("MainScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        // 타이틀 화면을 구성한 뒤에 옮겨질 내용입니다.
        Fade fade = GameObject.FindObjectOfType<Fade>();
        if (fade == null)
        {
            fade = Instantiate(Resources.Load<Fade>("Prefabs/Fade"));
            fade.Init();
            // 신이 변경되더라도 파괴되지 않도록 처리합니다.
            DontDestroyOnLoad(fade.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if( Input.anyKeyDown)
        {
            // 씬을 이동해주는 함수입니다.
            Fade.Instance.FadeOut();
            Invoke("SceneChange", 1.0f);
            //SceneManager.LoadSceneAsync("TownScene"); 동기
        }
    }
}
