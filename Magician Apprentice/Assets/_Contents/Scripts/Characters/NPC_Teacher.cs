using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Teacher : MonoBehaviour {

    // Use this for initialization
    bool talkingOver;
    public GameObject Boom;

    public bool TalkingOver
    {
        get
        {
            return talkingOver;
        }

        set
        {
            talkingOver = value;
        }
    }

    void Start () {       
        TalkingOver = false;
    }

    void Update()
    {
        if (TalkingOver)
        {
            var boom = Instantiate(Boom,transform.position,Quaternion.identity);
            gameObject.SetActive(false);
            TalkingOver = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag=="Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogBoxPanel02.Show();
                transform.LookAt(new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z));
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            DialogBoxPanel02.Hide();
        }
    }
}
