using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plat : MonoBehaviour {

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag=="Player")
        {
            collision.collider.transform.parent = transform;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            transform.DetachChildren();
        }
    }


}
