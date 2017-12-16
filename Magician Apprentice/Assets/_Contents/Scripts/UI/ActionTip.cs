using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTip : MonoBehaviour {



    private void OnCollisionEnter(Collision collision)
    {
        int lay = LayerMask.NameToLayer("ActionTips");
        if (collision.gameObject.layer == lay)
        {
            UI_ActionTips.Show();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        int lay = LayerMask.GetMask("ActionTips");
        if (collision.gameObject.layer == lay)
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
