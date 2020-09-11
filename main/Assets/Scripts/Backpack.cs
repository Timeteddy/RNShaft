using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Backpack : MonoBehaviour
{
    [Header("玩家")]
    public Player player;

    /// <summary>
    /// 準備放入背包的物品
    /// </summary>
    private string readlySetProps;

    /// <summary>
    /// 背包物品管理
    /// </summary>
    private string[] arrProps = new string[15];

    [Header("道具圖片")]
    /// <summary>
    /// 物品圖片
    /// </summary>
    public Sprite[] imgProps;

    [Header("背包道具顯示")]
    /// <summary>
    /// 背包物品顯示
    /// </summary>
    public Image[] imgBackProps;

    /// <summary>
    /// 背包物品的圖片字典
    /// </summary>
    private Dictionary<string, Sprite> propsImgMap = new Dictionary<string, Sprite>();

    /// <summary>
    /// 道具數量
    /// </summary>
    private int amount;

    private Button readlySetClean;

    void Awake()
    {
        amount = 0;

        propsImgMap.Add("3_1", imgProps[0]);
        propsImgMap.Add("3_2", imgProps[2]);
        propsImgMap.Add("3_3", imgProps[1]);
        propsImgMap.Add("3_4", imgProps[3]);
        propsImgMap.Add("3_5", imgProps[4]);
        propsImgMap.Add("11_1", imgProps[5]);
        propsImgMap.Add("11_2", imgProps[6]);
        propsImgMap.Add("11_3", imgProps[7]);
        propsImgMap.Add("11_4", imgProps[8]);
        propsImgMap.Add("11_5", imgProps[9]);
        propsImgMap.Add("11_6", imgProps[10]);
        propsImgMap.Add("11_7", imgProps[11]);
        propsImgMap.Add("11_8", imgProps[12]);
        propsImgMap.Add("11_9", imgProps[13]);
        propsImgMap.Add("11_10", imgProps[14]);

    }

    /// <summary>
    /// 打開背包
    /// </summary>
    public void onOpenBackPack()
    {
        int len = arrProps.Length;
        for (int i = 0; i < len; i++)
        {
            if (arrProps[i] == null) break;
            imgBackProps[i].sprite = propsImgMap[arrProps[i]];
            imgBackProps[i].color = new Color(255, 255, 255, 255);
        }
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
    /// 準備刪除的道具
    /// </summary>
    /// <param name="btn"></param>
    public void btnGetPropsClean(Button btn)
    {
        readlySetClean = btn;
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
        Destroy(readlySetClean.gameObject);
        player.finishGetProps();
    }
    /// <summary>
    /// 獲取準備放入背包的物品
    /// </summary>
    public string onGetReadlySetProps()
    {
        return readlySetProps;
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
