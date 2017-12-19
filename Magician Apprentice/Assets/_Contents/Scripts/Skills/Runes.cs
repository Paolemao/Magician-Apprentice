using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runes : MonoBehaviour {

    public int id;
    // Use this for initialization
    private void Awake()
    {
        InventroyManager.Instance.GetItemById(id);
    }

}
