using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLevel : MonoBehaviour {


    GameObject platform;
   // GameObject boom;

    private void Start()
    {
        platform = transform.Find("Platform").gameObject;
       // boom = transform.Find("Boom").gameObject;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag=="FireSkill")
        {
            platform.GetComponent<Animator>().SetBool("Move", true);
           // boom.SetActive(true);
            StartCoroutine(Boom());
        }
    }
    IEnumerator Boom()
    {
        yield return new WaitForSeconds(0.1f);
        this.gameObject.SetActive(false);
    }

}
