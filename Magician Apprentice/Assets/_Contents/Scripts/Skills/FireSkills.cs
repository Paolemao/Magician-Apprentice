using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkills : Skills {

    delegate IEnumerator SkillsTpye();

    Dictionary<string, SkillsTpye> skillstpye=new Dictionary<string, SkillsTpye>();

    public Spellbook spellbookRune;

    public override void Putskills()
    {
        base.Putskills();

        //根据魔法书里的火法槽决定使用的法术
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hitInfo,100f))
        {
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
                foreach (string skillName in skillstpye.Keys)
                {
                    if (skillName == "fireBall" + spellbookRune.FireSlota[0])
                    {
                        StartCoroutine(skillstpye[skillName]());
                    }
                }
            }
        }
    }

    protected override void Start()
    {
        base.Start();
        skillstpye.Add("fireBall", FireBall);
        skillstpye.Add("fireBallMove", FireBallMove);
    }
    private void Update()
    {
        spellbookRune = (Spellbook)gameObject.GetComponent<PlayerCharacter>().equipedAssistWeapon;
    }
    IEnumerator FireBall()
    {
        yield return null;
        GameObject projectile = Instantiate(SkillsEffects[currentEffect],spawnPosition.position,Quaternion.identity) as GameObject;
        projectile.transform.parent = spawnPosition;
        projectile.transform.LookAt(hitInfo.point);
        projectile.GetComponent<FireEffectScrip>().impactNormal = hitInfo.normal;      
    }
    IEnumerator FireBallMove()
    {
        yield return null;
        GameObject projectile = Instantiate(SkillsEffects[currentEffect], spawnPosition.position, Quaternion.identity) as GameObject;
        projectile.transform.parent = spawnPosition;
        projectile.transform.LookAt(hitInfo.point);
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward*speed);
        projectile.GetComponent<FireEffectScrip>().impactNormal = hitInfo.normal;
    }

}
