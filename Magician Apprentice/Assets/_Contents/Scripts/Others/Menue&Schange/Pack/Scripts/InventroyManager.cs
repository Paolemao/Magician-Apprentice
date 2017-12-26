using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// 存货总管理
/// </summary>
public class InventroyManager : MonoBehaviour {
    //单例模式
    private static InventroyManager _instance;

    public static InventroyManager Instance
    {
        get
        {
            if (_instance==null)
            {
                _instance = GameObject.Find("InventroyManager").GetComponent<InventroyManager>();
            }
            return _instance;
        }
    }

    public ItemUI PickedItem
    {
        get
        {
            return pickedItem;
        }
    }

    public bool IsPickedItem
    {
        get
        {
            return isPickedItem;
        }
    }

    private List<ItemData> itemList;//存储json解析出来的物品列表

    private ToolTip toolTip;//获取ToolTip脚本，方便对其管理
    private bool isToolTipShow = false;//提示框是否在显示状态
    private Canvas canvas;
    private Vector2 toolTipOffset = new Vector2(15,-10);//设置提示框跟随时与鼠标的偏移


    private ItemUI pickedItem;//鼠标选中的物品的脚本组件，用于制作拖动功能
    private bool isPickedItem = false;//鼠标是否选中该物品

    private void Awake()
    {
        ParseItemJson();
    }

    private void Start()
    {
        toolTip = GameObject.FindObjectOfType<ToolTip>();//根据类型获取
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        pickedItem = GameObject.Find("PickedItem").GetComponent<ItemUI>();
        pickedItem.Hide();
    }
    private void Update()
    {
        if (isToolTipShow == true && isPickedItem == false)//控制提示框跟随鼠标移动
        {
            Vector2 positonToolTip;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out positonToolTip);
            toolTip.SetLocalPosition(positonToolTip);//设置提示框位置，二维坐标自动转化为三维坐标
        }
        else if (IsPickedItem == true)//控制盛放物品的容器UI跟随鼠标移动
        {
            Vector2 positonPickedItem;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null,out positonPickedItem);
            pickedItem.SetLocalPosition(positonPickedItem);
        }
        if (isPickedItem==true&&Input.GetMouseButtonDown(0)&&UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1)==false)
            //表示鼠标是否在UI上
        {
            isPickedItem = false;
            pickedItem.Hide();
        }
    }

    ///<summary>
    ///解析Json文件
    ///</summary>
    public void ParseItemJson()
    {
        itemList = new List<ItemData>();

        //文本在unity里时TextAsset类型
        TextAsset itemText = Resources.Load<TextAsset>("GameData/"+"ItemData");//加载Json文件
        string itemJson = itemText.text;//得到Json文件里的文本内容

        JSONObject j = new JSONObject(itemJson);
        foreach (var temp in j.list)
        {
            //物品类型字符串转化为枚举类型
            ItemType type = (ItemType)System.Enum.Parse(typeof(ItemType), temp["type"].str);
            //print(type);
            //下面解析的时物品的共有属性：id,name,等
            int id = (int)(temp["id"].n);
            string name = temp["name"].str;
            ItemQuality quality = (ItemQuality)System.Enum.Parse(typeof(ItemQuality),temp["quality"].str);
            string description = temp["description"].str;
            int capacity =(int)(temp["capacity"].n);
            string iconName = temp["iconName"].str;
            string atlasName = temp["atlasName"].str;
            ItemData item = null;
            switch (type)
            {
                case ItemType.Consumable:
                    int maxHpPlus = (int)(temp["maxHpPlus"].n);
                    int minHpPlus = (int)(temp["minHpPlus"].n);
                    int maxMpPlus = (int)(temp["maxMpPlus"].n);
                    int minMpPlus = (int)(temp["minMpPlus"].n);
                    item = new ConsumableData(id, name, type, quality, description, capacity,iconName,atlasName,maxHpPlus,minHpPlus,maxMpPlus,minMpPlus);
                    break;
                case ItemType.Weapon:
                    float mpRecovery = (float)(temp["mpRecovery"].n);
                    item = new WeaponsData(id, name, type, quality, description, capacity, iconName, atlasName, mpRecovery);
                    break;
                case ItemType.Equipment:
                    mpRecovery = (float)(temp["mpRecovery"].n);
                    item = new EquipmentData(id, name, type, quality, description, capacity, iconName, atlasName,mpRecovery);
                    break;
                case ItemType.Skill:
                    int key = (int)(temp["key"].n);
                    item = new SkillAndRueData(id, name, type, quality, description, capacity, iconName, atlasName, key);
                    break;
                case ItemType.Rune:
                    key = (int)(temp["key"].n);
                    item = new SkillAndRueData(id, name, type, quality, description, capacity, iconName, atlasName,key);
                    break;
            }
            itemList.Add(item);//把解析到的物品信息加入物品列表里面
        }
    }

    //根据id得到item
    public ItemData GetItemById(int id)
    {
        foreach (ItemData item in itemList)
        {
            if (item.Id==id)
            {
                return item;
            }
        }
        return null;
    }
    //根据key值获取id
    public int GetIdByKey(int key)
    {
        int id = 90000 + key;
        return id;
    }

    //显示提示框方法
    public void ShowToolTip(string content)
    {
        if (this.isPickedItem == true) return;//如果物品槽中的物品被捡起了，那就不需要再显示提示框了
        toolTip.Show(content);
        isToolTipShow = true;
    }
    //隐藏提示框方法
    public void HideToolTip()
    {
        toolTip.Hide();
        isToolTipShow = false;
    }

    //获取物品槽里的指定数量(amount)物品UI
    public void PickUpItem(ItemData item,int amount)
    {
        PickedItem.SetItem(item,amount);
        this.isPickedItem = true;
        PickedItem.Show();//获取物品之后把跟随鼠标的容器显示出来
        this.toolTip.Hide();//同时隐藏物品信息提示框

        //控制盛放物品的容器UI跟随鼠标移动
        Vector2 postionPickeItem;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out postionPickeItem);
        pickedItem.SetLocalPosition(postionPickeItem);//设置容器的位置，二维坐标会自动转化为三维坐标但Z坐标为0
    }

    //从鼠标上减少（移除）指定数量的物品
    public void ReduceAmountItem(int amount = 1)
    {
        this.pickedItem.RemoveItemAmount(amount);
        if (pickedItem.Amount <= 0)
        {
            isPickedItem = false;
            PickedItem.Hide();//如果鼠标上没有物品了那就隐藏了
        }
    }

    //点击保存按钮，保存当前物品信息
    public void SaveInventory()
    {
        Knapsack.Instance.SaveInventory();
        //LootChest.Instance.SaveInventory();
        //CharacterPanel.Instance.SaveInventory();
    }

    //点击加载按钮，加载当前物品
    public void LoadInventory()
    {
        Knapsack.Instance.LoadInventory();
        LootChest.Instance.LoadInventory();
        //CharacterPanel.Instance.LoadInventory();
        //Forge.Instance.LoadInventory();
        ////加载玩家金币
        //if (PlayerPrefs.HasKey("CoinAmount") == true)
        //{
        //    GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CoinAmount = PlayerPrefs.GetInt("CoinAmount");
        //}
    }

}
