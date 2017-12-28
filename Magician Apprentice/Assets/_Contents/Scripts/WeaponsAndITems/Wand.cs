using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : Weapons {

    public float MpRecovery = 1f;

    public bool affected = false;

    public override void OnEquip()
    {
        base.OnEquip();

        User.mpRecovery += MpRecovery;
        

    }

    public override void OnUnEquip()
    {
        base.OnUnEquip();

        User.mpRecovery -= MpRecovery;
    }


}
