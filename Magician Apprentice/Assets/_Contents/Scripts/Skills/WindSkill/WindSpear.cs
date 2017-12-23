using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpear : SkilData {

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
        this.GetComponent<WindEffectScrip>().impactNormal = hitInfo.normal;
    }
}
