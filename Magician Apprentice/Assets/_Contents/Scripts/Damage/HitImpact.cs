using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 处理伤害修正，冲击修正， 受击效果
/// </summary>
[Serializable]
public class HitImpact {

    [Range(0f, 1000f)]
    [SerializeField]
    private float damageMax = 15f;

    public float GetDamage()
    {
        return damageMax;
    }
}
