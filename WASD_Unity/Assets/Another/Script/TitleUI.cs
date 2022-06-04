using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//1. �������� ó�� ����� Ui �̺�Ʈ ����� ����� �� �ֽ��ϴ�.
//2. Input Ŭ������ �Լ� �Ǵ� �Ӽ����� ����� �� �ֽ��ϴ�.
public class TitleUI : MonoBehaviour
{
    void SceneChange()
    {
        SceneManager.LoadScene("MainScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        // Ÿ��Ʋ ȭ���� ������ �ڿ� �Ű��� �����Դϴ�.
        Fade fade = GameObject.FindObjectOfType<Fade>();
        if (fade == null)
        {
            fade = Instantiate(Resources.Load<Fade>("Prefabs/Fade"));
            fade.Init();
            // ���� ����Ǵ��� �ı����� �ʵ��� ó���մϴ�.
            DontDestroyOnLoad(fade.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if( Input.anyKeyDown)
        {
            // ���� �̵����ִ� �Լ��Դϴ�.
            Fade.Instance.FadeOut();
            Invoke("SceneChange", 1.0f);
            //SceneManager.LoadSceneAsync("TownScene"); ����
        }
    }
}
