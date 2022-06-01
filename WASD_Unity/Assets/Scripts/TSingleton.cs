using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// T 라는 데이터 타입은 TSingleton<T> 으로부터 파생된 (상속받은) 클래스만 가능하다는 한정자입니다.
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
