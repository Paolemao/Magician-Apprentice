using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour {

    [HideInInspector]
    public EffectsScrip skills;

    [HideInInspector]
    public Collider trigger;

    protected void Start()
    {
        trigger = GetComponent<Collider>();
        if (!trigger)
        {
            trigger = gameObject.AddComponent<SphereCollider>();
        }

        trigger.isTrigger = true;
        trigger.enabled = true;
    }
    protected void OnTriggerEnter(Collider other)
    {

        if (CheckTrigger(other))
        {
            skills.OnHit(this,other);
        }
    }
    protected bool CheckTrigger(Collider other)
    {
        return ((skills!=null)&& (other.gameObject.tag != "Player"));
    }
    protected virtual void OnDrawGizmos()
    {

        trigger = gameObject.GetComponent<Collider>();

        if (!trigger) trigger = gameObject.AddComponent<SphereCollider>();
        Color color = Color.red;
        color.a = 0.6f;
        Gizmos.color = color;
        if (!Application.isPlaying && trigger && !trigger.enabled) trigger.enabled = true;
        if (trigger && trigger.enabled)
        {
            if (trigger as SphereCollider)
            {
                SphereCollider box = trigger as SphereCollider;

                var radius = transform.lossyScale * box.radius;

                Matrix4x4 rotationMatrix = Matrix4x4.TRS(box.bounds.center, transform.rotation, radius);
                Gizmos.matrix = rotationMatrix;
                Gizmos.DrawSphere(Vector3.zero, box.radius);
            }
        }
    }
}
