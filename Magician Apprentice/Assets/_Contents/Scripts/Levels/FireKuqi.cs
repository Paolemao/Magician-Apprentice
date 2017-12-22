using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireKuqi : MonoBehaviour {

    Transform blue;

	// Use this for initialization
	void Start () {
        blue = transform.Find("Blue");
	}
	
	// Update is called once per frame
	void Update () {
        RayTest();

    }
    void RayTest()
    {
        Debug.DrawRay(transform.position, transform.up, Color.red);
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position,transform.up,out hitInfo,100f))
        {
            if (hitInfo.collider.tag == "Test")
            {
                Debug.Log(blue.localScale.z);
                Debug.Log((transform.position - hitInfo.point).magnitude);
                var a = (transform.position - hitInfo.point).magnitude * 2.3f / 10;
                if (a>1)
                {
                    a = 1;
                }
                blue.localScale = new Vector3(blue.localScale.x, blue.localScale.y, a);
            }
            else
            {
                blue.localScale = new Vector3(1,1,1);

            }
        }
    }
}
