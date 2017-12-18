using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 战利品箱子类
/// </summary>
public class LootChest : Inventroy {

    //单例
    private static LootChest _instance;
    public Button PickUpAll;

    public static LootChest Instance
    {
        get
        {
            if (_instance == null)
            {
                var t = Resources.FindObjectsOfTypeAll<LootChest>();
                _instance = t[0];
                _instance.Init();
            }

            return _instance;
        }
    }

    private void Init()
    {
        var _lootChest = transform.Find("LootChest");
        slotArray = _lootChest.GetComponentsInChildren<Slot>();
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
    private void OnEnable()
    {
        PickUpAll.onClick.AddListener(PickUp);
    }
    private void OnDisable()
    {
        PickUpAll.onClick.RemoveListener(PickUp);
    }
    void PickUp()
    {
        foreach (Slot sl in slotArray)
        {
            if (sl.transform.childCount>0)
            {
                Knapsack.Instance.StoreItem(sl.GetItemID());
                DestroyImmediate(sl.transform.GetChild(0).gameObject);
            }
        }

    }
}
