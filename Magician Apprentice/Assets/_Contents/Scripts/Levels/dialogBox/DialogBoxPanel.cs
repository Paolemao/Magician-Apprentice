using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 对话盒
/// </summary>
public class DialogBoxPanel : MonoBehaviour {

    //单例
    private static DialogBoxPanel _instance;
    public Button next;
    protected Text dialogText;
    protected int nextPage = 1;

    public static DialogBoxPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                var t = Resources.FindObjectsOfTypeAll<DialogBoxPanel>();
                _instance = t[0];
            }

            return _instance;
        }
    }

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
        var d_01 = DialogBoxManager.Instance.GetDialogById(70001);
        dialogText.text = d_01.DialogBox;
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
        next.transform.Find("Text").GetComponent<Text>().fontSize=18;


        if (nextPage>1)
        {
            Hide();
            return;
        }
        dialogText = transform.Find("Dialog").GetComponent<Text>();
        var d_01 = DialogBoxManager.Instance.GetDialogById(70001+nextPage);
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
