using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("獲勝")]
    public AudioClip mscWin;
    [Header("失敗_1")]
    public AudioClip mscLose_1;
    [Header("失敗_2")]
    public AudioClip mscLose_2;
    #endregion

    #region 播放過場動畫(進入房間)
    /// <summary>播放過場動畫(進入房間) </summary>
    public void onAnimPlayPassIntoRoom()
    {
        GM.btnMscDoor();
        isIntoRoom = true;
        anim.SetTrigger("PassDoor");
    }
    #endregion

    #region 播放過場動畫(離開房間)
    /// <summary>播放過場動畫(離開房間) </summary>
    public void onAnimPlayPassLeventRoom()
    {
        GM.btnMscDoor();
        isIntoRoom = false;
        anim.SetTrigger("PassDoor");
    }
    #endregion

    #region 開始過場(動畫標籤)
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

    #region 失敗動畫 第二段(動畫標籤)
    public void animTagLose()
    {
        GM.audioS.PlayOneShot(mscLose_2, 1.0f);
    }
    #endregion

    #region 播放獲勝動畫
    /// <summary>播放獲勝動畫 </summary>
    public void onAnimPlayWin()
    {
        GM.audioS.PlayOneShot(mscWin, 1.0f);
        anim.SetTrigger("GameWin");
    }
    #endregion

    #region 播放失敗動畫
    /// <summary>播放失敗動畫 </summary>
    public void onAnimPlayLose()
    {
        GM.audioS.PlayOneShot(mscLose_1, 1.0f);
        anim.SetTrigger("GameLost");
    }
    #endregion

    #region 按鈕，重新開始遊戲
    public void btnReplay()
    {
        SceneManager.LoadScene(0);
    }
    #endregion

    #region 按鈕，離開遊戲
    public void btnExit()
    {
        Application.Quit();
    }
    #endregion

}
