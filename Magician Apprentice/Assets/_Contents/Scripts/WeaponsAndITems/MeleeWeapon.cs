using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapons {

    [Tooltip("配置此武器的所有HitBox")]
    public List<WeapontHitBox> hitBoxes;

    //被击对象缓存
    private Dictionary<WeapontHitBox, List<GameObject>> hitObjectCache;
    private bool canApplyDamage;

    public bool debugVisual;


    protected virtual void Start()
    {
        type = Weapontype.Mellee;
        hitObjectCache = new Dictionary<WeapontHitBox, List<GameObject>>();
        if (hitBoxes.Count > 0)
        {
            foreach (WeapontHitBox hitbox in hitBoxes)
            {
                hitbox.weapon = this;
                hitObjectCache.Add(hitbox, new List<GameObject>());
            }
        }
        else
        {
            this.enabled = false;
        }
    }
    public virtual void SetActiveDamage(bool value)
    {
        canApplyDamage = value;
        for (int i = 0; i < hitBoxes.Count; i++)
        {
            var hitCollider = hitBoxes[i];
            hitCollider.trigger.enabled = value;
            if (value == false && hitObjectCache != null)
                hitObjectCache[hitCollider].Clear();
        }
    }

    public virtual void OnHit(WeapontHitBox hitBox, Collider other)
    {
        if (canApplyDamage &&
            !hitObjectCache[hitBox].Contains(other.gameObject) &&
            (User != null && other.gameObject != User.gameObject))
        {

            hitObjectCache[hitBox].Add(other.gameObject);

            //SpawnHitEffect(other);
            //SpawnHitSound(other);

            var damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                var damageData = new DamageEventData(-hitImpact.GetDamage(), User);
                damageable.TakeDamage(damageData);

            }
        }
    }


    //protected void SpawnHitEffect(Collider other)
    //{

    //    var effect = hitImpact.GetHitEffect(other.sharedMaterial);

    //    if (effect)
    //    {
    //        var dir = (user.transform.position - other.transform.position).normalized;
    //        GameObject spawnedDecal = GameObject.Instantiate(effect, other.transform.position, Quaternion.LookRotation(dir));
    //        spawnedDecal.transform.SetParent(other.transform);
    //    }
    //}

    //protected void SpawnHitSound(Collider other)
    //{

    //    var sound = hitImpact.GetHitSound(other.sharedMaterial);
    //    if (sound)
    //    {
    //        AudioSource.PlayClipAtPoint(sound, other.transform.position);
    //    }
    //}
}
