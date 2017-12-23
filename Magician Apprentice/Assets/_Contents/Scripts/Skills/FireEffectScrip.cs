using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffectScrip : EffectsScrip {


    protected override void Start()
    {
        base.Start();
        StartCoroutine(DelayDestroy());
    }

    IEnumerator DelayDestroy()
    {
        if (!hasCollided)
        {
            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
            impactParticle = Instantiate(impactParticle, transform.position, Quaternion.identity);
            Destroy(impactParticle, 2f);
        }
    }
}
