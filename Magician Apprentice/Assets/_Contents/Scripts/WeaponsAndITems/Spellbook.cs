using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellbook : Weapons {

    public float maxMpUp=50;

    public float MpRecovery=1f;

    public bool affected = false;
    [HideInInspector]
    public List<string> FireSlota;

    [HideInInspector]
    public List<string> LightingSlota;

    //符文容器

    public string rune;

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
    private void Start()
    {
        FireSlota = null;
        LightingSlota = null;
        rune = "Move";
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            FireRuneAdd();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LightingRuneAdd();
        }

    }
    void FireRuneAdd()
    {

        FireSlota = new List<string>();
        FireSlota.Add(rune);
    }
    void LightingRuneAdd()
    {
        LightingSlota = new List<string>();
        LightingSlota.Add(rune);
    }

}
