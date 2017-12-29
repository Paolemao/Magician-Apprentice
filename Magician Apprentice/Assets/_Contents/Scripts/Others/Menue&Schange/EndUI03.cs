using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndUI03 : UiPanel<EndUI03> {

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
