using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katrina : SkilData {


    [SerializeField]
    public float speed = 100f;
    RaycastHit hitInfo;

    private void OnEnable()
    {

    }
    protected override void Start()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 100f))
        {
            transform.LookAt(hitInfo.point);

            //this.GetComponent<WindEffectScrip>().impactNormal = hitInfo.normal;
        }

    }
    protected override void Update()
    {
        base.Update();
        transform.Translate(0,0,speed*Time.deltaTime);
    }
}
