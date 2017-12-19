using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBookPack : Inventroy {

    //单例模式
    private static SpellBookPack _instance;

    public static SpellBookPack Instance
    {
        get
        {
            if (_instance == null)
            {
                var t = Resources.FindObjectsOfTypeAll<SpellBookPack>();
                _instance = t[0];
                _instance.Init();
            }
            return _instance;
        }
    }

    public Button fireButton;
    ItemData FireBaseItem;
    Transform fireSlot;

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
        FireBaseItem = InventroyManager.Instance.GetItemById(91000);
        fireSlot = transform.Find("FireSlot");
        if (fireSlot.Find("BaseSlot"))
        {
            fireSlot.Find("BaseSlot").GetComponent<SpellBookSlot>().StoreItem(FireBaseItem);
        }
    }
    private void OnEnable()
    {
        fireButton.onClick.AddListener(LearnSkill);
    }
    private void OnDisable()
    {
        fireButton.onClick.RemoveListener(LearnSkill);
    }
    void LearnSkill()
    {
        int redKey=0;
        int blueKey=0;

        //清空槽
        if (fireSlot.Find("FinalSlot"))
        {
            if (fireSlot.Find("FinalSlot").childCount>0)
            {
                DestroyImmediate(fireSlot.Find("FinalSlot").GetChild(0).gameObject);
            }
        }

        //获取红色符文槽的key值
        // Debug.Log(fireSlot.GetChild(1));

        if (fireSlot.Find("RedSlot"))
        {
            Debug.Log("++");
            if (fireSlot.Find("RedSlot").childCount > 0)
            {
                var redRune = (SkillAndRueData)fireSlot.Find("RedSlot").GetChild(0).GetComponent<ItemUI>().Item;
                redKey = redRune.Key;
            }
            else
            {
                redKey = 0;
            }
        }
        //获取蓝色符文槽的key值
        if (fireSlot.Find("BlueSlot"))
        {
            if (fireSlot.Find("BlueSlot").childCount > 0)
            {
                var blueRune = (SkillAndRueData)fireSlot.Find("BlueSlot").GetChild(0).GetComponent<ItemUI>().Item;
                blueKey = blueRune.Key;
            }
            else
            {
                blueKey = 0;
            }
        }

        var runeSkill = (SkillAndRueData)FireBaseItem;
        int key = runeSkill.Key + redKey + blueKey;
        int id=InventroyManager.Instance.GetIdByKey(key);
        Debug.Log(id);
        var item = InventroyManager.Instance.GetItemById(id);
        if (fireSlot.Find("FinalSlot"))
        {
            fireSlot.Find("FinalSlot").GetComponent<SpellBookSlot>().StoreItem(item);
        }

    }

}
