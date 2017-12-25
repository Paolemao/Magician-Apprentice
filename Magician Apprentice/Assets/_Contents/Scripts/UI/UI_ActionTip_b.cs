using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ActionTip_b : UiPanel<UI_ActionTip_b>
{

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
