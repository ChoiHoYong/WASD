using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// T ��� ������ Ÿ���� TSingleton<T> ���κ��� �Ļ��� (��ӹ���) Ŭ������ �����ϴٴ� �������Դϴ�.
public class TSingleton<T> : MonoBehaviour where T : TSingleton<T>
{
    private static T instance;

    public static T Instance
    {

        get
        {
            if(instance == null)
            {
                GameObject newObject = new GameObject(typeof(T).Name,typeof(T));
                DontDestroyOnLoad(newObject);

                instance = newObject.GetComponent<T>();
            }
            return instance;
        }
    }
    public virtual void Init()
    {

    }
}
