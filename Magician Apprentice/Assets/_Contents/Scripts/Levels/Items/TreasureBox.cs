using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour {

    public GameObject BoxOpen;
    public GameObject BoxClose;
    //public GameObject Loot;
    public List<Weapons> weapons;
    public List<Items> items;
    public List<Equipment> equipments;

    // Use this for initialization
    private void Awake()
    {


    }
    void Start () {
        DataUp();
        BoxOpen.SetActive(false);
        BoxClose.SetActive(true);
        //Loot.SetActive(false);
}

    private void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {

            if (other.GetComponent<PlayerCharacter>().OpenBox)
            {
                Debug.Log("+++++++++++++++++++++");
                BoxOpen.SetActive(true);
                BoxClose.SetActive(false);
                LootChest.Show();
                //Loot.SetActive(true);
                //Loot.transform.localPosition = new Vector3(0,other.transform.position.y,0);
                //Loot.transform.rotation = Quaternion.Euler(0,0,30);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            LootChest.Hide();
        }
    }
    void DataUp()
    {
        if (weapons != null)
        {
            foreach (var weapon in weapons)
            {
                LootChest.Instance.StoreItem(weapon.id);
            }
        }
        if (items != null)
        {
            foreach (var item in items)
            {
                LootChest.Instance.StoreItem(item.id);
            }
        }
        if (equipments != null)
        {
            foreach (var equipment in equipments)
            {
                LootChest.Instance.StoreItem(equipment.id);
            }
        }
    }
}
