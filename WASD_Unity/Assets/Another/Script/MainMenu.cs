using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    //public GameObject White_Border;

    public void OnClickSettings()
    {
        Debug.Log("WASD Settrings!!");
    }

    public void OnClickStart()
    {
        Debug.Log("WASD Start!!");
    }

    public void OnClickRank()
    {
        Debug.Log("WASD Rank!!");
        //White_Border.SetActive(true);
    }

    public void OnClickQuit()
    {
        Debug.Log("게임을 종료합니다.");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
