using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndUI02 : UiPanel<EndUI02> {

    static Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public static void AnimStart()
    {
        anim.SetBool("End", true);
    }
}
