using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Sensor))]
public class Ai_SpearEnemy : Character
{
    NavMeshAgent agent;//AI由导航网格控制移动


    #region AI控制相关
    //优先级（决策）
    List<int> priority;

    //行为（委托）
    delegate void AiAction();

    //打包（决策和行为）
    Dictionary<int, AiAction> DeAndAc;

    //目标1
    Transform _player;

    //远程AI的射程
    [SerializeField]
    [Range(0f, 30f)]
    float firingRang = 10f;

    //AI与攻击目标的直线距离
    float AiPlayerDistance;

    //视觉管理
    public Sensor_Spear sensor;
    //听觉管理
    ListenManager listenManager;
    public Transform _audoSource;//声源位置

    //导航点
    public WaypointGroup waypointGroup;
    int currentIndex = 0;



    #endregion


    protected override void Start()
    {
        base.Start();

        #region 武器初始化
        ProfaberWeapon_melee = Instantiate(ProfaberWeapon_melee);
        ProfaberWeapon_ranged = Instantiate(ProfaberWeapon_ranged);
        defaultAttackWeapon = ProfaberWeapon_melee.GetComponent<Weapons>();
        defaultAssistWeapon = ProfaberWeapon_ranged.GetComponent<Weapons>();


        equipedAttackWeapon = defaultAttackWeapon;
        equipedAssistWeapon = defaultAssistWeapon;

        //初始化武器位置
        equipedAttackWeapon.gameObject.transform.parent = armorSlots[0];
        equipedAttackWeapon.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        equipedAttackWeapon.gameObject.transform.localRotation = Quaternion.identity;

        //初始化副武器的位置
        equipedAssistWeapon.gameObject.transform.parent = armorSlots[2];
        equipedAssistWeapon.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        equipedAssistWeapon.gameObject.transform.localRotation = Quaternion.identity;

        agent = GetComponent<NavMeshAgent>();
        #endregion

        #region AI 相关字段的实例化
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        priority = new List<int>();
        DeAndAc = new Dictionary<int, AiAction>();
        listenManager = GetComponent<ListenManager>();

        sensor = GetComponent<Sensor_Spear>();

        
        beHit = false;
        axeCount = 3;
        Add();

        #endregion

        rigi.constraints = RigidbodyConstraints.None |
        RigidbodyConstraints.FreezeRotation |
        RigidbodyConstraints.FreezePositionX |
        RigidbodyConstraints.FreezePositionZ;
    }

    protected override void Update()
    {
        base.Update();
        if (!isDead)
        {
            JudgeState();
        }

    }

    protected override void UpdateControl()
    {
        base.UpdateControl();

        Movement(agent.velocity);
    }
    protected override void UpdateMovement()
    {
        //转向控制
        transform.Rotate(0, turnAmount * AngularSpeed * Time.deltaTime, 0);
        //移动控制
        agent.speed = speed;
        agent.angularSpeed = AngularSpeed;
        velocity = agent.velocity;
    }


    #region AI的10种优先级

    //10级巡逻
    void UpdatePatrol()
    {
        if (!waypointGroup)
        {
            return;
        }
        var points = waypointGroup.waypoints;

        var targetPos = points[currentIndex].transform.position;
        if (!agent.SetDestination(targetPos))
        {
            return;
        }
        if (agent.remainingDistance <= 0.01f)
        {

            currentIndex = (currentIndex + 1) % points.Count;
        }

    }
    //3级 被攻击
    void BeHit()
    {

        FindEnemy();
    }
    //4级（视觉）遇敌==>靠近(射程)==》攻击（飞斧）==》靠近==》攻击（草叉）
    void FindEnemy()
    {
        //当斧子数量足够且在射程范围内
        AiPlayerDistance = (transform.position - _player.position).magnitude;
        Debug.Log(AiPlayerDistance <= firingRang);
        if (axeCount > 0 && AiPlayerDistance <= firingRang)
        {
            agent.speed = 0;
            transform.LookAt(_player.position, Vector3.up);
            //equipedAttackWeapon.transform.parent.LookAt(_player.position, Vector3.up);
            ChangeWeapon();
            Shoot(true);
        }
        else if (AiPlayerDistance > firingRang)
        {
            if (!agent.SetDestination(_player.position))
            {
                return;
            }
        }
        else if (axeCount==0)
        {
            Shoot(false);
            equipedAttackWeapon.gameObject.transform.parent = armorSlots[0];
            equipedAttackWeapon.gameObject.transform.localPosition = new Vector3(0, 0, 0);
            equipedAttackWeapon.gameObject.transform.localRotation = Quaternion.identity;

            if (!agent.SetDestination(_player.position))
            {
                return;
            }
            if (agent.remainingDistance <= 3f)
            {
                bool active = true;
                SetActiveMelee(active);
                Melee();
                agent.speed = 0;
            }

        }

    }

