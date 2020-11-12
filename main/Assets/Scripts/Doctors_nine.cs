using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 醫生0
/// </summary>
public class Doctors_nine : NPC
{
    #region 宣告
    [Header("負責的病人")]
    public Patient_nine patint;

    [Header("題目")]
    public GameObject topic;

    /// <summary>痛苦反應時間 </summary>
    private float fltPainfulReaction = 0.5f;
    [Header("驚嘆號")]
    public Sprite imgMarvel;
    [Header("問號")]
    public Sprite imgQuestion;

    /// <summary>玩家的答案 </summary>
    [SerializeField]
    private int[] arrAnswer = new int[4];
    [Header("答案按鈕")]
    public Button[] arrBtnAnswer;
    /// <summary>能否說話 </summary>
    private bool isTock = true;
    #endregion

    #region 啟動
    void Awake()
    {
        dlgeSchedule = 0;

        npcData._TaskState = TaskState.start;

        myCamera.SE_FLLW_START += cameraFollowStart;
        myCamera.SE_FLLW_END += cameraFollowEnd;

        GameMachine.SE_TYPWRTR_START += typewriterStart;
        GameMachine.SE_TYPWRTR_END += typewriterEnd;

        PlotControl.SE_ROOM_START += plotSeRoomStart;
        PlotControl.SE_ROOM_ING += plotSeRoomIng;
        PlotControl.SE_ROOM_END += plotSeRoomEnd;

        resetAnswer();
    }
    #endregion

    #region 重複
    void Update()
    {
        if(isNexDialogue && isTock)
        {
            btnDialogue.interactable = true;
        }
        else
        {
            btnDialogue.interactable = false;
        }
    }
    #endregion

    #region 按鈕，對話系統
    public void btnDialogueSysetm()
    {
        switch (npcData._TaskState)
        {
            case TaskState.start:
                if (dlgeSchedule >= npcData.start.Length)
                {
                    npcData._TaskState = TaskState.ing;
                    dlge.onDisplayWindow(false);
                    dlge.setName(null);
                    dlgeSchedule = 0;
                    GM.onReturnControl();
                    btnDialogue.gameObject.SetActive(false);
                    return;
                }
                else if (dlgeSchedule == 1)  //劇情演示
                {
                    patint.onJitterStart();
                }

                dlge.setConten(npcData.start[dlgeSchedule]);
                break;
            case TaskState.ing:
                if (dlgeSchedule >= npcData.ing.Length)
                {
                    dlge.onDisplayWindow(false);
                    dlge.setName(null);
                    dlgeSchedule = 0;
                    GM.onReturnControl();
                    btnDialogue.gameObject.SetActive(false);
                    return;
                }
                dlge.setConten(npcData.ing[dlgeSchedule]);
                break;
            case TaskState.lose:
                if (dlgeSchedule >= npcData.lose.Length)
                {
                    dlge.onDisplayWindow(false);
                    dlge.setName(null);
                    dlgeSchedule = 0;
                    GM.onReturnControl();
                    btnDialogue.gameObject.SetActive(false);
                    return;
                }
                dlge.setConten(npcData.lose[dlgeSchedule]);
                break;
            case TaskState.finished:
                if (dlgeSchedule >= npcData.finshed.Length)
                {
                    isTock = false;
                    if (!GM.getArrTaskSchedule(9))
                    {
                        GM.finallyTask(9);
                    }
                    dlge.onDisplayWindow(false);
                    dlge.setName(null);
                    dlgeSchedule = 0;
                    GM.onReturnControl();
                    symbol.gameObject.SetActive(false);
                    btnDialogue.gameObject.SetActive(false);
                    GM.onSetReadlyDialogue("");

                    StartCoroutine(plotPressentationFirstAct());
                    return;
                }
                dlge.setConten(npcData.finshed[dlgeSchedule]);
                break;
            default:
                break;
        }
    }
    #endregion

    #region (按鈕)設定準備對話的對象
    /// <summary>
    /// 設定準備對話的對象
    /// </summary>
    public void btnSetReadlyDialogue()
    {
        GM.onSetReadlyDialogue("DcotorsNine");
    }
    #endregion

    #region 進入房間劇情控制
    /// <summary>進入房間劇情開始</summary>
    public void plotSeRoomStart()
    {
        if (GM.onGetSceneState() != SceneState.nineStart) return;

        anim.SetTrigger("nurse_posture_wave0");
        symbol.gameObject.SetActive(true);
    }

    /// <summary>進入房間劇情中</summary>
    public void plotSeRoomIng()
    {
    }

    /// <summary>進入房間劇情結束</summary>
    public void plotSeRoomEnd()
    {
    }
    #endregion

    #region 攝影機跟隨開始
    /// <summary>
    /// 攝影機跟隨開始
    /// </summary>
    private void cameraFollowStart()
    {

    }
    #endregion

