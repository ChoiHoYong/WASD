using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour
{
    public Slider progressbar;
    public Text loadText;
    private void Start()
    {
        PlayerPrefs.DeleteAll();
        StartCoroutine(LoadScene());        
    }
    IEnumerator LoadScene()
    {
        yield return null;
        //�񵿽����� �ҷ��ɴϴ�.(�ٸ� ���� ���� �ҷ����µ���)
        AsyncOperation operation = SceneManager.LoadSceneAsync("Stage1");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;
            if(progressbar.value < 1f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
            }
            else
            {
                loadText.text = "Press Key";
            }
            if (Input.anyKeyDown && progressbar.value >= 1f && operation.progress >=0.9f)
            {
                operation.allowSceneActivation = true;
            }
        }
    }
}
