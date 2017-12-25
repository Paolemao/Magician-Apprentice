using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireKuqiEffect : EffectsScrip {

    protected override void OnTriggerEnter(Collider other)
    {
        if (hasCollider)
        {
            impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));
            hasCollider = false;
            WaitColl();
        }
        Destroy(impactParticle, 3f);
    }
    public override void OnHit(HitBox hitbox, Collider other)
    {
        if (!hitObjectCache[hitbox].Contains(other.gameObject) &&
   (other.gameObject.tag == "Player"))
        {

            hitObjectCache[hitbox].Add(other.gameObject);

            var damageable = other.GetComponent<IDamageable>();

            if (damageable != null)
            {
                var damageDate = new DamageEventData(-hitImpact.GetDamage());
                damageable.TakeDamage(damageDate);
            }
        }
    }
}
