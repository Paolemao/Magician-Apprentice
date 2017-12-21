using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBoxPanel02 : MonoBehaviour
{


    private static DialogBoxPanel02 _instance;

    public static DialogBoxPanel02 Instance
    {
        get
        {
            if (_instance == null)
            {
                var t = Resources.FindObjectsOfTypeAll<DialogBoxPanel02>();
                _instance = t[0];
            }

            return _instance;
        }
    }
    public Button next;
    protected Text dialogText;
    protected int nextPage = 1;

    NPC_Teacher npc;

    //显示方法
    public static void Show()
    {
        Instance.gameObject.SetActive(true);
    }

    //隐藏方法
    public static void Hide()
    {
        Instance.gameObject.SetActive(false);
    }


    protected virtual void Start()
    {
        dialogText = transform.Find("Dialog").GetComponent<Text>();
        var d_01 = DialogBoxManager.Instance.GetDialogById(70100);
        dialogText.text = d_01.DialogBox;
        var n = Resources.FindObjectsOfTypeAll<NPC_Teacher>();
        npc = n[0];
    }
    protected void OnEnable()
    {

        next.onClick.AddListener(NextDialog);
    }
    protected void OnDisable()
    {
        next.onClick.RemoveListener(NextDialog);
    }


    protected virtual void NextDialog()
    {
        next.transform.Find("Text").GetComponent<Text>().fontSize = 18;


        if (nextPage > 5)
        {
            Hide();
            npc.TalkingOver = true;
            return;
        }
        dialogText = transform.Find("Dialog").GetComponent<Text>();
        var d_01 = DialogBoxManager.Instance.GetDialogById(70100 + nextPage);
        dialogText.text = d_01.DialogBox;
        nextPage++;
        StartCoroutine(Fade());
    }
    protected IEnumerator Fade()
    {
        yield return new WaitForSeconds(0.1f);
        next.transform.Find("Text").GetComponent<Text>().fontSize = 14;
    }
}
