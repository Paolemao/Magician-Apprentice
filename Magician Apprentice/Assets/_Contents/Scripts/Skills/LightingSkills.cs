using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingSkills : Skills {

    delegate IEnumerator SkillsTpye();

    Dictionary<string, SkillsTpye> skillstpye = new Dictionary<string, SkillsTpye>();

    public Spellbook spellbookRune;

    public override void Putskills()
    {
        base.Putskills();
        //根据魔法书里的雷法槽决定使用的法术
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 100f))
        {
            if (spellbookRune == null)
            {
                StartCoroutine(Lighting());
            }
            else if (spellbookRune.FireSlota == null)
            {
                StartCoroutine(Lighting());
            }
            else
            {
                foreach (string skillName in skillstpye.Keys)
                {
                    if (skillName == "Lighting" + spellbookRune.FireSlota[0])
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
        skillstpye.Add("Lighting", Lighting);
        skillstpye.Add("LightingMove", LightingMove);
    }
    private void Update()
    {
        spellbookRune = (Spellbook)gameObject.GetComponent<PlayerCharacter>().equipedAssistWeapon;
        Debug.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 100, Color.yellow);
    }
    IEnumerator Lighting()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject projectile = Instantiate(SkillsEffects[currentEffect], spawnPosition.position,spawnPosition.rotation);
        projectile.transform.Rotate(new Vector3(90, 0, 0));
        //projectile.transform.rotation = spawnPosition.localRotation;
        projectile.GetComponent<LightingEffectScrip>().impactNormal = hitInfo.normal;

    }
    IEnumerator LightingMove()
    {
        yield return null;
        StartCoroutine(Lighting());
    }
}
