using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLevel : MonoBehaviour {


    GameObject platform;

    private void Start()
    {
        platform = transform.Find("Platform").gameObject;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag=="FireSkills")
        {
            platform.GetComponent<Animator>().SetBool("Move", true);
            StartCoroutine(Boom());
        }
    }
    IEnumerator Boom()
    {
        yield return new WaitForSeconds(0.1f);
        //platform.SetActive(false);
    }

}
