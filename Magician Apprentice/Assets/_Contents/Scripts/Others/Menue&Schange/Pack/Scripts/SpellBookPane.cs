using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBookPane : UiPanel<SpellBookPane> {
    public Button close;
    private void OnEnable()
    {
        close.onClick.AddListener(Hide);
    }
    private void OnDisable()
    {
        close.onClick.RemoveListener(Hide);
    }
}
