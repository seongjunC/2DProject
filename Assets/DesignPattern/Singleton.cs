using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T instance
    {
        get
        {
            if (instance == null)
            {
                _instance = FindObjectOfType<T>();
                DontDestroyOnLoad(_instance);
            }
            return instance;
        }
    }

    protected void SingletonInit()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this as T;
            DontDestroyOnLoad(_instance);
        }
    }

}
