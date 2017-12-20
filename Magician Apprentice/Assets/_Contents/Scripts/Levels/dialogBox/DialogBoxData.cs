using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBoxData : MonoBehaviour {


    //对话框的一般属性
    int id;
    string dialogBox;

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public string DialogBox
    {
        get
        {
            return dialogBox;
        }

        set
        {
            dialogBox = value;
        }
    }

    public DialogBoxData()
    {
        this.Id = -1;//表示这是一个空的物品类
    }

    public DialogBoxData(int _id, string _dialogBox)
    {
        this.Id = _id;
        this.DialogBox = _dialogBox;
    }

}
