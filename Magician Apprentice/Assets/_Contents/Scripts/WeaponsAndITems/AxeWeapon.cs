using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeWeapon : MeleeWeapon
{


    //每分钟的攻击次数
    [SerializeField]
    private int shootPerMinute = 60;

    private float timeBetweenShootMin;
    private float nextTimeCanShoot;
    float _nextTimeCanShoot;

    //武器特效
    public GameObject impactParticle;
    public GameObject projectileParticle;



    protected override void Start()
    {
        base.Start();
        type = Weapontype.Ranged;
        timeBetweenShootMin = 60f/shootPerMinute;
        nextTimeCanShoot =1;
        _nextTimeCanShoot = 1;
    }

    public override void OnAttack()
    {
        //先播动画

        if (Time.time <User.time+nextTimeCanShoot)
        {
            return;
        }

        nextTimeCanShoot += timeBetweenShootMin;

        User.gameObject.GetComponent<Animator>().SetTrigger("Shoot");

        

        //ShootSpear();



    }
    public void ShootSpear()
    {
        Debug.Log("+++++++++++++++++++++++");
        var _user= (Ai_SpearEnemy)User;
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().AddForce((User.transform.forward* _user.forwardPower + User.transform.up* 2f),ForceMode.Impulse);
  
        User.axeCount -= 1;

        //特效
        projectileParticle = Instantiate(projectileParticle, transform.position,Quaternion.identity);
        projectileParticle.transform.parent = transform;

        transform.parent = null;
        User.equipedAssistWeapon = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy")
        {

            StartCoroutine(Des());   
        }
    }


    public void RangTakeTime()
    {
        //var Unuser = (Ai_SpearEnemy)User;

        Debug.Log(User.time);
        if (Time.time < User.time + nextTimeCanShoot+1.3f)
        {

            return;
        }

        nextTimeCanShoot += timeBetweenShootMin;

        User.RangedTake = true;

    }

    IEnumerator Des()
    {

        gameObject.GetComponent<BoxCollider>().isTrigger = false;
       
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }


}
