using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWall02 : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag=="Player")
        {
            PlayerDialogBox.Show();
            PlayerDialogBox.Instance.DialogTip(70206);
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            PlayerDialogBox.Hide();
        }
    }
}
