using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteleManager : MonoBehaviour {

    GameObject centerStele;
    GameObject wheel;
    GameObject plinth;

    private void Start()
    {
        centerStele = transform.Find("CenterStele").gameObject;
        wheel = transform.Find("Wheel").gameObject;
        plinth = transform.Find("Plinth").gameObject;
    }
    private void Update()
    {
        var center = centerStele.GetComponent<CenterStele>();
        var wh = wheel.GetComponent<Wheel>();
        var pli = plinth.GetComponent<Plinth>();
        if (center.beTrigger)
        {
            wh.levelStart = true;
            //center.beTrigger = false;
        }
        else
        {
            wh.levelStart = false;
        }

        if (wh.level_2_Start)
        {
            pli.altarUp = true;
            wh.level_2_Start = false;
        }      
    }
}
