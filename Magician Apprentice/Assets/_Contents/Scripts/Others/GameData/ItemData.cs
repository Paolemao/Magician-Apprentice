using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品基类
/// </summary>
public class ItemData {


    //物品的一般属性
    int id;
    string name;
    ItemType type;
    ItemQuality quality;
    string description;
    int capaticy;
    string iconName;
    string atlasName;

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }

        set
        {
            description = value;
        }
    }

    public string IconName
    {
        get
        {
            return iconName;
        }

        set
        {
            iconName = value;
        }
    }

    public string AtlasName
    {
        get
        {
            return atlasName;
        }

        set
        {
            atlasName = value;
        }
    }

    public ItemType Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }

    public ItemQuality Quality
    {
        get
        {
            return quality;
        }

        set
        {
            quality = value;
        }
    }

    public int Capaticy
    {
        get
        {
            return capaticy;
        }

        set
        {
            capaticy = value;
        }
    }

    public ItemData()
    {
        this.Id = -1;//表示这是一个空的物品类
    }

    public ItemData(int _id,string _name,ItemType _type,ItemQuality _quality, string _description, int _capaticy, string _iconName,string _atlasName )
    {
        this.Id = _id;
        this.Name = _name;
        this.Type = _type;
        this.Quality = quality;
        this.Description = description;
        this.Capaticy = _capaticy;
        this.IconName = _iconName;
        this.AtlasName = _atlasName;
    }

    //得到提示框应该显示的内容
    public virtual string GetToolTipText()
    {
        //类型
        string strItemType = "";
        switch (Type)
        {
            case ItemType.Weapon:
                strItemType = "武器";
                break;
            case ItemType.Consumable:
                strItemType = "消耗品";
                break;
            case ItemType.Equipment:
                strItemType = "装备";
                break;
        }
        //品质和颜色
        string strItemQuality = "";
        string color = "";
        switch (Quality)
        {
            case ItemQuality.Common:
                strItemQuality = "一般";
                color = "green";//绿色
                break;
            case ItemQuality.UnCommon:
                strItemQuality = "罕见";
                color = "blue";//蓝色
                break;
            case ItemQuality.Epic:
                strItemQuality = "史诗";
                color = "orange";//橙色
                break;
        }

        //格式
        string text = string.Format("<color={0}>{1}</color>\n" +
            "<color=yellow><size=12>说明:{2}</size></color>\n" +
            "<color=white><size=10>容量：{3}\n" +
            "物品类型：{4}\n" +
            "物品质量：{5}</size></color>",color,Description,Capaticy,Type,Quality);
        return text;
    }

}
/// <summary>
/// 物品类型
/// </summary>
public enum ItemType
{
    Weapon,//武器
    Equipment,//装备
    Consumable//消耗品
}

/// <summary>
/// 物品品质
/// </summary>
public enum ItemQuality
{
    Common,//一般
    UnCommon,//罕见
    Epic//史诗
}
