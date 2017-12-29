using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManger : MonoBehaviour {

    public Dictionary<string,Camera> cameras=new Dictionary<string, Camera>();

    public bool Is_main_CM01;

	// Use this for initialization
	void Start () {
        for (int i=0; i<transform.childCount; i++)
        {
            switch (transform.GetChild(i).name)
            {
                case "Player_main_CM01":
                    cameras["Player_main_CM01"] = transform.GetChild(i).GetComponent<Camera>();
                    break;
                case "Player_idle_CM01":
                    cameras["Player_idle_CM01"] = transform.GetChild(i).GetComponent<Camera>();
                    break;
            }
        }

        Is_main_CM01 = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
