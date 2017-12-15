using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : Weapons {

    public float maxMpUp = 50;

    public float MpRecovery = 1f;

    public bool affected = false;

    public override void OnEquip()
    {
        base.OnEquip();

        User.maxMp += maxMpUp;
        User.mpRecovery += MpRecovery;
        

    }

    public override void OnUnEquip()
    {
        base.OnUnEquip();
        User.maxMp -= maxMpUp;
        User.mpRecovery -= MpRecovery;
    }


}
