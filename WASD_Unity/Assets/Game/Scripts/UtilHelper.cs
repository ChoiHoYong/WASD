using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilHelper
{
    public static T Find<T> (Transform t, string path, bool active = true) where T: Component
    {
        Transform fObj = t.Find(path);
        if(fObj != null)
        {
            fObj.gameObject.SetActive(active);
            return fObj.GetComponent<T>();
        }
        return null;
    }
}
