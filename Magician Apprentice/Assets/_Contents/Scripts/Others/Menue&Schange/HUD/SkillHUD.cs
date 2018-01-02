using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillHUD : Inventroy {

    //单例模式
    private static SkillHUD _instance;

    public static SkillHUD Instance
    {
        get
        {
            if (_instance ==null)
            {
                var t = Resources.FindObjectsOfTypeAll<SkillHUD>();
                _instance = t[0];
                _instance.Init();
            }

            return _instance;
        }

    }

    public Button OpenSpellbook;

    bool isOpen;

    Transform fireFinalSlot;
    Transform windFinalSlot;
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
    public override void Start()
    {
        base.Start();

        fireFinalSlot = transform.Find("FireFinalSlot");
        windFinalSlot = transform.Find("WindFinalSlot");
    }
    protected override void Update()
    {
        base.Update();

        HUDUpdate();
    }
    void HUDUpdate()
    {
        //获取火系技能id；

        int fireid = 0;
        int windid = 0;


        if (GameController.Instance.Player.equipedAssistWeapon==null)
        {
            fireid = 91000;
            windid = 92000;

        }
        else
        {
            var  spellbook = (Spellbook)GameController.Instance.Player.equipedAssistWeapon;
            if (fireid != spellbook.fireId)
            {
                if(fireFinalSlot.childCount>0)
                {

                    DestroyImmediate(fireFinalSlot.GetChild(0).gameObject);
                }
                fireid = spellbook.fireId;
            }

            if (windid != spellbook.windId)
            {

                if (windFinalSlot.childCount > 0)
                {
                    DestroyImmediate(windFinalSlot.GetChild(0).gameObject);
                }
                windid = spellbook.windId;
            }

        }
        //通过id获取item；
        var _itemFire = InventroyManager.Instance.GetItemById(fireid);
        var _itemWind = InventroyManager.Instance.GetItemById(windid);
        //指定存储item
        fireFinalSlot.GetComponent<HUDSkillSlot>().StoreItem(_itemFire);
        windFinalSlot.GetComponent<HUDSkillSlot>().StoreItem(_itemWind);


    }

    private void OnEnable()
    {
        OpenSpellbook.onClick.AddListener(Open);
    }
    private void OnDisable()
    {
        OpenSpellbook.onClick.RemoveListener(Open);
    }

    void Open()
    {
        if (!isOpen)
        {
            SpellBookPane.Show();
            OpenSpellbook.transform.Find("Open").gameObject.SetActive(true);
            isOpen = true;
        }
        else
        {
            SpellBookPane.Hide();
            OpenSpellbook.transform.Find("Open").gameObject.SetActive(false);
            isOpen = false;
        }
    }
}
