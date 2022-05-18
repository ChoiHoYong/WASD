using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    private List<Animation> hearts = new List<Animation>();
    // Start is called before the first frame update
    public void Init()
    {
        for(int i = 0; i < transform.childCount; ++i)
        {
            Transform t = transform.GetChild(i);
            hearts.Add(t.GetComponent<Animation>());
        }
    }
    public void Play(int number, string animationName = "Heart")
    {
        if (number < hearts.Count)
            hearts[number].Play(animationName);
    }
}
