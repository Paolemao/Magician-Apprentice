using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeWeapon : MeleeWeapon
{


    //每分钟的攻击次数
    [SerializeField]
    private int shootPerMinute = 30;

    private float timeBetweenShootMin;
    private float nextTimeCanShoot=0;

    protected override void Start()
    {
        base.Start();
        type = Weapontype.Ranged;
        timeBetweenShootMin = 60f/shootPerMinute;
    }

    public override void OnAttack()
    {
        if (Time.time < nextTimeCanShoot)
        {
            return;
        }

        nextTimeCanShoot = Time.time + timeBetweenShootMin;
        gameObject.AddComponent<Rigidbody>();
        ShootSpear();


    }
    public void ShootSpear()
    {

        gameObject.GetComponent<Rigidbody>().AddForce(0, 5f, 100f);
        User.gameObject.GetComponent<Animator>().SetTrigger("Shoot");
        Debug.Log("*****************");
        User.axeCount -= 1;
    }


}
