using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellbook : Weapons {


    public float MpRecovery=1f;

    public bool affected = false;
    [HideInInspector]
    public int fireId=91000;

    [HideInInspector]
    public int windId = 92000;
    


    //符文容器

    public string rune;

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

    private void Update()
    {
        FireId();
        WindId();

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
    void WindId()
    {
        if (SpellBookPack.Instance.transform.Find("WindSlot"))
        {
            var f = SpellBookPack.Instance.transform.Find("WindSlot");
            if (f.transform.Find("FinalSlot").childCount > 0)
            {
                var wind = (SkillAndRueData)f.transform.Find("FinalSlot").GetChild(0).GetComponent<ItemUI>().Item;
                windId = wind.Id;
            }
        }
    }

}

