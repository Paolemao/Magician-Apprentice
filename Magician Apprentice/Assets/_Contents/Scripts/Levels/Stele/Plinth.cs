using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plinth : MonoBehaviour {

    public bool altarUp;
    CameraManger cameraManger;
    Animator anim;

    private void Start()
    {
        altarUp = false;
        anim = GetComponent<Animator>();
        cameraManger = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<CameraManger>();
    }
    private void Update()
    {
        if (altarUp)
        {
            //相机转换
            cameraManger.transform.Find("Player_main_CM01").gameObject.SetActive(false);
            cameraManger.transform.Find("Player_main_CM02").gameObject.SetActive(true);
            Debug.Log("++++++++++++");
            anim.SetBool("Up",true);
        }       
    }

}
