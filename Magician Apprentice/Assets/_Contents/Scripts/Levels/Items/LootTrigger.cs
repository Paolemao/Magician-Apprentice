using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//捡包和Ui查看
public class LootTrigger : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        if (other.tag=="Player")
        {

            other.gameObject.GetComponent<Character>().canTake = true;
            if (other.gameObject.GetComponent<Character>().takingWeapon)
            {
                //ui非空，ui还没打开,则打开反之
                if (PickUpPanel.Get()!=null&&!PickUpPanel.Get().gameObject.activeSelf)
                {
                    PickUpPanel.Show();
                    //Cursor.visible = true;
                }
                else
                {
                    PickUpPanel.Hide();
                    //Cursor.visible = false;
                }
            }
        }
    }
}
