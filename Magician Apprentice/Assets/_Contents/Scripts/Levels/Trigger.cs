using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour {

    public UnityEvent events;
    [SerializeField]
    private GameObject fire;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private Animator anim;

    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Skills")
        {
            fire = Instantiate(fire,firePoint.position,Quaternion.identity);
            anim.SetTrigger("Open") ;
        }
        events.Invoke();       
    }
}
