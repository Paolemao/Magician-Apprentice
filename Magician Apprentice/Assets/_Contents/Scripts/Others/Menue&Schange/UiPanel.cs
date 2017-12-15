using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiPanel<T> : MonoBehaviour where T:Component {

    static GameObject _ins = null;

    static GameObject Ins
    {
        get
        {
            if (_ins==null)
            {
                var t = Resources.FindObjectsOfTypeAll<T>();
                _ins = t[0].gameObject;
            }
            return _ins;
        }
    }

    public static void Show()
    {
        Ins.SetActive(true);
    }

    public static void Hide()
    {
        Ins.SetActive(false);
    }

    public static T Get()
    {
        if (_ins)
        {
            return _ins.GetComponent<T>();
        }
        return null;
    }
}
