using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBoxPanel02 : DialogBoxPanel {

    protected override void Start()
    {
        dialogText = transform.Find("Dialog").GetComponent<Text>();
        var d_01 = DialogBoxManager.Instance.GetDialogById(70100);
        dialogText.text = d_01.DialogBox;
    }

    protected override void NextDialog()
    {
        next.transform.Find("Text").GetComponent<Text>().fontSize = 18;


        if (nextPage > 5)
        {
            Hide();
            return;
        }
        dialogText = transform.Find("Dialog").GetComponent<Text>();
        var d_01 = DialogBoxManager.Instance.GetDialogById(70100 + nextPage);
        dialogText.text = d_01.DialogBox;
        nextPage++;

        StartCoroutine(Fade());
    }
}
