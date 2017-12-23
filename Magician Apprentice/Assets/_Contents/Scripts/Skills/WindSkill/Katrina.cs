using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katrina : SkilData {


    [SerializeField]
    public float speed = 50f;


    private void OnEnable()
    {
    }
    protected override void Start()
    {

        SkillOver();
    }
    protected override void Update()
    {
        base.Update();
        transform.Translate(0,0,speed*Time.deltaTime);
    }
    IEnumerator SkillOver()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
