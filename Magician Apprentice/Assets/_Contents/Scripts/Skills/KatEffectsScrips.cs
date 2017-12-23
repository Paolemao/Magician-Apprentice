using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatEffectsScrips : EffectsScrip {

    protected override void Start()
    {
        base.Start();
        StartCoroutine(DelayDestroy());
    }

    IEnumerator DelayDestroy()
    {
        if (!hasCollided)
        {
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);

        }
    }
}
