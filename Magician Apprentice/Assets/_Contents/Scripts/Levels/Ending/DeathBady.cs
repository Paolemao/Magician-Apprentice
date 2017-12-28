using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBady : MonoBehaviour {

    public GameObject teacher;

    private void OnEnable()
    {
        var _teacher = Instantiate(teacher, transform.position, Quaternion.identity);
        _teacher.GetComponent<Animator>().Play("Death");
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag=="Player")
        {
            PlayerDialogBox.Show();
            PlayerDialogBox.Instance.DialogTip(70200);
        }
    }
    IEnumerator End()
    {
        yield return new WaitForSeconds(1f);


    }

}
