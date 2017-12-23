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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FireSkills")
        {
            if (!transform.parent.GetComponent<Wheel>().levelStart)
            {
                return;
            }
            fire.SetActive(true);
            Firing = true;
            isFire = true;
        }
    }

    IEnumerator Goout()
    {
        yield return new WaitForSeconds(30f);
        fire.SetActive(false);
        isFire = false;
    }
}
