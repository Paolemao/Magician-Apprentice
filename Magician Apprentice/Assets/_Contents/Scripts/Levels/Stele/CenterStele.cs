using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterStele : MonoBehaviour {

    public bool beTrigger;
    GameObject red;
    GameObject green;

    private void Start()
    {
        red = transform.Find("Red").gameObject;
        red.SetActive(true);
        green = transform.Find("Green").gameObject;
        green.SetActive(false);

        //外来访问
        beTrigger = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "LightingSkill")
        {
            red.SetActive(false);
            green.SetActive(true);

            beTrigger = true;
        }
    }



}
