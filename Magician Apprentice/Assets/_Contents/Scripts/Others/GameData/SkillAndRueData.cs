using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAndRueData : ItemData {

    int key;

    public int Key
    {
        get
        {
            return key;
        }

        set
        {
            key = value;
        }
    }

    public SkillAndRueData(int _id, string _name, ItemType _type, ItemQuality _quality, string _description, int _capaticy, string _iconName, string _atlasName, int _key)
        : base(_id,_name,_type,_quality,_description,_capaticy, _iconName,_atlasName)
    {
        Key = _key;
    }

    public override string GetToolTipText()
    {
        string text = base.GetToolTipText();
        string newText = string.Format("{0}\n<color=white><size=10>键：{1}</size></color>", text, Key);
        return newText;
    }
}
