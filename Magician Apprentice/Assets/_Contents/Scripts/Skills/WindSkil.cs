using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSkil : Skills {

    delegate IEnumerator SkillsTpye();

    Dictionary<string, SkillsTpye> skillstpye = new Dictionary<string, SkillsTpye>();

    public Spellbook spellbookRune;

    public GameObject projectile ;


    public override void Putskills()
    {
        base.Putskills();
        //根据魔法书里的风法槽决定使用的法术
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 100f))
        {
            if (spellbookRune == null)
            {
                projectile.SetActive(true);
            }
            else if (spellbookRune.FireSlota == null)
            {
                StartCoroutine(Wind());
            }
            else
            {
                foreach (string skillName in skillstpye.Keys)
                {
                    if (skillName == "Wind" + spellbookRune.FireSlota[0])
                    {
                        StartCoroutine(skillstpye[skillName]());
                    }
                }
            }
        }
    }
    public override void Unkeep()
    {
        projectile.SetActive(false);
    }

    protected override void Start()
    {
        base.Start();
        skillstpye.Add("Lighting", Wind);
        skillstpye.Add("LightingMove", WindMove);
        StartCoroutine(Wind());
    }
    private void Update()
    {
        spellbookRune = (Spellbook)gameObject.GetComponent<PlayerCharacter>().equipedAssistWeapon;
        Debug.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 100, Color.yellow);
    }
    IEnumerator Wind()
    {
        yield return new WaitForSeconds(0.5f);
        projectile = Instantiate(SkillsEffects[currentEffect], spawnPosition.position,spawnPosition.rotation);
        projectile.transform.parent = spawnPosition;
        //projectile.SetActive(false);

    }
    IEnumerator WindMove()
    {
        yield return null;
        StartCoroutine(Wind());
    }
}
