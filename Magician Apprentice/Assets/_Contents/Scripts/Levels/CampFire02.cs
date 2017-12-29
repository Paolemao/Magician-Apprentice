using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire02 : MonoBehaviour {

    // Use this for initialization
    bool idle;
    bool camChange;
    CameraManger cameraManger;
    GameObject _player;


    #region before
   void Start()
    {
        idle = false;
        camChange = false;
        cameraManger = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<CameraManger>();
        _player = null;
    }

    // Update is called once per frame
    void Update()
    {

        if (idle)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //动画坐下
                if (_player )
                {

                    cameraManger.transform.Find("Player_main_CM01").gameObject.SetActive(false);
                    cameraManger.transform.Find("Player_main_CM02").gameObject.SetActive(false);


                    cameraManger.transform.Find("Player_idle_CM01").gameObject.SetActive(true);

                    _player.GetComponent<Animator>().SetBool("Sit", true);
                    _player.GetComponent<PlayerCharacter>().sit = true;

                    _player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    CampfirePanel.Show();
                    camChange = true;
                }
            }
            //UIButton_Leave
            if (_player.GetComponent<PlayerCharacter>().leaveCampfire)
            {
                idle = false;
            }
        }
        else
        {

            //动画起身
            if (_player)
            {
                //相机转换
                if (camChange)
                {
                    //相机转换
                    if (cameraManger.Is_main_CM01)
                    {
                        cameraManger.transform.Find("Player_main_CM01").gameObject.SetActive(true);
                    }
                    else
                    {
                        cameraManger.transform.Find("Player_main_CM02").gameObject.SetActive(true);
                    }

                    cameraManger.transform.Find("Player_idle_CM01").gameObject.SetActive(false);
                }

                _player.GetComponent<Animator>().SetBool("Sit", false);
                _player.GetComponent<PlayerCharacter>().sit = false;
                transform.Find("RoundFireRed").gameObject.SetActive(false);
                transform.Find("Collder").gameObject.layer = 0;

                if (_player.GetComponent<PlayerCharacter>().leaveCampfire)
                {
                    _player.GetComponent<PlayerCharacter>().leaveCampfire = false;
                    //idle = true;
                }
                _player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None |
                                                                RigidbodyConstraints.FreezeRotation;
                CampfirePanel.Hide();
                _player = null;
            }
        }

    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "FireSkills")
        {
            transform.Find("RoundFireRed").gameObject.SetActive(true);
            transform.Find("Collder").gameObject.layer = 9;
            gameObject.layer = 9;
            idle = true;
        }
        if (other.tag == "Player")
        {
            _player = other.gameObject;
            _player.gameObject.GetComponent<PlayerCharacter>().inCampfire = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _player = other.gameObject;
            _player.gameObject.GetComponent<PlayerCharacter>().inCampfire = false;
            StartCoroutine(FireOff());
        }
    }
    IEnumerator FireOff()
    {
        yield return new WaitForSeconds(5f);
        if (!_player)
        {
            yield break;
        }
        if (_player.gameObject.GetComponent<PlayerCharacter>().inCampfire)
        {
            yield break;
        }
       idle = false;
    }
    #endregion
}
