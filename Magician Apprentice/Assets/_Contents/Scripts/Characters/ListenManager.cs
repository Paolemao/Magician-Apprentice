using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenManager : MonoBehaviour {

    float listenRange;

    [SerializeField]
    AudioSource source;

    [SerializeField]
    PlayerCharacter enemy;

    bool noFind;
    bool isNoise;

    public float ListenRange
    {
        get
        {
            return listenRange;
        }

        set
        {
            listenRange = value;
        }
    }

    public bool NoFind
    {
        get
        {
            return noFind;
        }

        set
        {
            noFind = value;
        }
    }

    public bool IsNoise
    {
        get
        {
            return isNoise;
        }

        set
        {
            isNoise = value;
        }
    }

    void Start () {
        NoFind = false;
        IsNoise = false;
        //source = null;
        //ListenRange = source.maxDistance;


    }
	
	void Update () {

        if (NoFind)
        {
            IsNoise = false;
        }

	}
}
