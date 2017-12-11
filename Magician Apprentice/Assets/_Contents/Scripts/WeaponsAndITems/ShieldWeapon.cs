using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldWeapon : Weapons {

    [SerializeField]
    private int durability;

    void Start () {

        durability = 5;
	}
	
	// Update is called once per frame
	void Update () {
        if (durability <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Skills")
        {
            durability -= 1;
        }
    }
}
