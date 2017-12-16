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

    public bool OpenBox=false;
    #endregion

    //UI血槽
    Slider hpSlider;

    Slider mpSlider;

    GameObject handPic;

    #region 篝火互动
    public bool leaveCampfire=false;
    #endregion

    protected override void Start()
    {
        base.Start();
        equipedAttackWeapon = defaultAttackWeapon;
        equipedAssistWeapon = defaultAssistWeapon;

        //初始化武器位置
        equipedAttackWeapon.gameObject.transform.parent = armorSlots[0];
        equipedAttackWeapon.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        equipedAttackWeapon.gameObject.transform.localRotation = Quaternion.identity;

        fire = GetComponent<FireSkills>();
        lighting = GetComponent<LightingSkills>();
        Wind = GetComponent<WindSkil>();
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
        if (MagicPoint<=0)
        {
            MagicPoint = 0;
        }
        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift) && MagicPoint > 0)
        {


            MagicPoint -= 10;
            ThunderSkill();
            lighting.Putskills();
            //anim.ResetTrigger("Thunder");
            Attacking(true);
        }
        else if (Input.GetMouseButtonDown(1) && MagicPoint > 0)//火球
        {
            MagicPoint -= MagicPoint - 5;

            fire.Putskills();

            FireSkill();
            Attacking(true);
        }
        else if (Input.GetMouseButton(1) && MagicPoint > 0)//镭射
        {
            var _fire = (FireSkills)fire;
            if (_fire.spellbookRune==null)
            {
                return;
            }
            if (_fire.spellbookRune.FireSlota[0] == Rune.Restraint)
            {
                if (_fire._EffectProfaber==null)
                {
                    return;
                }
                _fire._EffectProfaber.GetComponent<LaserBeamEffect>().ShootBeamInDir(spellMove.position, transform.forward);
                //FireSkill();
                Attacking(true);
            }
            else
            {
                return;
            }
            
        }
        else if (Input.GetKey(KeyCode.Space) && MagicPoint > 0)
        {
            //    // MagicPoint -= MagicPoint - 5;
            //    if (equipedAssistWeapon != null)
            //    {
            //        SpellMove();
            //    }
            //    wind = true;
            rigi.constraints = RigidbodyConstraints.FreezePosition;
            //    Attacking(true);
            //    Wind.Putskills();

        }//风盾
        else if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift) && MagicPoint > 0)
        {
            MagicPoint -= MagicPoint - 5;
            IceSkill();
            Attacking(true);
        }//冰球
        else
        {
            Attacking(false);
            //wind = false;
            //var _wind = (WindSkil)Wind;
            //if (_wind.projectile)
            //{
            //    Wind.Unkeep();
            //}

            if (equipedAssistWeapon != null)
            {
                equipedAssistWeapon.gameObject.transform.localPosition = new Vector3(0, 0, 0);
                equipedAssistWeapon.gameObject.transform.localRotation = Quaternion.identity;
                //equipedAssistWeapon.gameObject.transform.Find("OpenBook").gameObject.SetActive(false);
                //equipedAssistWeapon.gameObject.transform.Find("CloseBook").gameObject.SetActive(true);
            }


            rigi.constraints = RigidbodyConstraints.None |
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezeRotationZ;
        }
        WindSkill();
        #endregion

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



}
