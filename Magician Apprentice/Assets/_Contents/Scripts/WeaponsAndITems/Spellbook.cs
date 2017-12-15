using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellbook : Weapons {

    public float maxMpUp=50;

    public float MpRecovery=1f;

    public bool affected = false;
    [HideInInspector]
    public Rune[] FireSlota;

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
        FireSlota = new Rune[2];
        FireSlota[0] = Rune.UnRune;
        FireSlota[1] = Rune.UnRune;
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
        //FireSlota[0] = Rune.Move;
        //FireSlota[1] = Rune.Follow;
        FireSlota[0] = Rune.Restraint;
    }
    void LightingRuneAdd()
    {
        LightingSlota = new List<string>();
        LightingSlota.Add(rune);
    }

}

public enum Rune
{
    Move,//移动
    Restraint,//约束
    Follow,//跟踪
    Increase,//繁殖
    UnRune//无
}
