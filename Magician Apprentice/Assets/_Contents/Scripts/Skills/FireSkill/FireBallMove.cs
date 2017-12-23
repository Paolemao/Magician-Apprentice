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
        rigid = GetComponent<Rigidbody>();
        rigid.constraints = RigidbodyConstraints.None;


    }
    protected override void Update()
    {
        base.Update();
       rigid.AddForce(this.transform.forward * speed);
    }
}
