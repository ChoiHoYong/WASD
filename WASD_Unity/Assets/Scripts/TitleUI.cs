using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//1. �������� ó�� ����� Ui �̺�Ʈ ����� ����� �� �ֽ��ϴ�.
//2. Input Ŭ������ �Լ� �Ǵ� �Ӽ����� ����� �� �ֽ��ϴ�.
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
            // ���� �̵����ִ� �Լ��Դϴ�.
            SceneManager.LoadScene("TownScene");
            //SceneManager.LoadSceneAsync("TownScene"); ����
        }
    }
}
