using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTip : UiPanel<SkillTip> {

     static Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public static void  AnimStart()
    {
        anim.SetBool("SkillTip",true);
    }
}
