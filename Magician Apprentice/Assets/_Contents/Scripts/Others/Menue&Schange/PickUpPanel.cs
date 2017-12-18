using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class PickUpPanel : UiPanel<PickUpPanel> {

    //周围的内容和模板
    public Transform aroundContent;
    public Transform itemtemplate;

    //清单槽（图片）
    public Image[] inventorySolt;

    public List<Transform> aroundItem = new List<Transform>();

    private void Start()
    {
        Hide();
        itemtemplate.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        //清空
        for (int i = 0; i < aroundContent.childCount; i++)
        {
            Destroy(aroundContent.GetChild(i).gameObject);
        }
        var player = GameController.Instance.Player;
        var rets = Physics.OverlapSphere(player.transform.position, 2);

        foreach (var ret in rets)
        {
            //假如有战利品
            if (ret.tag == "Loot")
            {
                Transform loot = ret.transform;
                for (int i = 0; i < loot.childCount; i++)
                {
                    var weapon = loot.GetChild(i).GetComponent<Weapons>();

                    //确认其不在包中
                    if (weapon && !weapon.isInInventory)
                    {
                        //实例化模板
                        var item = Instantiate(itemtemplate);


                        //给模板加对应的名字
                        item.GetComponentInChildren<Text>().text = weapon.data.Name;

                        //获取图集，给模板加对应的图片
                        var atlas = Resources.Load<SpriteAtlas>("Icon/"/* + weapon.data.Atlas*/);
                        item.GetChild(0).GetComponent<Image>().sprite = atlas.GetSprite(weapon.data.IconName);
                        item.transform.SetParent(aroundContent);
                        item.gameObject.SetActive(true);

                    }
                }
            }
        }
    }

    private void Update()
    {
       
    }


}
