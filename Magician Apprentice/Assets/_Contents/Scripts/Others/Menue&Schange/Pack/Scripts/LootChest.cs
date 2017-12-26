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
    public Button close;

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
        if (!GameController.Instance.Player.HasTaked)//当玩家还没和NPC谈话时是无法打开宝箱的
        {
            PlayerDialogBox.Show();
            PlayerDialogBox.Instance.DialogTip(70201);
            return;
        }

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
        close.onClick.AddListener(Hide);
    }
    private void OnDisable()
    {
        PickUpAll.onClick.RemoveListener(PickUp);
        close.onClick.RemoveListener(Hide);
    }
    void PickUp()
    {
        foreach (Slot sl in slotArray)
        {
            if (sl.transform.childCount>0)
            {
                ItemUI itemUI= sl.transform.GetChild(0).GetComponent<ItemUI>();
                if (itemUI.Item.Type == ItemType.Rune)
                {
                    RunePack.Instance.StoreItem(sl.GetItemID());
                }
                else
                {
                    for (int i = 0; i < itemUI.Amount; i++)
                    {
                        Knapsack.Instance.StoreItem(sl.GetItemID());
                    }
                }
                DestroyImmediate(sl.transform.GetChild(0).gameObject);
            }
        }

        GameController.Instance.Player.HasTook = true;//角色的状态为已拿取宝箱物体
    }
}
