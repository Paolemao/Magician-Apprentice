using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 消耗品类
/// </summary>

public class ConsumableData : ItemData
{
    int maxHpPlus;
    int minHpPlus;
    int maxMpPlus;
    int minMpPlus;

    public int MaxHpPlus
    {
        get
        {
            return maxHpPlus;
        }

        set
        {
            maxHpPlus = value;
        }
    }

    public int MinHpPlus
    {
        get
        {
            return minHpPlus;
        }

        set
        {
            minHpPlus = value;
        }
    }

    public int MaxMpPlus
    {
        get
        {
            return maxMpPlus;
        }

        set
        {
            maxMpPlus = value;
        }
    }

    public int MinMpPlus
    {
        get
        {
            return minMpPlus;
        }

        set
        {
            minMpPlus = value;
        }
    }

    public ConsumableData(int _id, string _name, ItemType _type, ItemQuality _quality, string _description, int _capaticy, string _iconName, string _atlasName,
        int _maxHpPlus,int _minHpPlus,int _maxMpPlus,int _minMpPlus)
        :base(_id, _name, _type, _quality, _description, _capaticy,  _iconName,_atlasName)
    {
        MaxHpPlus = _maxHpPlus;
        MinHpPlus = _minHpPlus;
        maxMpPlus = _maxMpPlus;
        MinMpPlus = _minMpPlus;
    }

    //对父类的GetToolTipText()重写
    public override string GetToolTipText()
    {
        string text = base.GetToolTipText();
        string newText = string.Format("{0}\n" +
            "<color=white><size=10>最大回血:{1}HP\n" +
            "最小回血:{2}\n" +
            "最大回蓝:{3}\n" +
            "最小回蓝:{4}</size></color>\n",text,MaxMpPlus,minHpPlus,MaxMpPlus,MinMpPlus);
        return newText;
    }

    public override string ToString()
    {
        string str = "";
        str += Id;
        str += Name;
        str += Type;
        str += Quality;
        str += Description;
        str += Capaticy;
        str += maxHpPlus;
        str += minHpPlus;
        str += MaxMpPlus;
        str += minMpPlus;
        str += IconName;
        str += AtlasName;

        return str;
    }

}
