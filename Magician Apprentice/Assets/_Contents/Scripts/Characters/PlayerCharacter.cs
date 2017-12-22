using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PlayerCharacter : Character {

    Vector3 aimTarget;
    public Transform spellMove;

    #region 技能
    Skills fire;
    Skills lighting;
    Skills Wind;
    GameObject _skill = null;
    GameObject _skill02 = null;

    public bool OpenBox=false;
    public Transform spawnPosition;
    public Transform FootPosition;
    #endregion

    #region UI
    //UI血槽
    Slider hpSlider;
    Slider mpSlider;
    GameObject handPic;
    //背包
    bool openKnapsack=false;
    #endregion

    #region 篝火互动
    public bool leaveCampfire=false;
    public bool inCampfire = false;
    #endregion

    protected override void Start()
    {
        base.Start();
        //equipedAttackWeapon = defaultAttackWeapon;
        //equipedAssistWeapon = defaultAssistWeapon;

        ////初始化武器位置
        //equipedAttackWeapon.gameObject.transform.parent = armorSlots[0];
        //equipedAttackWeapon.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        //equipedAttackWeapon.gameObject.transform.localRotation = Quaternion.identity;

        //fire = GetComponent<FireSkills>();
        //lighting = GetComponent<LightingSkills>();
        //Wind = GetComponent<WindSkil>();
        aimTarget = new Vector3();

        ////血条的最大值与最高血量同
        //var hud = transform.Find("HUD");
  
        //hpSlider = hud.Find("HP").GetComponent<Slider>();
        //mpSlider = hud.Find("MP").GetComponent<Slider>();
        //handPic = hud.Find("Hands").GetComponent<GameObject>();


        //hpSlider.maxValue = maxHp;
        //mpSlider.maxValue = maxMp;
    }

    protected override void Update()
    {

        base.Update();
        if (!isDead)
        {
            //UI血条蓝条
            //mpSlider.value = MagicPoint;
            var images = Resources.FindObjectsOfTypeAll<Image>();
            if (beHit)
            {
                foreach (var im in images)
                {
                    if (im.name == "Cry")
                    {
                        im.gameObject.SetActive(true);
                    }
                    if (im.name == "Normal")
                    {
                        im.gameObject.SetActive(false);
                    }
                }
            }
        }
        //hpSlider.value = healthPoint;
        anim.SetBool("IsWitch",true);
    }


    protected override void UpdateControl()
    {
        base.UpdateControl();
        UpdateAimTarget();
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        //移动向量
        var move = v * Vector3.forward + h * Vector3.right;
        Movement(move);


        #region 技能控制相关
        //if (MagicPoint<=0)
        //{
        //    MagicPoint = 0;
        //}
        //if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift) && MagicPoint > 0)
        //{


        //    MagicPoint -= 10;
        //    ThunderSkill();
        //    lighting.Putskills();
        //    //anim.ResetTrigger("Thunder");
        //    Attacking(true);
        //}
        //else if (Input.GetMouseButtonDown(1) && MagicPoint > 0)//火球
        //{
        //    MagicPoint -= MagicPoint - 5;

        //    fire.Putskills();

        //    FireSkill();
        //    Attacking(true);
        //}
        //else if (Input.GetMouseButton(1) && MagicPoint > 0)//镭射
        //{
        //    //var _fire = (FireSkills)fire;
        //    //if (_fire.spellbookRune==null)
        //    //{
        //    //    return;
        //    //}
        //    //if (_fire.spellbookRune.FireSlota[0] == Rune.Restraint)
        //    //{
        //    //    if (_fire._EffectProfaber==null)
        //    //    {
        //    //        return;
        //    //    }
        //    //    _fire._EffectProfaber.GetComponent<LaserBeamEffect>().ShootBeamInDir(spellMove.position, transform.forward);
        //    //    //FireSkill();
        //    //    Attacking(true);
        //    //}
        //    //else
        //    //{
        //    //    return;
        //    //}

        //}
        //else if (Input.GetKey(KeyCode.Space) && MagicPoint > 0)
        //{
        //    //    // MagicPoint -= MagicPoint - 5;
        //    //    if (equipedAssistWeapon != null)
        //    //    {
        //    //        SpellMove();
        //    //    }
        //    //    wind = true;
        //    rigi.constraints = RigidbodyConstraints.FreezePosition;
        //    //    Attacking(true);
        //    //    Wind.Putskills();

        //}//风盾
        //else if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift) && MagicPoint > 0)
        //{
        //    MagicPoint -= MagicPoint - 5;
        //    IceSkill();
        //    Attacking(true);
        //}//冰球
        //else
        //{
        //    Attacking(false);
        //    //wind = false;
        //    //var _wind = (WindSkil)Wind;
        //    //if (_wind.projectile)
        //    //{
        //    //    Wind.Unkeep();
        //    //}

        //    if (equipedAssistWeapon != null)
        //    {
        //        equipedAssistWeapon.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        //        equipedAssistWeapon.gameObject.transform.localRotation = Quaternion.identity;
        //        //equipedAssistWeapon.gameObject.transform.Find("OpenBook").gameObject.SetActive(false);
        //        //equipedAssistWeapon.gameObject.transform.Find("CloseBook").gameObject.SetActive(true);
        //    }


        //    rigi.constraints = RigidbodyConstraints.None |
        //    RigidbodyConstraints.FreezeRotationX |
        //    RigidbodyConstraints.FreezeRotationY |
        //    RigidbodyConstraints.FreezeRotationZ;
        //}
        //WindSkill();
        #endregion

        //释放技能
        ReleaseSkill();

        #region 装备相关
        //捡武器
        if (Input.GetKeyDown(KeyCode.E))
        {

            takingWeapon = true;
            takingItem = true;
            OpenBox = true;
        }
        else
        {
            takingWeapon = false;
            takingItem = false;
            OpenBox = false;
        }
        //换武器
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeWeapon();
        }

        //装备对属性的影响
        if (equipedAttackWeapon != null)
        {
            if (!equipedAttackWeapon.GetComponent<Wand>().affected)
            {
                equipedAttackWeapon.OnEquip();
                equipedAttackWeapon.GetComponent<Wand>().affected = true;
            }
        }
        if (equipedAssistWeapon != null)
        {
            if (!equipedAssistWeapon.GetComponent<Spellbook>().affected)
            {
                equipedAssistWeapon.OnEquip();
                equipedAssistWeapon.GetComponent<Spellbook>().affected = true;
            }
        }
        #endregion

        #region UI控制相关
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!openKnapsack)
            {
                Knapsack.Show();
                openKnapsack = true;
            }
            else
            {
                Knapsack.Hide();
                openKnapsack = false;
            }
        }
        #endregion

    }
    protected override void UpdateMovement()
    {
        base.UpdateMovement();
        if (isAttacking)
        {
            transform.LookAt(aimTarget,Vector3.up);
        }
    }
    private void UpdateAimTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit,2000f))
        {
            Vector3 finalPos = new Vector3(hit.point.x,transform.position.y,hit.point.z);

            aimTarget = finalPos;

        }
    }
    //法术书移动
    void SpellMove()
    {

        equipedAssistWeapon.gameObject.transform.position = spellMove.position;
        GameObject parent = GameObject.FindGameObjectWithTag("Spellbook");
        if (equipedAssistWeapon.gameObject.transform.Find("OpenBook").tag == "OpenBook")
        {
            equipedAssistWeapon.gameObject.transform.Find("OpenBook").gameObject.SetActive(true);
            equipedAssistWeapon.gameObject.transform.localRotation = spellMove.localRotation;
        }
        if (equipedAssistWeapon.gameObject.transform.Find("CloseBook").tag == "CloseBook")
        {
            equipedAssistWeapon.gameObject.transform.Find("CloseBook").gameObject.SetActive(false);
        }

    }
    void ReleaseSkill()
    {
        //如果没有魔法书，释放基础魔法
        if (equipedAssistWeapon== null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //查找所有技能
                var skills = Resources.LoadAll<SkilData>("Prefab/" + "FireSkill");
                foreach (var skill in skills)
                {                  
                    if (skill.id == 91000)//通过Id找火系技能
                    {
                        var s = Instantiate(skill.gameObject, spawnPosition.position, Quaternion.identity) as GameObject;//实例化出来
                    }
                }
                Attacking(true);
            }
            Attacking(false);

            if (Input.GetMouseButtonDown(1))
            {
                //查找所有技能
                var skills = Resources.LoadAll<SkilData>("Prefab/" + "WindSkill");
                Debug.Log(skills == null);
                foreach (var skill in skills)
                {
                    Debug.Log(skill.id);
                    if (skill.id == 92000)//通过Id找风系技能
                    {
                        StartCoroutine(WindWait());
                        var s = Instantiate(skill.gameObject, spawnPosition.position, spawnPosition.rotation) as GameObject;//实例化出来
                    }
                }
                Attacking(true);
            }
            Attacking(false);
        }
        else
        {
            //获取符文书
            var spellbook = (Spellbook)equipedAssistWeapon;
            //查找所有技能
            var skills = Resources.LoadAll<SkilData>("Prefab/" + "FireSkill");
            var windSkills= Resources.LoadAll<SkilData>("Prefab/" + "WindSkill");

            #region FireSkill
            if (Input.GetMouseButtonDown(0))
            {

                foreach (var skill in skills)
                {
                    if (skill.id == spellbook.fireId)//通过Id找火系技能
                    {

                        Debug.Log(skill.id);
                        _skill02 = Instantiate(skill.gameObject, spawnPosition.position, Quaternion.identity) as GameObject;//实例化出来
                        Debug.Log("********************");
                    }
                }
                Attacking(true);
            }
            else if (Input.GetMouseButton(0))
            {
                if (_skill02 == null)
                {
                    return;
                }
                if (spellbook.fireId == 91011)//通过Id找火系技能
                {
                    // _skill02.transform.parent = spawnPosition;
                    _skill02.GetComponent<DeathLaser>().ShootBeamInDir(spawnPosition.position, transform.forward);
                    //_skill02.GetComponent<DeathLaser>().Shoot(spawnPosition.position);

                }
                Attacking(true);
            }
            else
            {
                Attacking(false);
            }
            #endregion

            #region WindSkill

            if (Input.GetMouseButtonDown(1))
            {
                foreach (var skill in windSkills)
                {
                    if (skill.id == spellbook.windId)//通过Id找风系技能
                    {
                        Debug.Log(skill.id);
                        if (spellbook.windId == 92010)
                        {
                            _skill = Instantiate(skill.gameObject, FootPosition.position, Quaternion.identity) as GameObject;//实例化出来
                        }
                        _skill = Instantiate(skill.gameObject, spawnPosition.position, Quaternion.identity) as GameObject;//实例化出来

                    }
                }
                Attacking(true);
            }
            else
            {
                Attacking(false);
            }

            if (Input.GetMouseButton(1))
            {
                if (_skill == null)
                {
                    return;
                }
                if (spellbook.fireId == 92001)//通过Id找风系技能
                {
                    _skill.transform.parent = spawnPosition;
                    rigi.constraints = RigidbodyConstraints.FreezePosition;
                    //动画
                }
                Attacking(true);
            }
            else
            {
                rigi.constraints = RigidbodyConstraints.None|
                                   RigidbodyConstraints.FreezeRotation;
                if (_skill != null)
                {
                    //Destroy(_skill);
                }

                Attacking(false);
            }
            #endregion

        }
    }
    IEnumerator WindWait()
    {
        yield return new WaitForSeconds(0.5f);
    }


}
