using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2_lightTrigger : MonoBehaviour {

    public GameObject[] light = new GameObject[9];
    int j = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            for (int i=0;i<light.Length;i++)
            {
                light[j].transform.Find("TorchFireYellow").gameObject.SetActive(true);

            }
        }
    }
}
