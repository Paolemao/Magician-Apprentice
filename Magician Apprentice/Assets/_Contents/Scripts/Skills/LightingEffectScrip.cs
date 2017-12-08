using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingEffectScrip : EffectsScrip {

    protected override void Start()
    {
        base.Start();
        StartCoroutine(DelayDestroy());
    }

    IEnumerator DelayDestroy()
    {
        if (!hasCollided)
        {
            yield return new WaitForSeconds(0.2f);
            Destroy(gameObject);

        }
    }
}
