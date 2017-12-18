using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour {

    // Use this for initialization
    public bool CanIdle;
    CameraManger cameraManger;
    GameObject _player;


	void Start () {
        CanIdle = false;
        cameraManger = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<CameraManger>();
        _player = null;
	}
	
	// Update is called once per frame
	void Update () {

        if (CanIdle)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //相机转换
                cameraManger.transform.Find("Player_main_CM01").gameObject.SetActive(false);
                cameraManger.transform.Find("Player_idle_CM01").gameObject.SetActive(true);
                //动画坐下
                if (_player)
                {
                    _player.GetComponent<Animator>().SetBool("Sit", true);
                    _player.GetComponent<PlayerCharacter>().sit = true;
                }
                _player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                CampfirePanel.Show();
            }
            //UIButton_Leave
            if (_player.GetComponent<PlayerCharacter>().leaveCampfire)
            {
                CanIdle = false;
            }
        }
        else
        {
            //相机转换
            cameraManger.transform.Find("Player_main_CM01").gameObject.SetActive(true);
            cameraManger.transform.Find("Player_idle_CM01").gameObject.SetActive(false);
            //动画起身
            if (_player)
            {
                _player.GetComponent<Animator>().SetBool("Sit", false);
                _player.GetComponent<PlayerCharacter>().sit = false;
                transform.Find("RoundFireRed").gameObject.SetActive(false);
                transform.Find("Collder").gameObject.layer = 0;

                if (_player.GetComponent<PlayerCharacter>().leaveCampfire)
                {
                    _player.GetComponent<PlayerCharacter>().leaveCampfire = false;
                    CanIdle = true;
                }
            }
            CampfirePanel.Hide();

        }

	}
    private void OnTriggerStay(Collider other)
    {
        if (other.tag=="FireSkills")
        {
            transform.Find("RoundFireRed").gameObject.SetActive(true);
            transform.Find("Collder").gameObject.layer = 9;
            gameObject.layer = 9;
            CanIdle = true;
        }
        if (other.tag=="Player")
        {
            _player = other.gameObject;
            other.gameObject.GetComponent<PlayerCharacter>().inCampfire = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(FireOff());
            other.gameObject.GetComponent<PlayerCharacter>().inCampfire = false;
        }
    }
    IEnumerator FireOff()
    {
        yield return new WaitForSeconds(5f);
        CanIdle = false;
    }
}
