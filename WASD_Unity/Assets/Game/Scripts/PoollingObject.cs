using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoollingObject : MonoBehaviour
{
    public void OnDeactive()
    {
        gameObject.SetActive(false);
    }

    public void OnActive()
    {
        gameObject.SetActive(true);
    }

    public void DeactiveDelay(float delayTime)
    {
        Invoke("OnDeactive", delayTime);
    }
}
