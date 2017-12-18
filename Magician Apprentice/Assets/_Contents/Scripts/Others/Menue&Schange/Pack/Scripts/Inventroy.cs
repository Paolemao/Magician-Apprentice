using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
/// <summary>
/// 存活类，背包和战利品袋子的基类
/// </summary>
public class Inventroy : MonoBehaviour
{

    //存放物品槽的数组
    protected Slot[] slotArray;

    

    //控制背包的显示和隐藏的相关变量
    private float targetAlpha = 1f;//显示目标值
    private float smoothing = 4f;//渐变平滑速度

    private CanvasGroup canvasGroupMy;//对CanvasGroup的引用，用于制作隐藏和现实效果

    public CanvasGroup CanvasGroupMy
    {
        get
        {
            if (canvasGroupMy==null)
            {
                canvasGroupMy = GetComponent<CanvasGroup>();
            }
            return canvasGroupMy;
        }
    }
    
    public virtual void Start()
    {
        if (canvasGroupMy==null)
        {
            canvasGroupMy = GetComponent<CanvasGroup>();
        }
    }
    protected void Update()
    {
        //处理渐变
        if (this.CanvasGroupMy.alpha!=targetAlpha)
        {
            this.CanvasGroupMy.alpha = Mathf.Lerp(this.CanvasGroupMy.alpha,targetAlpha,smoothing*Time.deltaTime);
            if (Mathf.Abs(this.CanvasGroupMy.alpha-targetAlpha)<0.01f)
            {
                this.CanvasGroupMy.alpha = targetAlpha;
            }
        }
    }

    //根据ID存储物品
    public bool StoreItem(int id)
    {
        ItemData item = InventroyManager.Instance.GetItemById(id);
        return StoreItem(item);
    }

    //根据ItemData存储物品
    public bool StoreItem(ItemData item)
    {
        if (item.Id==0)
        {
            Debug.LogWarning("要存储的物品Id不存在");
            return false;
        }
        if (item.Capaticy == 1)//如果此物品只能放一个，那就找一个空的物品槽来存放即可
        {
            Slot slot = FindEmptySlot();
            if (slot == null)//如果空的物品槽没了
            {
                Debug.LogWarning("没有空的物品槽可使用了");
                return false;//存储失败
            }
            else
            {
                slot.StoreItem(item);
            }
        }
        else//如果此物能放多个
        {
            Slot slot = FindSameIDSlot(item);
            if (slot != null)//找到符合条件的物品槽，就把物品存起来
            {
                slot.StoreItem(item);
            }
            else//没有找到符合条件的物品槽，那就找一个没有存放物品的物品槽去存放物品
            {
                Slot emptySlot = FindEmptySlot();
                if (emptySlot != null)
                {
                    emptySlot.StoreItem(item);//放到空的物品槽中
                }
                else
                {
                    Debug.LogWarning("没有空的物品槽可供使用");
                    return false;
                }
            }
        }
        return true;
    }

    //寻找空的物品槽
    private Slot FindEmptySlot()
    {
        foreach (Slot slot in slotArray)
        {
            if (slot.transform.childCount==0)//物品槽下面无子类，说明该物品槽为空
            {
                return slot;
            }
        }
        return null;
    }

    //查找存放的物品是相同的物品槽
    private Slot FindSameIDSlot(ItemData item)
    {
        foreach (Slot slot in slotArray)
        {
            //如果当前的物品槽已经有物品了，并且里面的物品类型和需要找的一样，并且物品槽没有被填满
            if (slot.transform.childCount>=1&&item.Id==slot.GetItemID()&&slot.isFiled()==false)
            {
                return slot;
            }
        }
        return null;
    }


    //控制物品信息的保存（ID，Amount数量）
    public void SaveInventory()
    {
        StringBuilder sb = new StringBuilder();//用于保存物品信息的字符串
        foreach (Slot slot in slotArray)
        {
            if (slot.transform.childCount > 0)
            {
                ItemUI itemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
                sb.Append(itemUI.Item.Id + "," + itemUI.Amount + "-");//用逗号分隔一个物品中的ID和数量，用 - 分隔多个物品
            }
            else
            {
                sb.Append("0-");//如果物品槽里没有物品就是0；
            }
        }
        PlayerPrefs.SetString(this.gameObject.name,sb.ToString());//本地保存字符串数据
    }
    //控制物品信息的加载（根据ID，Amount数量）
    public void LoadInventory()
    {
        if (PlayerPrefs.HasKey(this.gameObject.name) == false) return;//判断保存的这个关键key是否存在
        string str = PlayerPrefs.GetString(this.gameObject.name);//获取上面保存的字符串数据
        string[] itemArry = str.Split('-');//按照-来分隔多个物品
        for (int i=0;i<itemArry.Length-1;i++)//长度减一是因为最后一个字符是-
        {
            string itemStr = itemArry[i];
            if (itemStr!="0")
            {
                string[] temp = itemStr.Split(',');//按照逗号分割这个物品的信息（ID和Amount）
                int id = int.Parse(temp[0]);
                ItemData item = InventroyManager.Instance.GetItemById(id);//通过物体的ID得到该物体
                int amount = int.Parse(temp[1]);
                for (int j=0;j<amount;j++)//一个个存
                {
                    slotArray[i].StoreItem(item);
                }
            }
        }
    }
}

