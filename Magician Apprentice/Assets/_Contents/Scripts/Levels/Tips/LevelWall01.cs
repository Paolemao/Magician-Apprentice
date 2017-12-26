using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWall01 : MonoBehaviour {

    bool ending;

    private void Start()
    {
        ending = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag=="Player")
        {
            if (ending==false)
            {
                PlayerDialogBox.Show();
            }


            if (!GameController.Instance.Player.HasTaked)//与NPC谈话结束前_引导玩家与NPC谈话
            {
                PlayerDialogBox.Instance.DialogTip(70201);
            }
            else if (!GameController.Instance.Player.HasTook)//打开宝箱前_引导玩家去打开宝箱
            {
                PlayerDialogBox.Instance.DialogTip(70202);
            }
            else if (!GameController.Instance.Player.HasARest)//坐篝火前_引导玩家去篝火休息
            {
                PlayerDialogBox.Instance.DialogTip(70204);
                ending = true;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag=="Player")
        {
            PlayerDialogBox.Hide();
        }
    }

}
