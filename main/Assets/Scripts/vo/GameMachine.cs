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
    /// 打字速度
    /// </summary>
    private static float typwrtrSpeed = 0.1f;

    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 打字機
    /// </summary>
    /// <param name="text">要放入的text</param>
    /// <param name="str">顯示的字</param>
    public static void Typewriter(Text text, string str)
    {
        SE_TYPWRTR_START();
        instance.StartCoroutine(startTypewriter(text, str));
    }

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
            yield return new WaitForSeconds(typwrtrSpeed);
        }

        SE_TYPWRTR_END();
        yield return null;
    }

}
