using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMove : FireBall {

    [Range(1000f,5000f)]
    [SerializeField]
    public float speed=1000f;

    private void OnEnable()
    {
        
    }
    protected override void Start()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 100f))
        {
            transform.LookAt(hitInfo.point);
            rigid = GetComponent<Rigidbody>();
            rigid.constraints = RigidbodyConstraints.None;
            this.GetComponent<FireEffectScrip>().impactNormal = hitInfo.normal;
        }

    }
    protected override void Update()
    {
        base.Update();
       rigid.AddForce(this.transform.forward * speed);
    }
}
