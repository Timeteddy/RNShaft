using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimScene : MonoBehaviour
{
    #region 宣告
    [Header("玩家")]
    public Player player;
    [Header("遊戲控制器")]
    public GameManager GM;
    [Header("動畫控制器")]
    public Animator anim;
    /// <summary>是否為進入房間 </summary>
    private bool isIntoRoom;
    #endregion

    #region 播放過場動畫(進入房間)
    /// <summary>播放過場動畫(進入房間) </summary>
    public void onAnimPlayPassIntoRoom()
    {
        isIntoRoom = true;
        anim.SetTrigger("PassDoor");
    }
    #endregion

    #region 播放過場動畫(離開房間)
    /// <summary>播放過場動畫(離開房間) </summary>
    public void onAnimPlayPassLeventRoom()
    {
        isIntoRoom = false;
        anim.SetTrigger("PassDoor");
    }
    #endregion

    #region 開始過場
    /// <summary>正式過場 </summary>
    public void animTagPass()
    {
        if (isIntoRoom)
        {
            GM.onPassIntoRoom();
        }
        else
        {
            GM.onPassLeventRoom();
        }
    }
    #endregion

    #region 播放獲勝動畫
    /// <summary>播放獲勝動畫 </summary>
    public void onAnimPlayWin()
    {

    }
    #endregion

    #region 播放失敗動畫
    /// <summary>播放失敗動畫 </summary>
    public void onAnimPlayLose()
    {

    }
    #endregion

}
