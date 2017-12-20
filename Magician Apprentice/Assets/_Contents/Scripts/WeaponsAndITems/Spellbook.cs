using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellbook : Weapons {

    public float maxMpUp=50;

    public float MpRecovery=1f;

    public bool affected = false;
    [HideInInspector]
    public int fireId=91000;

    

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
         FireId();

    }
    void FireId()
    {
        if (SpellBookPack.Instance.transform.Find("FireSlot"))
        {
            var f = SpellBookPack.Instance.transform.Find("FireSlot");
            if (f.transform.Find("FinalSlot").childCount>0)
            {
                var fire = (SkillAndRueData)f.transform.Find("FinalSlot").GetChild(0).GetComponent<ItemUI>().Item;
                fireId = fire.Id;
            }
        }
    }
    void LightingRuneAdd()
    {
        LightingSlota = new List<string>();
        LightingSlota.Add(rune);
    }

}

