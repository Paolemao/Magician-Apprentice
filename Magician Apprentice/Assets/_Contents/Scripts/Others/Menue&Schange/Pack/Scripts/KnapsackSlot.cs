using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KnapsackSlot : Slot {

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        
        if (eventData.button == PointerEventData.InputButton.Right)//鼠标右键点击直接实现穿戴，不经过拖拽
        {
            if (transform.childCount > 0 && InventroyManager.Instance.IsPickedItem == false)//需要穿戴的物品得有，并且鼠标上要没有物品，否则就发生：当鼠标上有物品，在其他物品上点击鼠标右键也能穿上这种情况。
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();
                if (currentItemUI.Item is WeaponsData)//只有装备和物品才可以穿戴
                {

                    ItemData currentItem = currentItemUI.Item;//临时存储物品信息，防止物品UI销毁导致物品空指针
                    if (currentItemUI.Item.Id < 10010)
                    {
                        var weapons = Resources.FindObjectsOfTypeAll<Weapons>();
                        foreach (var weapon in weapons)
                        {
                            if (weapon.id == currentItemUI.Item.Id)//通过Id换装备
                            {
                                var t = Instantiate(weapon.gameObject);//实例化武器
                                t.transform.localScale = t.transform.localScale;
                                if (!GameController.Instance.Player.equipedAttackWeapon)//当手上没有装备时
                                {
                                    GameController.Instance.Player.equipedAttackWeapon = t.GetComponent<Weapons>();
                                    //初始化武器位置
                                    GameController.Instance.Player.equipedAttackWeapon.gameObject.transform.parent = GameController.Instance.Player.armorSlots[0];
                                    GameController.Instance.Player.equipedAttackWeapon.gameObject.transform.localPosition = new Vector3(0, 0, 0);
                                    GameController.Instance.Player.equipedAttackWeapon.gameObject.transform.localRotation = Quaternion.identity;
                                    StartCoroutine(Remove());
                                }
                                else if (!GameController.Instance.Player.UnequipedAttackWeapon)//当装备槽没有装备时
                                {
                                    Debug.Log("___________________________");
                                    t.transform.localScale = t.transform.localScale;
                                    GameController.Instance.Player.UnequipedAttackWeapon = t.GetComponent<Weapons>();
                                    GameController.Instance.Player.UnequipedAttackWeapon.gameObject.transform.parent = GameController.Instance.Player.armorSlots[1];
                                    GameController.Instance.Player.UnequipedAttackWeapon.gameObject.transform.localPosition = new Vector3(0, 0, 0);
                                    GameController.Instance.Player.UnequipedAttackWeapon.gameObject.transform.localRotation = Quaternion.identity;
                                    StartCoroutine(Remove());
                                }
                                else//替换手上的装备,交换信息
                                {
                                    var temp = GameController.Instance.Player.equipedAttackWeapon;//换下武器，存到临时位置
                                    Destroy(GameController.Instance.Player.equipedAttackWeapon.gameObject);

                                    t.transform.localScale = t.transform.localScale;
                                    GameController.Instance.Player.equipedAttackWeapon = t.GetComponent<Weapons>();//换上新武器
                                    //初始化武器位置
                                    GameController.Instance.Player.equipedAttackWeapon.gameObject.transform.parent = GameController.Instance.Player.armorSlots[0];
                                    GameController.Instance.Player.equipedAttackWeapon.gameObject.transform.localPosition = new Vector3(0, 0, 0);
                                    GameController.Instance.Player.equipedAttackWeapon.gameObject.transform.localRotation = Quaternion.identity;
                                    StartCoroutine(Remove());
                                    //在背包中存储换下的武器
                                    Knapsack.Instance.StoreItem(InventroyManager.Instance.GetItemById(temp.id));
                                }
                            }
                        }

                    }
                    else if (currentItemUI.Item.Id >= 10010 && currentItemUI.Item.Id < 10050)
                    {
                        var weapons = Resources.FindObjectsOfTypeAll<Weapons>();
                        foreach (var weapon in weapons)
                        {
                            if (weapon.id == currentItemUI.Item.Id)//通过Id换装备
                            {
                                var t = Instantiate(weapon.gameObject);//实例化武器
                                if (!GameController.Instance.Player.equipedAssistWeapon)//当手上没有装备时
                                {
                                    GameController.Instance.Player.equipedAssistWeapon = t.GetComponent<Weapons>();
                                    //初始化武器位置
                                    GameController.Instance.Player.equipedAssistWeapon.gameObject.transform.parent = GameController.Instance.Player.ItemSlot;
                                    GameController.Instance.Player.equipedAssistWeapon.gameObject.transform.localPosition = new Vector3(0, 0, 0);
                                    GameController.Instance.Player.equipedAssistWeapon.gameObject.transform.localRotation = Quaternion.identity;
                                    StartCoroutine(Remove());
                                }
                                else//替换手上的装备,交换信息
                                {
                                    var temp = GameController.Instance.Player.equipedAssistWeapon;//换下武器道具，存到临时位置
                                    Destroy(GameController.Instance.Player.equipedAssistWeapon.gameObject);

                                    t.transform.localScale = t.transform.localScale;
                                    GameController.Instance.Player.equipedAssistWeapon = t.GetComponent<Weapons>();//换上新武器道具
                                    //初始化武器位置
                                    GameController.Instance.Player.equipedAssistWeapon.gameObject.transform.parent = GameController.Instance.Player.ItemSlot;
                                    GameController.Instance.Player.equipedAssistWeapon.gameObject.transform.localPosition = new Vector3(0, 0, 0);
                                    GameController.Instance.Player.equipedAssistWeapon.gameObject.transform.localRotation = Quaternion.identity;
                                    StartCoroutine(Remove());
                                    //在背包中存储换下的武器/道具
                                    Knapsack.Instance.StoreItem(InventroyManager.Instance.GetItemById(temp.id));
                                }
                            }
                        }
                    }

                }
                else if (currentItemUI.Item is ConsumableData)
                {
                    ItemData currentItem = currentItemUI.Item;//临时存储物品信息，防止物品UI销毁导致物品空指针
                    //当角色在篝火状态下可加最大值的血量
                    var it = (ConsumableData)currentItemUI.Item;
                    if (GameController.Instance.Player.inCampfire)
                    {
                        GameController.Instance.Player.healthPoint += it.MaxHpPlus;
                        GameController.Instance.Player.MagicPoint += it.MaxMpPlus;
                    }
                    else
                    {
                        GameController.Instance.Player.healthPoint += it.MinHpPlus;
                        GameController.Instance.Player.MagicPoint += it.MinMpPlus;
                    }
                    currentItemUI.RemoveItemAmount(1);//当前物品槽中的物品减少1个
                    if (currentItemUI.Amount <= 0)//物品槽中的物品没有了
                    {
                        DestroyImmediate(currentItemUI.gameObject);//立即销毁物品槽中的物品
                        InventroyManager.Instance.HideToolTip();//隐藏该物品的提示框
                    }
                }
                else if (currentItemUI.Item is EquipmentData)//换装，在此游戏中只有执行一次的机会
                {
                    //新手教程对话框
                    
                    PlayerDialogBox.Show();
                    PlayerDialogBox.Instance.DialogTip(70205);

                    
                    ItemData currentItem = currentItemUI.Item;
                    if (currentItemUI.Item.Type==ItemType.Equipment)
                    {
                        var witch = GameController.Instance.Player.gameObject.transform.Find("Witch");
                        witch.Find("Peasant").gameObject.SetActive(false);
                        witch.Find("Witch_01").gameObject.SetActive(true);
                        StartCoroutine(IsWitch());
                    }

                    currentItemUI.RemoveItemAmount(1);//当前物品槽中的物品减少1个
                    if (currentItemUI.Amount <= 0)//物品槽中的物品没有了
                    {
                        DestroyImmediate(currentItemUI.gameObject);//立即销毁物品槽中的物品
                        InventroyManager.Instance.HideToolTip();//隐藏该物品的提示框
                    }
                }
            }
        }
    }
    IEnumerator Remove()
    {
        yield return null;
        ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();
        currentItemUI.RemoveItemAmount(1);//当前物品槽中的物品减少1个
        if (currentItemUI.Amount <= 0)//物品槽中的物品没有了
        {
            DestroyImmediate(currentItemUI.gameObject);//立即销毁物品槽中的物品
            InventroyManager.Instance.HideToolTip();//隐藏该物品的提示框
        }
    }
    IEnumerator IsWitch()
    {
        yield return new WaitForSeconds(0.5f);
        GameController.Instance.Player.isWitch = true;
    }
}
