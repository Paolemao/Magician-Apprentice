using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : SkilData {

    protected Rigidbody rigid;
    private Character _user;
    public Character user
    {
        get
        {
            if (_user == null)
                _user = GetComponent<Character>();
            if (!_user)
                _user = GetComponentInParent<Character>();
            return _user;
        }
    }
    protected RaycastHit hitInfo;

    protected override void Start()
    {
        base.Start();
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 100f))
        {
            transform.LookAt(hitInfo.point);
            rigid = GetComponent<Rigidbody>();
            rigid.constraints = RigidbodyConstraints.FreezeAll;
            this.GetComponent<FireEffectScrip>().impactNormal = hitInfo.normal;
        }

    }
}
