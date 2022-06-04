using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    void MoveToLoadingScene()
    {
        SceneManager.LoadScene("LoadingScene");
    }
    public void ChangeScene()
    {
        Fade.Instance.FadeOut();
        Invoke("MoveToLoadingScene", 1.0f);
    }

    void MoveToMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void ChangeMain()
    {
        Fade.Instance.FadeOut();
        Invoke("MoveToMainScene", 1.0f);
    }
}
