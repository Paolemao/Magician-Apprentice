using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponTrigger : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Character>().canTake = true;
            if (other.gameObject.GetComponent<Character>().takingWeapon)
            {

                if (other.gameObject.GetComponent<Character>().UnequipedAttackWeapon == null)
                {

                    other.gameObject.GetComponent<Character>().UnequipedAttackWeapon = gameObject.GetComponent<Weapons>();
                    other.gameObject.GetComponent<Character>().UnequipedAttackWeapon.gameObject.transform.parent = other.gameObject.GetComponent<Character>().armorSlots[1];
                    other.gameObject.GetComponent<Character>().UnequipedAttackWeapon.gameObject.transform.localPosition = new Vector3(0,0,0);
                    other.gameObject.GetComponent<Character>().UnequipedAttackWeapon.gameObject.transform.localRotation = Quaternion.identity;
                }
                other.gameObject.GetComponent<Character>().takingWeapon = false;

                gameObject.GetComponent<Collider>().isTrigger = false;
            }
        }
    }
}
