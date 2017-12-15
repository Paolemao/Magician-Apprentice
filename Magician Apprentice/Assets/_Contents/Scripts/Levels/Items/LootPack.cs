using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPack : MonoBehaviour {

    public List<GameObject> loots;
    private void OnEnable()
    {
        foreach (var l in loots)
        {
            var loot = Instantiate(l);
            loot.transform.parent = transform;
            //loot.SetActive(false);
        }
    }
}
