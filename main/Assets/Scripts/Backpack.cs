using UnityEngine;
using UnityEngine.UI;

public class Backpack : MonoBehaviour
{
    /// <summary>
    /// 準備放入背包的物品
    /// </summary>
    private string readlySetProps;

    /// <summary>
    /// 背包物品管理
    /// </summary>
    private string[] arrProps = new string[15];

    /// <summary>
    /// 物品圖片
    /// </summary>
    public Sprite[] imgProps;

    /// <summary>
    /// 道具數量
    /// </summary>
    private int amount;

    void Awake()
    {
        amount = 0;
    }

    /// <summary>
    /// 打開背包
    /// </summary>
    public void onOpenBackPack()
    {

    }

    /// <summary>
    /// 關閉背包
    /// </summary>
    public void onCloseBackPack()
    {

    }

    /// <summary>
    /// 獲取道具
    /// </summary>
    /// <param name="name"></param>
    public void btnGetProps(string name)
    {
        readlySetProps = name;
    }

    /// <summary>
    /// 放棄道具
    /// </summary>
    public void onForgoProps()
    {
        readlySetProps = null;
    }

    /// <summary>
    /// 將道具放入背包
    /// </summary>
    public void onPutBackpack()
    {
        if (readlySetProps == null) return;
        arrProps[amount] = readlySetProps;
        amount++;
    }

    /// <summary>
    /// 將物品標記拿出標籤
    /// </summary>
    public void btnForgoType()
    {

    }

    /// <summary>
    /// 確認拿出道具
    /// </summary>
    public void btnEnterForgo()
    {

    }
}
