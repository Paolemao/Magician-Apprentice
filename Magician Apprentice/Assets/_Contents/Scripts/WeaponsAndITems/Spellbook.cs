0.using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellbook : Weapons {

    public float maxMpUp=50;

    public float MpRecovery=1f;

    public bool affected = false;
    [HideInInspector]
    public int key;

    

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

        LightingSlota = null;


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
        var fire = (SkillAndRueData)SpellBookPack.Instance.transform.Find("FinalSclot").GetChild(0).GetComponent<ItemUI>().Item;
        int fireKey = fire.Key;
    }
    void LightingRuneAdd()
    {
        LightingSlota = new List<string>();
        LightingSlota.Add(rune);
    }

}

