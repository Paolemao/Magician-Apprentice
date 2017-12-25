using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            transform.Find("Teacher").gameObject.SetActive(true);
        }
    }
}
