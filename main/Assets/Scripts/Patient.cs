using UnityEngine;

public class Patient : MonoBehaviour
{
    #region 宣告
    public Animator anim;
    [Header("主治醫生")]
    public Doctors_zero doctors;
    /// <summary>能否開啟題目 </summary>
    private bool isOpenTopic;
    #endregion

    #region 痛苦動畫開始
    /// <summary>
    /// 痛苦動畫開始
    /// </summary>
    public void onJitterStart()
    {
        anim.SetBool("Jitter", true);
    }
    #endregion

    #region 痛苦動畫結束
    /// <summary>
    /// 痛苦動畫開始
    /// </summary>
    public void onJitterEnd()
    {
        anim.SetBool("Jitter", false);
    }
    #endregion

    #region 進入觸發區
    void OnTriggerEnter2D(Collider2D evt)
    {
        if (doctors.npcData._TaskState != TaskState.ing) return;
        isOpenTopic = true;
    }
    #endregion

    #region 進入觸發區
    void OnTriggerExit2D(Collider2D evt)
    {
        if (doctors.npcData._TaskState != TaskState.ing) return;
        isOpenTopic = false;
    }
    #endregion

    #region 開啟題目
    /// <summary>
    /// 開啟題目
    /// </summary>
    public void btnOpenTopic()
    {
        if (!isOpenTopic) return;
        if (doctors.npcData._TaskState != TaskState.ing) return;
        doctors.GM.onChangePlayerStatePlot();
        doctors.topic.SetActive(true);
    }
    #endregion
}
