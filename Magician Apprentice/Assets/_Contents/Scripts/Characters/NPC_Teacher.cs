using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Teacher : Character {

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

    protected override void Start () {
        base.Start();
        TalkingOver = false;
    }

    protected override void Update()
    {
        base.Update();
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
                transform.LookAt(other.transform.position);
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
