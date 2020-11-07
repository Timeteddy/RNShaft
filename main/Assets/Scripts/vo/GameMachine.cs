using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public delegate void typewriterStart();
public delegate void typewriterEnd();

/// <summary>
/// 遊戲常用功能
/// </summary>
public class GameMachine : MonoBehaviour
{
    #region 宣告
    /// <summary>
    /// 打字機效果開始
    /// </summary>
    public static event typewriterStart SE_TYPWRTR_START;

    /// <summary>
    /// 打字機效果結束
    /// </summary>
    public static event typewriterEnd SE_TYPWRTR_END;

    public static GameMachine instance;

    /// <summary>
    /// 能否點擊跳過
    /// </summary>
    private static bool isHitJumpOver = true;

    /// <summary>
    /// 打字速度
    /// </summary>
    private static float typwrtrSpeed = 0.1f;
    /// <summary>正常打字速度 </summary>
    private static float flaNormalSpeed = 0.1f;
    /// <summary>略過打字速度 </summary>
    private static float flaHitJumOver = 0.0f;
    [Header("音效控制器")]
    public AudioSource audioS;
    [Header("打字音效")]
    public AudioClip mscKey;
    #endregion

    #region 起始
    void Awake()
    {
        instance = this;
    }
    #endregion

    #region 重複
    private void Update()
    {
        onClickMouseDown();
    }
    #endregion

    #region 點擊滑鼠或畫面
    /// <summary>
    /// 點擊滑鼠或畫面
    /// </summary>
    private void onClickMouseDown()
    {
        if (!isHitJumpOver) return;
        if (Input.GetMouseButtonDown(0))
        {
            typwrtrSpeed = flaHitJumOver;
        }
    }
    #endregion

    #region 打字機
    /// <summary>
    /// 打字機
    /// </summary>
    /// <param name="text">要放入的text</param>
    /// <param name="str">顯示的字</param>
    public static void Typewriter(Text text, string str)
    {
        SE_TYPWRTR_START();
        typwrtrSpeed = flaNormalSpeed;
        instance.StartCoroutine(startTypewriter(text, str));
    }
    #endregion

    #region 打字機本體
    /// <summary>
    /// 打字機本體
    /// </summary>
    /// <param name="text"></param>
    /// <param name="str"></param>
    static IEnumerator startTypewriter(Text text, string str)
    {
        string dialog = str;
        text.text = "";
        int len = dialog.Length;
        for (int i = 0; i < len; i++)
        {
            text.text += dialog[i];
            //加入音效
            if(typwrtrSpeed != flaHitJumOver)
            {
                instance.audioS.PlayOneShot(instance.mscKey, 0.5f);
            }
            yield return new WaitForSeconds(typwrtrSpeed);
            isHitJumpOver = true;
        }

        SE_TYPWRTR_END();
        isHitJumpOver = false;
        yield return null;
    }
    #endregion

}
