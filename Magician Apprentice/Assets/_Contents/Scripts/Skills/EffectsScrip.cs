using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsScrip : MonoBehaviour {

    public GameObject impactParticle;

    [HideInInspector]
    public Vector3 impactNormal;

    protected bool hasCollided = false;

    [Tooltip("配置技能的所有hitBox")]
    public List<HitBox> hitBoxes;

    //获取被击中物体的缓存
    protected Dictionary<HitBox, List<GameObject>> hitObjectCache;

    protected bool canApplyDamage;

    public bool debugVisual;


    [SerializeField]
    protected HitImpact hitImpact;

    protected virtual void Start()
    {
        //特效（技能）的HitBox
        hitObjectCache = new Dictionary<HitBox, List<GameObject>>();
        if (hitBoxes.Count > 0)
        {
            foreach (HitBox hitBox in hitBoxes)
            {
                hitBox.skills =this;
                hitObjectCache.Add(hitBox, new List<GameObject>());
            }
        }
        else
        {
            this.enabled = false;
        }

    }
    protected virtual void Update()
    {

    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!hasCollided)
        {
            hasCollided = true;
        }
        if (other.tag=="Player")
        {
            return;
        }

        impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));

        Destroy(impactParticle, 3f);
        Destroy(gameObject);
    }


    public virtual void OnHit(HitBox hitbox, Collider other)
    {

        if (!hitObjectCache[hitbox].Contains(other.gameObject) &&
           (other.gameObject.tag != "Player"))
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
    protected virtual void OnTriggerStay(Collider other)
    {
        
    }

}
