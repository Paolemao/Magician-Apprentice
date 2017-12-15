using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//继承于Monobehaviour的单例
public class SingletonBehaviour<T> : MonoBehaviour where T:Component {

    private static T _instance;

    public  static T Instance
    {
        get
        {
            if (_instance==null)
            {
                //在场景中找到T的实例   
                _instance = FindObjectOfType<T>();
                if (_instance==null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }

    }
    public virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);
        name = "_" + typeof(T).Name;
        if (_instance == null)
        {
            _instance = this as T;
        }
        else
        {
            //保证只有单例存在
            Destroy(gameObject);
        }
    }
}
