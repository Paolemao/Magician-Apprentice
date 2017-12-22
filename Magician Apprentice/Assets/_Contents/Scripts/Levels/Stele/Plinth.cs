using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plinth : MonoBehaviour {

    public bool altarUp;

    Animator anim;

    private void Start()
    {
        altarUp = false;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (altarUp)
        {
            Debug.Log("++++++++++++");
            anim.SetBool("Up",true);
        }       
    }

}
