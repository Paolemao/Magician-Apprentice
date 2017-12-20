using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBall : SkilData {

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
        //死球，能被击打，能存在更长时间
        base.Start();
        rigid = GetComponent<Rigidbody>();
        rigid.constraints = RigidbodyConstraints.FreezePositionY;
        this.GetComponent<DeathBallEffectScrip>().impactNormal = hitInfo.normal;
    }
}
