using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TempEnemy : Character {

    protected override void Start()
    {
        anim = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();

        healthPoint = maxHp;
        MagicPoint = maxMp;
    }
}
