using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour {

    public GameObject BoxOpen;
    public GameObject BoxClose;
    public GameObject Loot;

	// Use this for initialization
	void Start () {

        BoxOpen.SetActive(false);
        BoxClose.SetActive(true);
        //Loot.SetActive(false);
}

    private void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {

            if (other.GetComponent<PlayerCharacter>().OpenBox)
            {
                Debug.Log("+++++++++++++++++++++");
                BoxOpen.SetActive(true);
                BoxClose.SetActive(false);
                //Loot.SetActive(true);
                //Loot.transform.localPosition = new Vector3(0,other.transform.position.y,0);
                //Loot.transform.rotation = Quaternion.Euler(0,0,30);
            }
        }
    }
}
