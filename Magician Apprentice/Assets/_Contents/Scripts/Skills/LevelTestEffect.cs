using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTestEffect : EffectsScrip
{
    //[Header("Prefabs")]
    //public GameObject FatFlameThrowerGreen;


    protected override void Start()
    {
        base.Start();
        ShootInDir();
    }

    //private void Update()
    //{
    //    Debug.Log("+++++++++++++++++++");
    //    Ray ray = new Ray(transform.position,transform.forward);
    //    RaycastHit hitInfo;
    //    var layerMask = LayerMask.GetMask("NoEnviroment");
    //    if (Physics.Raycast(ray,out hitInfo,100f, layerMask))
    //    {
    //        Vector3 dir = hitInfo.point - transform.position;
    //        ShootInDir();
    //    }
    //    Debug.Log("+++++++++++++++++++");

    //}
    void ShootInDir()
    {
        Debug.Log("_______________________");
        transform.localScale = new Vector3(transform.localScale.x,transform.localScale.y,transform.localScale.z*0.5f);
    }

}
