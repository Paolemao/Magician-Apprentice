using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerCharacter : Character {

    Vector3 aimTarget;
    public Transform spellMove;

    #region 职业
    [HideInInspector]
    public bool isWitch;
    #endregion

    #region 技能

    GameObject _skill = null;
    GameObject _skill02 = null;

    public bool OpenBox=false;
    public Transform spawnPosition;
    public Transform FootPosition;

    RaycastHit hitInfo;
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

    #region 新手教程
    bool hasTaked;
    bool hasTook;
    bool hasARest;
    bool hasEquiped;
    bool hasTip;

    public bool HasTaked
    {
        get
        {
            return hasTaked;
        }

        set
        {
            hasTaked = value;
        }
    }

    public bool HasTook
    {
        get
        {
            return hasTook;
        }

        set
        {
            hasTook = value;
        }
    }

    public bool HasARest
    {
        get
        {
            return hasARest;
        }

        set
        {
            hasARest = value;
        }
    }

    public bool HasEquiped
    {
        get
        {
            return hasEquiped;
        }

        set
        {
            hasEquiped = value;
        }
    }

    public bool HasTip
    {
        get
        {
            return hasTip;
        }

        set
        {
            hasTip = value;
        }
    }
    #endregion

    //开场
    public bool GameStart;
    CameraManger cameraManger;



    protected override void Start()
    {
        base.Start();
        //开始场景
        //GameOver.Hide();
        anim.Play("Sleep");
        DialogBoxPanel.Show();
        cameraManger = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<CameraManger>();

        aimTarget = new Vector3();
        isWitch = false;
        //血条的最大值与最高血量同
        var hud = transform.Find("HUD");

        hpSlider = hud.Find("HP").GetComponent<Slider>();
        mpSlider = hud.Find("MP").GetComponent<Slider>();
        handPic = hud.Find("Hands").GetComponent<GameObject>();


        hpSlider.maxValue = maxHp;
        mpSlider.maxValue = maxMp;

        //新手教程
        hasTaked = false;
        hasTook = false;
        hasARest = false;
        hasEquiped = false;
        hasTip = false;

    }

    protected override void Update()
    {

        base.Update();

        if (!isDead)
        {
            //UI血条蓝条
            mpSlider.value = MagicPoint;
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
        else
        {
            SceneManager.LoadScene("Persistent");
            InventroyManager.Instance.LoadInventory();
        }

        hpSlider.value = healthPoint;
        anim.SetBool("IsWitch",true);
    }


    protected override void UpdateControl()
    {
        if (!GameStart)//对话没结束，无法移动
        {
            forwardAmount = 0;
            turnAmount = 0;
            return;
        }
        else
        {
            anim.SetBool("GameBegin",true);
            //相机转换
            cameraManger.transform.Find("Player_main_CM01").gameObject.SetActive(true);
            cameraManger.transform.Find("Player_main_CM").gameObject.SetActive(false);
        }

        if (CheckGuiRaycastObjects())
        {
            forwardAmount = 0;
            turnAmount = 0;
            return;
        }

        if (sit)
        {
            forwardAmount = 0;
            turnAmount = 0;
            return;
        }

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
        if (isWitch&&MagicPoint>0)
        {
            ReleaseSkill();
            //UI提示
            SkillTip.AnimStart();
        }

        //新手教程
        if (HasTaked&&hasTook&&!hasTip)//完成交谈和开箱后_引导玩家穿装备
        {
            PlayerDialogBox.Show();
            PlayerDialogBox.Instance.DialogTip(70203);
            hasTip = true;
        }

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
        if (isAttacking||isFireAttacking)
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

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 100f))
        {
            if (equipedAssistWeapon == null)
            {

                if (Input.GetMouseButtonDown(0))
                {
                    MagicPoint -= 5;

                    Attacking(true);
                    anim.SetTrigger("Fire");
                    //查找所有技能
                    var skills = Resources.LoadAll<SkilData>("Prefab/" + "FireSkill");
                    foreach (var skill in skills)
                    {
                        if (skill.id == 91000)//通过Id找火系技能
                        {
                            var s = Instantiate(skill.gameObject, spawnPosition.position, Quaternion.identity) as GameObject;//实例化出来
                        }
                    }
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    Attacking(false);
                }

                if (Input.GetMouseButtonDown(1))
                {
                    MagicPoint -= 3;
                    StartCoroutine(WindWait());//风切
                    Attacking(true);
                }
                else if (Input.GetMouseButtonUp(1))
                {
                    Attacking(false);
                }
            }
            else
            {
                //获取符文书
                var spellbook = (Spellbook)equipedAssistWeapon;
                //查找所有技能
                var skills = Resources.LoadAll<SkilData>("Prefab/" + "FireSkill");
                var windSkills = Resources.LoadAll<SkilData>("Prefab/" + "WindSkill");

                #region FireSkill
                if (Input.GetMouseButtonDown(0))
                {

                    foreach (var skill in skills)
                    {
                        if (skill.id == spellbook.fireId)//通过Id找火系技能
                        {
                            if (spellbook.fireId == 91000)
                            {
                                MagicPoint -= 5;
                                anim.SetTrigger("Fire");
                                _skill02 = Instantiate(skill.gameObject, spawnPosition.position, Quaternion.identity) as GameObject;//实例化出来
                                _skill02.transform.LookAt(hitInfo.point);
                            }
                            else if (spellbook.fireId == 91010)
                            {
                                MagicPoint -= 7;
                                anim.SetTrigger("Fire");
                                _skill02 = Instantiate(skill.gameObject, spawnPosition.position, Quaternion.identity) as GameObject;//实例化出来
                                _skill02.transform.LookAt(hitInfo.point);
                                _skill02.GetComponent<FireEffectScrip>().impactNormal = hitInfo.normal;
                            }
                            else if (spellbook.fireId == 91001)
                            {
                                MagicPoint -= 6;
                                anim.SetTrigger("Fire");
                                _skill02 = Instantiate(skill.gameObject, spawnPosition.position, Quaternion.identity) as GameObject;//实例化出来
                            }
                            else if (spellbook.fireId == 91011)
                            {
                                MagicPoint -= 10;
                                _skill02 = Instantiate(skill.gameObject, spawnPosition.position, Quaternion.identity) as GameObject;//实例化出来
                            }
                        }
                        FireAttacking(true);
                    }

                }
                else
                {
                    FireAttacking(false);
                }

                if (Input.GetMouseButton(0))
                {
                    if (_skill02 == null)
                    {
                        return;
                    }
                    if (spellbook.fireId == 91011)//通过Id找火系技能
                    {
                        anim.SetBool("FireKeep", true);
                        // _skill02.transform.parent = spawnPosition;
                        _skill02.GetComponent<DeathLaser>().ShootBeamInDir(spawnPosition.position, transform.forward);
                        //_skill02.GetComponent<DeathLaser>().Shoot(spawnPosition.position);
                        FireAttacking(true);
                    }
                }
                else
                {
                    FireAttacking(false);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    anim.SetBool("FireKeep", false);
                }
                #endregion

                #region WindSkill

                if (Input.GetMouseButtonDown(1))
                {
                    foreach (var skill in windSkills)
                    {

                        if (skill.id == spellbook.windId)//通过Id找风系技能
                        {
                            if (spellbook.windId == 92000)//通过Id找风系技能
                            {
                                MagicPoint -= 3;
                                StartCoroutine(WindWait());
                            }
                            else if (spellbook.windId == 92010)
                            {
                                MagicPoint -= 5;
                                _skill = Instantiate(skill.gameObject, FootPosition.position, Quaternion.identity) as GameObject;//实例化出来
                                _skill.transform.parent = FootPosition;
                            }
                            else if (spellbook.windId == 92001)
                            {
                                MagicPoint -= 8;

                                _skill = Instantiate(skill.gameObject, spawnPosition.position, spawnPosition.rotation);
                                _skill.transform.Rotate(new Vector3(0, 90, 0));
                                _skill.transform.parent = spawnPosition;

                                rigi.constraints = RigidbodyConstraints.FreezePosition;
                            }
                            else if(spellbook.windId == 92011)
                            {
                                MagicPoint -= 10;
                                anim.SetTrigger("Fire");
                                _skill = Instantiate(skill.gameObject, spawnPosition.position, Quaternion.identity) as GameObject;//实例化出来
                                _skill.transform.LookAt(hitInfo.point);

                                _skill.GetComponent<KatEffectsScrips>().impactNormal = hitInfo.normal;
                            }
                        }
                        Attacking(true);
                    }

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
                    if (spellbook.windId == 92001)//通过Id找风系技能
                    {
                        //循环动画
                        anim.SetBool("WindKeep", true);
                    }
                    Attacking(true);
                }
                else
                {
                    rigi.constraints = RigidbodyConstraints.None |
                                       RigidbodyConstraints.FreezeRotation;
                    if (_skill != null)
                    {
                        //Destroy(_skill);
                    }

                    Attacking(false);
                }

                if (Input.GetMouseButtonUp(1))
                {
                    if (spellbook.windId == 92001)//通过Id找风系技能
                    {
                        Destroy(_skill); 
                        //结束动画
                        anim.SetBool("WindKeep", false);
                    }

                }

                #endregion

            }
        }
            
    }
    IEnumerator WindWait()
    {
        var skills = Resources.LoadAll<SkilData>("Prefab/" + "WindSkill");
        foreach (var skill in skills)
        {
            if (skill.id == 92000)//通过Id找风系技能
            {
                anim.SetTrigger("Thunder");//动画
                yield return new WaitForSeconds(0.5f);
                var _skill = Instantiate(skill.gameObject, spawnPosition.position, spawnPosition.rotation) as GameObject;//实例化出来
                _skill.transform.Rotate(new Vector3(90, 0, 0));
                _skill.GetComponent<WindEffectScrip>().impactNormal = hitInfo.normal;

            }

        }

    }//风切


}
