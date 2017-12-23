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
    public Button windButton;

    ItemData FireBaseItem;
    ItemData WindBaseItem;

    Transform fireSlot;
    Transform windSlot;

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
        WindBaseItem = InventroyManager.Instance.GetItemById(92000);

        fireSlot = transform.Find("FireSlot");
        windSlot = transform.Find("WindSlot");

        if (fireSlot.Find("BaseSlot"))
        {
            fireSlot.Find("BaseSlot").GetComponent<SpellBookSlot>().StoreItem(FireBaseItem);
        }
        if (windSlot.Find("BaseSlot"))
        {
            windSlot.Find("BaseSlot").GetComponent<SpellBookSlot>().StoreItem(WindBaseItem);
        }
    }
    private void OnEnable()
    {
        fireButton.onClick.AddListener(LearnFireSkill);
        windButton.onClick.AddListener(LearnWindSkill);
    }
    private void OnDisable()
    {
        fireButton.onClick.RemoveListener(LearnFireSkill);
        windButton.onClick.RemoveListener(LearnWindSkill);
    }
    void LearnFireSkill()
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


        //火系技能
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
    void LearnWindSkill()
    {
        int redKey = 0;
        int blueKey = 0;

        if (windSlot.Find("FinalSlot"))
        {
            if (windSlot.Find("FinalSlot").childCount > 0)
            {
                DestroyImmediate(windSlot.Find("FinalSlot").GetChild(0).gameObject);
            }
        }

        if (windSlot.Find("RedSlot"))
        {
            Debug.Log("++");
            if (windSlot.Find("RedSlot").childCount > 0)
            {
                var redRune = (SkillAndRueData)windSlot.Find("RedSlot").GetChild(0).GetComponent<ItemUI>().Item;
                redKey = redRune.Key;
            }
            else
            {
                redKey = 0;
            }
        }

        //获取蓝色符文槽的key值
        if (windSlot.Find("BlueSlot"))
        {
            if (windSlot.Find("BlueSlot").childCount > 0)
            {
                var blueRune = (SkillAndRueData)windSlot.Find("BlueSlot").GetChild(0).GetComponent<ItemUI>().Item;
                blueKey = blueRune.Key;
            }
            else
            {
                blueKey = 0;
            }
        }


        //风系技能
        var WindRuneSkill = (SkillAndRueData)WindBaseItem;
        int Windkey = WindRuneSkill.Key + redKey + blueKey;
        int WindId = InventroyManager.Instance.GetIdByKey(Windkey);

        var WindItem = InventroyManager.Instance.GetItemById(WindId);
        if (windSlot.Find("FinalSlot"))
        {
            windSlot.Find("FinalSlot").GetComponent<SpellBookSlot>().StoreItem(WindItem);
        }


    }

}
