using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTip : MonoBehaviour {



    private void OnTriggerEnter(Collider other)
    {
        //int lay = LayerMask.NameToLayer("Player");
        if (other.tag == "Player")
        {
            UI_ActionTips.Show();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            UI_ActionTips.Hide();
        }
    }

    //    //花圈调试
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, firingRang);
    //}
}
