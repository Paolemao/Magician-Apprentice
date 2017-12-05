using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemTrigger : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {

        if (other.tag=="Player")
        {

            other.gameObject.GetComponent<Character>().canTake = true;
            if (other.gameObject.GetComponent<Character>().takingItem)
            {
                other.gameObject.GetComponent<Character>().equipedAssistWeapon = gameObject.GetComponent<Weapons>();
                other.gameObject.GetComponent<Character>().equipedAssistWeapon.transform.parent= 
                    other.gameObject.GetComponent<Character>().ItemSlot;
                other.gameObject.GetComponent<Character>().equipedAssistWeapon.gameObject.transform.localPosition = new Vector3(0, 0, 0);
                other.gameObject.GetComponent<Character>().equipedAssistWeapon.gameObject.transform.localRotation = Quaternion.identity;

                other.gameObject.GetComponent<Character>().takingItem = false;
                gameObject.GetComponent<Collider>().isTrigger = false;
            }
        }
    }

}
