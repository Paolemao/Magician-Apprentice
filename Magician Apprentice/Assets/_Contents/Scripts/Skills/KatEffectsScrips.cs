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

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Enemy")
        {
            if (hasCollider)
            {
                impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));
                hasCollider = false;
                WaitColl();
            }

            Destroy(impactParticle, 3f);
            Destroy(gameObject);
        }

    }
}
