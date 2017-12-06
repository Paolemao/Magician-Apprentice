using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour {

    public GameObject[] SkillsEffects;

    public Transform spawnPosition;

    public int currentEffect = 0;
    public float speed = 1000;

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

    [HideInInspector]
    public int Skillkey;

    protected RaycastHit hitInfo;

    protected virtual void Start()
    {

    }

    public virtual void Putskills()
    {

    }
}
