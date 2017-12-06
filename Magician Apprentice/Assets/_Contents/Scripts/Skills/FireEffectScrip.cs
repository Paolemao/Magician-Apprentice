using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffectScrip : MonoBehaviour {

    
    public GameObject impactParticle;

    [HideInInspector]
    public Vector3 impactNormal;

    private bool hasCollided = false;

    [Tooltip("配置技能的所有hitBox")]
    public List<HitBox> hitBoxes;

    //获取被击中物体的缓存
    private Dictionary<HitBox, List<GameObject>> hitObjectCache;

    private bool canApplyDamage;

    public bool debugVisual;


    //施法者
    private Character _user;
    public Character user
    {
        get
        {
            if (_user == null)
                _user = GetComponent<Character>();
            if (!_user)
                _user = GetComponentInParent<Character>();
            return _user;
        }
    }

    [SerializeField]
    protected HitImpact hitImpact;

    private void Start()
    {
        StartCoroutine(DelayDestroy());

        hitObjectCache = new Dictionary<HitBox, List<GameObject>>();

        if (hitBoxes.Count > 0)
        {
            foreach (HitBox hitBox in hitBoxes)
            {
                hitBox.skills = this;
                hitObjectCache.Add(hitBox, new List<GameObject>());
            }
        }
        else
        {
            this.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasCollided)
        {
            hasCollided = true;
        }
        impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));

        Destroy(impactParticle, 3f);
        Destroy(gameObject);
    }
    //private void OnCollisionEnter(Collision hit)
    //{
    //    if (!hasCollided)
    //    {
    //        hasCollided = true;
    //    }

    //    ////yield WaitForSeconds (0.05);
    //    //foreach (GameObject trail in trailParticles)
    //    //{
    //    //    GameObject curTrail = transform.Find(projectileParticle.name + "/" + trail.name).gameObject;
    //    //    curTrail.transform.parent = null;
    //    //    Destroy(curTrail, 3f);
    //    //}
    //    //Destroy(projectileParticle, 3f);
    //    //Destroy(impactParticle, 5f);
    //    //Destroy(gameObject);
    //    ////projectileParticle.Stop();

    //    impactParticle = Instantiate(impactParticle,transform.position,Quaternion.FromToRotation(Vector3.up,impactNormal));
    //    Destroy(gameObject);
    //    Destroy(impactParticle, 3f);

    //}

    public virtual void OnHit(HitBox hitbox, Collider other)
    {

        if (!hitObjectCache[hitbox].Contains(other.gameObject) &&
           (user != null && other.gameObject != user.gameObject))
        {
 
            hitObjectCache[hitbox].Add(other.gameObject);

            var damageable = other.GetComponent<IDamageable>();

            if (damageable != null)
            {
                var damageDate = new DamageEventData(-hitImpact.GetDamage(), user);
                damageable.TakeDamage(damageDate);
                Debug.Log("+++++++++++");
            }
        }
    }
    

    IEnumerator DelayDestroy()
    {
        if (!hasCollided)
        {
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
            impactParticle = Instantiate(impactParticle, transform.position, Quaternion.identity);
            Destroy(impactParticle, 3f);
        }
    }



}
