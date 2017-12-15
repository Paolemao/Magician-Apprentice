using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ActionTips : UiPanel<UI_ActionTips> {

    private void OnEnable()
    {
        StartCoroutine(Hiding());
    }
    IEnumerator Hiding()
    {
        yield return new WaitForSeconds(2f);
        Hide();

    }
}
