using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 背包类，继承自存活类
/// </summary>
public class Knapsack : Inventroy {

    //单例模式
    private static Knapsack _instance;

    public static Knapsack Instance
    {
        get
        {
            if (_instance == null)
            {
                //_instance = GameObject.Find("KnapscakPanel").GetComponent<Knapsack>();
                var t = Resources.FindObjectsOfTypeAll<Knapsack>();
                _instance = t[0];
                _instance.Init();
            }
            return _instance;
        }
    }
    private void Init()
    {
        var _knapsack = transform.Find("Knapsack");
        slotArray = _knapsack.GetComponentsInChildren<Slot>();
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
