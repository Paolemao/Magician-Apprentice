using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicLevel3 : MonoBehaviour {


    GameObject Statue_01;
    GameObject Statue_02;
    GameObject Statue_03;

    GameObject buff;

    private void Start()
    {
        Statue_01 = transform.Find("Statue_01").gameObject;
        Statue_02 = transform.Find("Statue_02").gameObject;
        Statue_03 = transform.Find("Statue_03").gameObject;

        buff = transform.Find("Buff2").gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            StartCoroutine(StartAnim());
        }
    }
    IEnumerator StartAnim()
    {
        yield return new WaitForSeconds(1.5f);
        Statue_01.GetComponent<Animator>().SetBool("Up",true);
        yield return new WaitForSeconds(1.5f);
        Statue_02.GetComponent<Animator>().SetBool("Up", true);
        yield return new WaitForSeconds(1.5f);
        Statue_03.GetComponent<Animator>().SetBool("Up", true);

        yield return new WaitForSeconds(1.5f);
        Statue_01.transform.Find("Start").gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Statue_02.transform.Find("Start").gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Statue_03.transform.Find("Start").gameObject.SetActive(true);
        buff.SetActive(true);

        yield return new WaitForSeconds(3f);
        PlayerDialogBox.Hide();
        EndUI.Show();
        yield return new WaitForSeconds(3f);
        EndUI.Hide();
        GameController.Instance.LoadScene("MainMenue");
        MainMenuPanel.Show();

    }

}
