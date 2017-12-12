using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour,IDamageable {

    //属性
    public float healthPoint = 0f;
    public float MagicPoint = 0f;

    public float maxHp = 100f;
    public float maxMp = 100f;
    public float hpRecovery = 0f;
    public float mpRecovery = 0f;

    [HideInInspector]
    public float currentMpRecoveryDelay;
    [HideInInspector]
    public float currentHpRecoveryDelay;

    //状态
    protected bool isGround = false;
    protected bool isDead = false;

    [Range(0.1f, 3f)]
    [SerializeField]
    protected float speed;

    [Range(1f, 1000f)]
    [SerializeField]
    protected float AngularSpeed;

    [SerializeField]
    protected float groundCheckDistance = 0.2f;


    //GameObject
    protected Rigidbody rigi;
    protected Animator anim;
    protected CapsuleCollider capsule;

    protected float turnAmount;
    protected float forwardAmount;
    protected float rightAmount;

    protected bool isAttacking;
    protected bool wind;

    protected Vector3 velocity;
    protected Vector3 moveRaw;
    protected Vector3 groundNormal;

    //武器和道具

    public GameObject ProfaberWeapon_melee;
    public GameObject ProfaberWeapon_ranged;

    [SerializeField]
    protected Weapons defaultAttackWeapon=null;
    [SerializeField]
    protected Weapons defaultAssistWeapon=null;

    public Weapons equipedAttackWeapon=null;

    public Weapons UnequipedAttackWeapon=null;

    public Weapons equipedAssistWeapon=null;


    [HideInInspector]
    public bool takingWeapon=false;
    [HideInInspector]
    public bool takedWeapon = false;
    [HideInInspector]
    public bool takingItem = false;
    [HideInInspector]
    public bool canTake = false;

    //装备槽
    public Transform[] armorSlots = new Transform[2];
    //飞斧容量
    [SerializeField]
    public int axeCount;
    // 道具槽
    public Transform ItemSlot;

    protected bool beHit;

    #region Cycle
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        
        rigi.drag = 5;

        healthPoint = maxHp;
        MagicPoint = maxMp;

        wind = false;

    }

    protected virtual void Update()
    {
        if (!isDead)
        {
            UpdateControl();
        }
        Recovery();
        UpdateMovement();
        UpdateAnimator();
    }
    protected virtual void FixedUpdate()
    {
        rigi.MovePosition(rigi.position+velocity*Time.fixedDeltaTime);
    }
    //获取控制
    protected virtual void UpdateControl() { }

    protected virtual void UpdateMovement()
    {
        transform.Rotate(0, turnAmount * AngularSpeed * Time.deltaTime, 0);
        velocity = transform.forward * forwardAmount * speed;
        velocity.y = rigi.velocity.y;
    }
    protected virtual void UpdateAnimator()
    {
        anim.SetFloat("Forward",forwardAmount);
        anim.SetFloat("Turn",turnAmount);
    }

    #endregion 

    //处理移动参数
    public virtual void Movement(Vector3 move)
    {
        //向量归1化
        if (move.magnitude > 1f)
        {
            move.Normalize();
        }
        //世界转本地
        move = transform.InverseTransformDirection(move);

        //将move投影到地板的2D平面上 （斜坡）
        move = Vector3.ProjectOnPlane(move,groundNormal);
        //X轴与2D向量的夹角
        turnAmount = Mathf.Atan2(move.x,move.z);
        rightAmount = move.x;
        forwardAmount = move.z;

    }
    #region Skills
    public virtual void ThunderSkill()
    {
        anim.SetTrigger("Thunder");
    }
    public virtual void FireSkill()
    {
        anim.SetTrigger("Fire");
    }
    public virtual void IceSkill()
    {
        anim.SetTrigger("Ice");
    }
    public virtual void WindSkill()
    {
        anim.SetBool("Wind",wind);
    }
    public virtual void Attacking(bool attacking)
    {

            isAttacking = attacking;
    }
    #endregion

    #region Interface 伤害
    public virtual void TakeDamage(DamageEventData damageData)
    {
        if (damageData == null) return;

        if (healthPoint > Mathf.Abs(damageData.delta) || damageData.delta > 0)
        {
            healthPoint += damageData.delta;
            if (healthPoint < 0)
            {
                healthPoint = 0;
            }
            beHit = true;
            anim.Play("Hit");
        }
        else
        {
            beHit = false;
            if (!isDead) Die();
        }
    }
    #endregion

    #region 影响
    void Recovery()
    {
        if (MagicPoint<maxMp)
        {
            MagicPoint += mpRecovery * Time.deltaTime;
        }
        if (healthPoint<maxHp)
        {
            healthPoint += hpRecovery * Time.deltaTime;
        }


    }
    #endregion

    public virtual void Die()
    {
        if (isDead) return;
        isDead = true;
        Movement(Vector3.zero);
        anim.Play("Die");
        capsule.height = 0.2f;
        capsule.center = new Vector3(0,0.3f,0);
    }

    public void TakeDamage()
    {
        throw new System.NotImplementedException();
    }

    public virtual void ChangeWeapon()
    {
        equipedAttackWeapon.OnUnEquip();
        var temp = equipedAttackWeapon;
        equipedAttackWeapon = UnequipedAttackWeapon;
        UnequipedAttackWeapon = temp;
        equipedAttackWeapon.gameObject.transform.parent = armorSlots[0];
        equipedAttackWeapon.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        equipedAttackWeapon.gameObject.transform.localRotation = Quaternion.identity;
        UnequipedAttackWeapon.gameObject.transform.parent = armorSlots[1];
        UnequipedAttackWeapon.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        UnequipedAttackWeapon.gameObject.transform.localRotation = Quaternion.identity;
    }

    #region 武器使用

    public virtual void Melee()//近攻
    {
        if (equipedAttackWeapon.type != Weapontype.Mellee) return;
        anim.SetTrigger("Melee");
    }

    public virtual void TakeShield(bool takeshield)//举盾
    {
        anim.SetBool("TakeShield", takeshield);
        if (equipedAssistWeapon!=null)
        {
            equipedAssistWeapon.gameObject.GetComponent<Collider>().enabled=true;
        }
    }


    public virtual void SetActiveMelee(bool active)
    {
        if (equipedAttackWeapon.type == Weapontype.Mellee)
        {
            var _equipedAttackWeapon = (MeleeWeapon)equipedAttackWeapon;
            _equipedAttackWeapon.SetActiveDamage(active);
        }
    }
    public virtual bool Shoot(bool shooting)
    {
        if (equipedAssistWeapon.type!=Weapontype.Ranged)
        {
            return false;
        }
        if (axeCount==0)
        {
            return false;
        }

        bool successful;
        if (shooting)
        {
            equipedAssistWeapon.OnAttack();
            successful = true;
        }
        else
        {
            successful = false;
        }

        if (successful)
        {


        }

        return successful;
    }



    #endregion
}
