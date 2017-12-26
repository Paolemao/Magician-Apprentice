using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDialogBox : MonoBehaviour {

    //单例
    private static PlayerDialogBox _instance;
    public Button next;
    protected Text dialogText;
    protected int nextPage = 1;

    public static PlayerDialogBox Instance
    {
        get
        {
            if (_instance == null)
            {
                var t = Resources.FindObjectsOfTypeAll<PlayerDialogBox>();
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
    private void Awake()
    {
        dialogText = transform.Find("Dialog").GetComponent<Text>();
    }

    protected virtual void Start()
    {
       // dialogText = transform.Find("Dialog").GetComponent<Text>();
        //var d_01 = DialogBoxManager.Instance.GetDialogById(70200);
        //dialogText.text = d_01.DialogBox;
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
        StartCoroutine(Fade());
        Hide();

        //if (nextPage > 1)
        //{
        //    Hide();

        //   //GameOver.Show();
        //   //SceneManager.LoadScene("FinalScene");
        //    return;
        //}


    }
    protected IEnumerator Fade()
    {
        yield return new WaitForSeconds(0.1f);
        next.transform.Find("Text").GetComponent<Text>().fontSize = 14;
    }

    public void DialogTip(int dialogId)//通过对话框id找到指定的对话
    {
        var dialog = DialogBoxManager.Instance.GetDialogById(dialogId);
        dialogText.text = dialog.DialogBox;
    }
}
