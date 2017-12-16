using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentData : ItemData {

    float mpRecovery;

    public float MpRecovery
    {
        get
        {
            return mpRecovery;
        }

        set
        {
            mpRecovery = value;
        }
    }

    public EquipmentData(int _id, string _name, ItemType _type, ItemQuality _quality, string _description, int _capaticy, string _iconName, string _atlasName, float _mpRecovery)
        : base(_id,_name,_type,_quality,_description,_capaticy, _iconName,_atlasName)
    {
        MpRecovery = _mpRecovery;
    }

    public override string GetToolTipText()
    {
        string text = base.GetToolTipText();
        string newText = string.Format("{0}\n<color=white><size=10>{1}</size></color>", text, MpRecovery);
        return newText;
    }
}
