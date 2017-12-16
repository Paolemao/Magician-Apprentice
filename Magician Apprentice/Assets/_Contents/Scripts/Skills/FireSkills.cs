using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkills : Skills {

    //delegate IEnumerator SkillsTpye();

    //Dictionary<string, SkillsTpye> skillstpye=new Dictionary<string, SkillsTpye>();

    public Spellbook spellbookRune;

    [HideInInspector]
    public GameObject _EffectProfaber=null;

    public override void Putskills()
    {
        base.Putskills();
        currentEffect = 0;
        //根据魔法书里的火法槽决定使用的法术
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hitInfo,100f))
        {
            _EffectProfaber= Instantiate(SkillsEffects[currentEffect], spawnPosition.position, Quaternion.identity) as GameObject;
            _EffectProfaber.transform.LookAt(hitInfo.point);

            if (spellbookRune == null)
            {
                StartCoroutine(FireBall());
            }
            else if (spellbookRune.FireSlota == null)
            {
                StartCoroutine(FireBall());
            }
            else
            {
                foreach (var rune in spellbookRune.FireSlota)
                {
                    switch (rune)
                    {
                        case Rune.Follow://槽二

                           // StartCoroutine(FireBallFollow());
                            break;
                        case Rune.Increase://槽二
                            break;
                        case Rune.Move://槽一
                            StartCoroutine(FireBallMove());
                            break;
                        case Rune.Restraint://槽一

                            Destroy(_EffectProfaber);
                            _EffectProfaber = null;
                            currentEffect = 1;
                            StartCoroutine(FireLaserBeam());
                            break;
                    }
                }
            }
            
        }
    }

    protected override void Start()
    {
        base.Start();
    }
    private void Update()
    {
        spellbookRune = (Spellbook)gameObject.GetComponent<PlayerCharacter>().equipedAssistWeapon;
        Debug.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 100, Color.yellow);
    }
    IEnumerator FireBall()
    {
        yield return null;
        //锁Y轴 防止下落
        _EffectProfaber.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        _EffectProfaber.GetComponent<FireEffectScrip>().impactNormal = hitInfo.normal;
        
    }
    IEnumerator FireBallMove()//移动
    {
        yield return null;
        _EffectProfaber.GetComponent<Rigidbody>().AddForce(_EffectProfaber.transform.forward*speed);
        _EffectProfaber.GetComponent<FireEffectScrip>().impactNormal = hitInfo.normal;

    }
    IEnumerator FireBallDown()
    {
        yield return null;
        GameObject projectile = Instantiate(SkillsEffects[currentEffect], spawnPosition.position, Quaternion.identity) as GameObject;
        projectile.transform.parent = spawnPosition;
        projectile.transform.LookAt(hitInfo.point);

        projectile.transform.position = hitInfo.point + Vector3.up * 5f;
        projectile.GetComponent<FireEffectScrip>().impactNormal = hitInfo.normal;

    }
    IEnumerator FireLaserBeam()
    {
        yield return null;
        _EffectProfaber = Instantiate(SkillsEffects[currentEffect], spawnPosition.position, Quaternion.identity);
        _EffectProfaber.transform.LookAt(hitInfo.point);
        _EffectProfaber.GetComponent<LaserBeamEffect>().StartInst();

    }//约束
    //IEnumerator FireBallFollow()
    //{
    //    yield return null;
    //    _EffectProfaber.transform.Find("Scout").gameObject.SetActive(true);
    //    Debug.Log(_EffectProfaber.transform.Find("Scout").gameObject.name);
    //    if (_EffectProfaber.transform.Find("Scout").gameObject.GetComponent<ScoutTrigger>().onTrigger)
    //    {
    //        var _enmey = _EffectProfaber.transform.Find("Scout").gameObject.GetComponent<ScoutTrigger>().enemy;
    //        Vector3 v = _enmey.position - _EffectProfaber.transform.position;
    //        float angle = Vector3.Angle(v, _EffectProfaber.transform.forward);
    //        float minAngle = Mathf.Min(angle, 300 * Time.deltaTime);
    //        _EffectProfaber.transform.Rotate(Vector3.Cross(_EffectProfaber.transform.forward, v.normalized), minAngle);
    //    }
    //}
}
