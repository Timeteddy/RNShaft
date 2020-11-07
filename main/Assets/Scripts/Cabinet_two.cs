using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 櫃子
/// </summary>
public class Cabinet_two : MonoBehaviour
{
    #region 宣告
    /// <summary>主要畫面 </summary>
    public GameObject main;

    /// <summary>初始畫面 </summary>
    public GameObject original;
    public GameObject original_text;

    /// <summary>
    /// 0櫃子抽屜第1層
    /// 1櫃子抽屜第2層
    /// 2櫃子最上層
    /// 3桌面
    /// </summary>
    public GameObject[] arrBtnCabinet;

    public bool isOpenTopic;

    [Header("音效控制器")]
    public AudioSource audioS;
    [Header("選單")]
    public AudioClip mscOpenMenu;
    #endregion

    public void btnOpenWindow()
    {
        if (!isOpenTopic) return;
        audioS.PlayOneShot(mscOpenMenu, 1.0f);
        main.SetActive(true);
    }

    #region 關閉視窗
    public void btnClosed()
    {
        main.SetActive(false);
    }
    #endregion

    #region 開啟特定位置
    /// <summary>
    /// 開啟特定區域
    /// </summary>
    /// <param name="index">特定的區域</param>
    public void btnOpenPoint(int index)
    {
        original.SetActive(false);
        original_text.SetActive(false);
        arrBtnCabinet[index].SetActive(true);
        audioS.PlayOneShot(mscOpenMenu, 1.0f);
    }
    #endregion

    #region 回到初始位置
    /// <summary>
    /// 關閉特定區域
    /// </summary>
    /// <param name="index">特定的區域</param>
    public void btnClosedPoint(int index)
    {
        original.SetActive(true);
        original_text.SetActive(true);
        arrBtnCabinet[index].SetActive(false);
    }
    #endregion

    #region 進入觸發區
    void OnTriggerEnter2D(Collider2D evt)
    {
        isOpenTopic = true;
    }
    #endregion

    #region 離開觸發區
    void OnTriggerExit2D(Collider2D evt)
    {
        isOpenTopic = false;
    }
    #endregion

}
