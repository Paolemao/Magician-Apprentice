using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour {

    public GameObject BoxOpen;
    public GameObject BoxClose;
    public GameObject Weapon;

	// Use this for initialization
	void Start () {

        BoxOpen.SetActive(false);
        BoxClose.SetActive(true);
        Weapon.SetActive(false);
}

    private void Update()
    {
        Weapon.transform.Rotate(Vector3.up * 10f);
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
                Weapon.SetActive(true);
                Weapon.transform.localPosition = new Vector3(0,other.transform.position.y,0);
                Weapon.transform.rotation = Quaternion.Euler(0,0,30);
            }
        }
    }
}
