using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunePack : Inventroy {

    //单例模式
    private static RunePack _instance;

    public static RunePack Instance
    {
        get
        {
            if (_instance == null)
            {
                var t = Resources.FindObjectsOfTypeAll<RunePack>();
                _instance = t[0];
                _instance.Init();
            }
            return _instance;
        }
    }
    private void Init()
    {
        slotArray = transform.GetComponentsInChildren<Slot>();
    }
    //显示方法
    public static void Show()
    {
        Instance.gameObject.SetActive(true);
    }

    //隐藏方法
    public static void Hide()
    {
        Instance.gameObject.SetActive(false);
    }
}
