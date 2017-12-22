using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMove : SkilData {

    bool IsWork;

    protected override void Start()
    {
        base.Start();
        IsWork = true;
        StartCoroutine(SkillOver());
        GameController.Instance.Player.speed += 10;
    }


    IEnumerator SkillOver()
    {
        yield return new WaitForSeconds(10f);
        GameController.Instance.Player.speed -= 10;
        Destroy(gameObject);
    }
}
