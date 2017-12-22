using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brazier : MonoBehaviour {

    GameObject fire;
    bool Firing;
    public bool isFire;

    private void OnEnable()
    {
        fire = transform.Find("Fire").gameObject;
        fire.SetActive(false);
    }
    private void OnDisable()
    {
        fire.SetActive(false);
    }
    private void Start()
    {
        //fire = transform.Find("Fire").gameObject;

        //fire.SetActive(false);

        isFire = false;
        Firing = false;


        //StartCoroutine(Goout());
    }
    private void Update()
    {
        if (Firing)
        {
            Firing = false;
            StartCoroutine(Goout());

        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag=="FireSkill")
        {
            fire.SetActive(true);
            Firing = true;
            isFire = true;
        }
    }
    IEnumerator Goout()
    {
        yield return new WaitForSeconds(5f);
        fire.SetActive(false);
        isFire = false;
    }
}