    //5级(听觉) 靠近声源==》2分支
    void HearedNoise()
    {

        var AudoSourceDistance = ((transform.position - _audoSource.position).magnitude);
        if (AudoSourceDistance <= listenManager.ListenRange)
        {
            if (agent.SetDestination(_audoSource.position))
            {
                //举盾动作
                TakeShield(true);

                agent.speed = 3 * speed / 5;
            }
            if (agent.remainingDistance <= 0.3f)
            {
                listenManager.NoFind = true;
                return;
            }
        }
    }

    ////1级（血量不足）举盾
    //void Defencing()
    //{
    //    //当无盾时
    //    if (equipedAssistWeapon == null)
    //    {


    //        TakeShield(false);
    //        agent.speed = speed;
    //        FindEnemy();
    //        return;
    //    }
    //    else
    //    {

    //        if (!agent.SetDestination(_player.position))
    //        {
    //            return;
    //        }
    //        if (agent.remainingDistance <= 2f)
    //        {
    //            TakeShield(false);
    //            bool active = true;
    //            SetActiveMelee(active);
    //            Melee();
    //            agent.speed = 0;
    //        }
    //        else
    //        {
    //            TakeShield(true);
    //            agent.speed = 1 * speed / 5;
    //        }

    //    }
    //}

    //打包
    void Add()
    {
        DeAndAc.Add(10, UpdatePatrol);
        DeAndAc.Add(4, FindEnemy);
        DeAndAc.Add(5, HearedNoise);
        DeAndAc.Add(3, BeHit);
        //DeAndAc.Add(1, Defencing);
    }

    //判断状态
    void JudgeState()
    {
        Debug.Log(sensor.IsFindEnemy);
        int min = 10;
        priority.Clear();
        listenManager.enabled = true;
        if (sensor.IsFindEnemy)
        {
            priority.Add(4);
        }
        if (listenManager.IsNoise)
        {
            priority.Add(5);
        }
        if (healthPoint < 30)
        {
            priority.Add(1);
        }
        if (beHit)
        {
            priority.Add(3);
        }
        //取最优先，最小值为最优先
        foreach (var a in priority)
        {
            if (a < min)
            {
                min = a;
            }
        }
        foreach (var a in DeAndAc)
        {

            if (a.Key == min)
            {
                a.Value.Invoke();
            }
        }

    }

    //花圈调试
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, firingRang);
    }

    #endregion

    public override void ChangeWeapon()
    {
        equipedAttackWeapon.gameObject.transform.parent = armorSlots[3];
        equipedAttackWeapon.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        equipedAttackWeapon.gameObject.transform.localRotation = Quaternion.identity;

        equipedAssistWeapon.gameObject.transform.parent = armorSlots[1];
        equipedAssistWeapon.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        equipedAssistWeapon.gameObject.transform.localRotation = Quaternion.identity;

        //var newAssistWeapon = Instantiate(ProfaberWeapon_ranged);
        //newAssistWeapon.gameObject.transform.parent = armorSlots[2];
        //newAssistWeapon.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        //newAssistWeapon.gameObject.transform.localRotation = Quaternion.identity;

    }




}
