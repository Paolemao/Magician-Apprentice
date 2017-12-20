using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对话总管理
/// </summary>
public class DialogBoxManager : MonoBehaviour {

    //单例模式
    private static DialogBoxManager _instance;

    public static DialogBoxManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("DialogBoxManager").GetComponent<DialogBoxManager>();
            }
            return _instance;
        }
    }
    private List<DialogBoxData> dialogList;
    private void Awake()
    {
        ParseItemJson();
    }
    ///<summary>
    ///解析Json文件
    ///</summary>
    public void ParseItemJson()
    {
        dialogList = new List<DialogBoxData>();

        //文本在unity里时TextAsset类型
        TextAsset dialogText = Resources.Load<TextAsset>("GameData/" + "dialogBox");//加载Json文件
        string dialogJson = dialogText.text;//得到Json文件里的文本内容
        Debug.Log(dialogJson);
        JSONObject j = new JSONObject(dialogJson);
        foreach (var temp in j.list)
        {
            //下面解析的时物品的共有属性：id,name,等
            int id = (int)(temp["id"].n);
            string dialogBox = temp["dialogBox"].str;
            DialogBoxData dialog = null;
            dialog = new DialogBoxData(id, dialogBox);

            dialogList.Add(dialog);//把解析到的对话盒加入列表里面
        }
    }
    //根据id得到item
    public DialogBoxData GetDialogById(int id)
    {
        foreach (DialogBoxData dialog in dialogList)
        {
            if (dialog.Id == id)
            {
                return dialog;
            }
        }
        return null;
    }

}
