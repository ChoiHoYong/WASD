using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ui컴포넌트를 사용하기 위해서 추가하였습니다.
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    private static Image background;
    private static Fade instance = null;
    public static Fade Instance
    {
        get { return instance; }
    }
    // Start is called before the first frame update
    public void Init()
    {
        background = GetComponentInChildren<Image>(true);
        instance = this;
    }

    public IEnumerator IChangeColor(Color start, Color end, float targetTime = 1.0f)
    {
        float elapsedTime = 0;
        background.color = start;
        while(true)
        {
            elapsedTime += Time.deltaTime / targetTime;
            Color color = Color.Lerp(start, end, elapsedTime);
            background.color = color;
            if (elapsedTime >= 1.0f)
                break;
            yield return null;
        }
    }

    private void ChangeColor(Color start, Color end, float targetTime)
    {
        StartCoroutine(IChangeColor(start, end, targetTime));
    }

    public void FadeIn(float targetTime = 1.0f)
    {
        ChangeColor(Color.black, new Color(0, 0, 0, 0), targetTime);
    }
    public void FadeOut(float targetTime = 1.0f)
    {
        ChangeColor(new Color(0, 0, 0, 0), Color.black, targetTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
