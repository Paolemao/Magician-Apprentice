using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTrigger : MonoBehaviour {

    
    AudioSource audioSource;

    private void Start()
    {


        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {

            transform.Find("Rain").gameObject.SetActive(true);

            var door = transform.Find("Door");
            door.transform.Find("DoorL").gameObject.GetComponent<Animator>().SetBool("Open", true);
            door.transform.Find("DoorR").gameObject.GetComponent<Animator>().SetBool("Open", true);

            other.GetComponent<Animator>().SetBool("IsWitch", true);
            audioSource.Play();
            StartCoroutine(DeathBOdy());//3秒后尸体出现
        }
    }
    IEnumerator DeathBOdy()
    {
        yield return new WaitForSeconds(3f);
        transform.Find("Teacher").gameObject.SetActive(true);
    }
}
