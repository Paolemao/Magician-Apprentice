using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeEffect : MonoBehaviour {

    public GameObject[] trailParticles;
    public GameObject impactParticle;

    [HideInInspector]
    public Vector3 impactNormal;
}
