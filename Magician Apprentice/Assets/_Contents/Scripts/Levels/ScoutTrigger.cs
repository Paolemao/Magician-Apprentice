using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutTrigger : MonoBehaviour {

    [HideInInspector]
    public Transform enemy;
    [HideInInspector]
    public bool onTrigger;
    private void Start()
    {
        onTrigger = false;
        enemy = null;
    }
    private void OnTriggerEnter(Collider other)
    {
        onTrigger = true;
        if (other.tag == "Enemy")
        {
            enemy = other.gameObject.transform;
        }

    }
    
}
