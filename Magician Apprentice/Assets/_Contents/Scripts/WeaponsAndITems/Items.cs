using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {

    public int id;
    // Use this for initialization
    private void Awake()
    {
        InventroyManager.Instance.GetItemById(id);
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
