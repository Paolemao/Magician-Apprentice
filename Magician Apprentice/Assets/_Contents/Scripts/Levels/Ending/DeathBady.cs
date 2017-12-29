﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBady : MonoBehaviour {

    public GameObject teacher;

    private void OnEnable()
    {
        var _teacher = Instantiate(teacher, transform.position, Quaternion.identity);
        _teacher.GetComponent<Animator>().Play("Death");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            PlayerDialogBox.Show();
            PlayerDialogBox.Instance.DialogTip(70200);
            StartCoroutine(End());
        }
    }
    IEnumerator End()
    {
        yield return new WaitForSeconds(3f);
        PlayerDialogBox.Hide();
        EndUI.Show();
        yield return new WaitForSeconds(3f);
        EndUI.Hide();
        GameController.Instance.LoadScene("MainMenue");
        MainMenuPanel.Show();
    }

}
