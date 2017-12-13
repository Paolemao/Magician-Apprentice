using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapontype
{
    Mellee,
    
    Ranged
}

public class Weapons : MonoBehaviour {

    public Transform leftHandIk;
    public Transform rightHandIk;

    [HideInInspector]
    public Weapontype type;

    [SerializeField] //处理伤害冲击修正
    protected HitImpact hitImpact;

        //时间
    public float time;
    bool getTime;

    private Character _user;

    public Character User
    {
        //实例化_user
        get
        {
            if (_user==null)
            {
                _user = GetComponent<Character>();
            }
            if (!_user)
            {
                _user = GetComponentInParent<Character>();
            }
            return _user;
        }
    }

    public bool isEquiped { get; private set;}

    public virtual void OnEquip()
    {
        isEquiped = true;
    }
    public virtual void OnUnEquip()
    {

        isEquiped = false;
    }
    public virtual void OnAttack()
    {

    }

}