    #region 攝影機跟隨結束
    /// <summary>
    /// 攝影機跟隨結束
    /// </summary>
    private void cameraFollowEnd()
    {
        if (GM.onGetSceneState() != SceneState.nineStart) return;
        if (npcData._TaskState != TaskState.finished) return;

        symbol.sprite = imgQuestion;
    }
    #endregion

    #region 打字效果開始
    /// <summary>
    /// 打字效果開始
    /// </summary>
    private void typewriterStart()
    {
        isNexDialogue = false;
    }
    #endregion

    #region 打字效果結束
    /// <summary>
    /// 打字效果結束
    /// </summary>
    private void typewriterEnd()
    {
        if (GM.onGetDialoguePeople() != "DcotorsNine") return;

        switch (npcData._TaskState)
        {
            case TaskState.start:
                dlgeSchedule++;
                break;
            case TaskState.ing:
                dlgeSchedule++;
                break;
            case TaskState.lose:
                dlgeSchedule++;
                break;
            case TaskState.finished:
                dlgeSchedule++;
                break;
            default:
                break;
        }

        isNexDialogue = true;
    }
    #endregion

    #region 開始對話
    /// <summary>
    /// 開始對話
    /// </summary>
    public void onStartDialogue()
    {
        if (!isTock) return;
        btnDialogue.gameObject.SetActive(true);
        dlge.onDisplayWindow(true);
        dlge.setName(npcData._name);

        switch (npcData._TaskState)
        {
            case TaskState.start:
                if (dlgeSchedule == 0)
                {
                    patint.onJitterStart();
                }
                dlge.setConten(npcData.start[dlgeSchedule]);
                break;
            case TaskState.ing:
                if (dlgeSchedule >= npcData.ing.Length) 
                    dlgeSchedule = 0;
                dlge.setConten(npcData.ing[dlgeSchedule]);
                break;
            case TaskState.lose:
                if (dlgeSchedule >= npcData.lose.Length)
                    dlgeSchedule = 0;
                dlge.setConten(npcData.lose[dlgeSchedule]);
                break;
            case TaskState.finished:
                if (dlgeSchedule >= npcData.finshed.Length)
                    dlgeSchedule = 0;
                dlge.setConten(npcData.finshed[dlgeSchedule]);
                break;
            default:
                break;
        }
    }
    #endregion

    #region 答案重置
    /// <summary>答案重置 </summary>
    private void resetAnswer()
    {
        for (int i = 0; i < arrAnswer.Length; i++)
        {
            arrAnswer[i] = 0;
            arrBtnAnswer[i].interactable = true;
        }
        arrAnswer[0] = -1;
        arrAnswer[1] = -1;
        arrAnswer[3] = -1;
    }
    #endregion

    #region 按鈕，取消回答
    /// <summary>按鈕，取消回答 </summary>
    public void btnAnswerClose()
    {
        resetAnswer();
    }
    #endregion

    #region 按鈕,回答問題
    /// <summary>
    /// 回答問題
    /// </summary>
    public void btnAnswerQuestion(int answer)
    {
        switch (answer)
        {
            case 0:         //選擇正確答案
                arrAnswer[answer] = 1;
                break;
            case 1:         //選擇正確答案
                arrAnswer[answer] = 1;
                break;
            case 3:         //選擇正確答案
                arrAnswer[answer] = 1;
                break;
            default:        //選擇錯誤答案
                arrAnswer[answer] = -1;
                break;
        }
        arrBtnAnswer[answer].interactable = false;
        isNexDialogue = false;
        dlgeSchedule = 0;
    }
    #endregion

    #region 按鈕，確認
    /// <summary>
    /// 確認問題
    /// </summary>
    public void btnEnter()
    {
        isNexDialogue = false;
        dlgeSchedule = 0;
        topic.SetActive(false);
        //當發現回答的內容有錯誤的答案時
        for (int i = 0; i < arrAnswer.Length; i++)
        {
            if (arrAnswer[i] == -1)
            {
                npcData._TaskState = TaskState.lose;
                GM.animScene.onAnimPlayLose();
                return;
            }
        }

        //如果沒有錯誤的答案
        npcData._TaskState = TaskState.finished;
        StartCoroutine(plotPressentationSecondAct());
    }
    #endregion

    #region 劇情, 開始治療
    /// <summary>
    /// 劇情第一幕
    /// </summary>
    /// <returns></returns>
    IEnumerator plotPressentationFirstAct()
    {
        isNexDialogue = false;
        yield return new WaitForSeconds(fltPainfulReaction);
        anim.SetBool("nurse_run_left", true);   //向左看
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("nurse_run_left", false);

        yield return new WaitForSeconds(1.0f);
        anim.SetBool("nurse_action_treatment", true);
    }
    #endregion

    #region 劇情, 過關
    /// <summary>
    /// 劇情第一幕
    /// </summary>
    /// <returns></returns>
    IEnumerator plotPressentationSecondAct()
    {
        GM.onChangePlayerStatePlot();
        patint.onJitterEnd();
        isNexDialogue = false;
        yield return new WaitForSeconds(2.0f);
        GM.myCamera.onCheckInStart(transform);
    }
    #endregion

}
