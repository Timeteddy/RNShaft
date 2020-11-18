using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 醫生0
/// </summary>
public class Doctors_two : NPC
{
    #region 宣告
    [Header("負責的病人")]
    public Patient_two patint;

    [Header("題目")]
    public GameObject topic;

    /// <summary>痛苦反應時間 </summary>
    private float fltPainfulReaction = 0.5f;
    [Header("驚嘆號")]
    public Sprite imgMarvel;
    [Header("問號")]
    public Sprite imgQuestion;
    [Header("道具按鈕")]
    public Button[] arrBtnProps;
    [Header("提示")]
    public GameObject prompt;
    [Header("給予視窗")]
    public GameObject giveWindow;
    /// <summary>可以給予道具 </summary>
    public bool isGive = false;

    /// <summary>玩家的答案 </summary>
    [SerializeField]
    private int[] arrAnswer = new int[6];
    [Header("答案按鈕")]
    public Button[] arrBtnAnswer;
    /// <summary>完家給予的道具 </summary>
    [SerializeField]
    private int intAnswer;
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

        for (int i = 0; i < arrAnswer.Length; i++)
        {
            arrAnswer[i] = -1;
            arrBtnAnswer[i].interactable = true;
        }
        intAnswer = 0;
    }
    #endregion

    #region 重複
    void Update()
    {
        btnDialogue.interactable = isNexDialogue;
        //if (GM.onGetDialoguePeople() != "DcotorsTwo") return;
        //onClickMouseDown();
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
                    if (!GM.getArrTaskSchedule(2))
                    {
                        GM.finallyTask(2);
                    }
                    dlge.onDisplayWindow(false);
                    dlge.setName(null);
                    dlgeSchedule = 0;
                    GM.onReturnControl();
                    symbol.gameObject.SetActive(false);
                    btnDialogue.gameObject.SetActive(false);
                    GM.onSetReadlyDialogue("");
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
        GM.onSetReadlyDialogue("DcotorsTwo");
    }
    #endregion

    #region 進入房間劇情控制
    /// <summary>進入房間劇情開始</summary>
    public void plotSeRoomStart()
    {
        if (GM.onGetSceneState() != SceneState.twoStart) return;

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
        if (GM.onGetSceneState() != SceneState.twoStart) return;
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
        if (GM.onGetDialoguePeople() != "DcotorsTwo") return;

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
        if (npcData._TaskState != TaskState.ing)
        {
            btnDialogue.gameObject.SetActive(true);
            dlge.onDisplayWindow(true);
            dlge.setName(npcData._name);
        }

        switch (npcData._TaskState)
        {
            case TaskState.start:
                dlge.setConten(npcData.start[dlgeSchedule]);
                break;
            case TaskState.ing:
                giveWindow.SetActive(true);
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

    #region 按鈕,回答問題
    /// <summary>
    /// 回答問題
    /// </summary>
    public void btnAnswerQuestion(int answer)
    {
        if (!isGive) return;

        switch (answer)
        {
            case 0:       //過關
                arrAnswer[0] = 1;
                break;
            case 2:       //過關
                arrAnswer[1] = 1;
                break;
            case 3:       //過關
                arrAnswer[2] = 1;
                break;
            case 4:       //過關
                arrAnswer[3] = 1;
                break;
            case 11:       //過關
                arrAnswer[4] = 1;
                break;
            case 12:       //過關
                arrAnswer[5] = 1;
                break;
            case 1:       //失敗
                arrAnswer[0] = -1;
                break;
            case 5:       //失敗
                arrAnswer[1] = -1;
                break;
            case 6:       //失敗
                arrAnswer[2] = -1;
                break;
            case 7:       //失敗
                arrAnswer[3] = -1;
                break;
            case 8:       //失敗
                arrAnswer[4] = -1;
                break;
            case 9:       //失敗
                arrAnswer[5] = -1;
                break;
            case 10:       //失敗
                arrAnswer[0] = -1;
                break;
            case 13:       //失敗
                arrAnswer[1] = -1;
                break;
            case 14:       //失敗
                arrAnswer[2] = -1;
                break;
        }
        isNexDialogue = false;
        dlgeSchedule = 0;
        intAnswer++;
    }
    #endregion

    #region 關閉自己
    public void btnInteranctableMySelf(Button mySelf)
    {
        if (!isGive) return;
        mySelf.interactable = false;
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
        //發現回答的問題過多時
        if(intAnswer != arrAnswer.Length)
        {
            npcData._TaskState = TaskState.lose;
            GM.animScene.onAnimPlayLose();
            return;
        }

        //如果沒有錯誤的答案
        npcData._TaskState = TaskState.finished;
        StartCoroutine(plotPressentationSecondAct());
    }
    #endregion

    #region 按鈕，開始給予
    public void btnOpenGive()
    {
        isGive = true;
        prompt.SetActive(true);
        for (int i = 0; i < arrAnswer.Length; i++)
        {
            arrAnswer[i] = -1;
        }
        for (int i = 0; i < arrBtnAnswer.Length; i++)
        {
            arrBtnAnswer[i].interactable = true;
        }

        intAnswer = 0;
    }
    #endregion

    #region 按鈕，取消給予
    public void btnClosedGive()
    {
        isGive = false;
        prompt.SetActive(false);
        GM.onReturnControl();

        for (int i = 0; i < arrAnswer.Length; i++)
        {
            arrAnswer[i] = -1;
        }
        for(int i = 0; i < arrBtnAnswer.Length; i++) 
        {
            arrBtnAnswer[i].interactable = true;
        }

        intAnswer = 0;
    }
    #endregion

    #region 設定錯誤答案
    /// <summary>
    /// 設定錯誤答案
    /// </summary>
    /// <param name="index">按鈕數字</param>
    public void setWrong(int index)
    {
        //switch (index)
        //{
        //    case 0:       //過關
        //        arrAnswer[index] = -1;
        //        break;
        //    case 2:       //過關
        //        arrAnswer[index] = -1;
        //        break;
        //    case 3:       //過關
        //        arrAnswer[index] = -1;
        //        break;
        //    case 4:       //過關
        //        arrAnswer[index] = -1;
        //        break;
        //    case 11:       //過關
        //        arrAnswer[index] = -1;
        //        break;
        //    case 12:       //過關
        //        arrAnswer[index] = -1;
        //        break;
        //    default:        //失敗
        //        arrAnswer[index] = 0;
        //        break;
        //}
    }
    #endregion

    #region 劇情, 病人抖動
    /// <summary>
    /// 劇情第一幕
    /// </summary>
    /// <returns></returns>
    IEnumerator plotPressentationFirstAct()
    {
        patint.onJitterStart();
        isNexDialogue = false;
        yield return new WaitForSeconds(fltPainfulReaction);
        anim.SetBool("nurse_run_left", true);   //向左看
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("nurse_run_left", false);

        yield return new WaitForSeconds(1.0f);

        anim.SetBool("nurse_run_front", true);  //向前看
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("nurse_run_front", false);

        yield return new WaitForSeconds(0.5f);
        onStartDialogue();
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
