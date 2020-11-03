using UnityEngine;
using System.Collections;

/// <summary>
/// 醫生0
/// </summary>
public class Doctors_seven : NPC
{
    #region 宣告
    [Header("負責的病人")]
    public Patient_seven patint;

    [Header("題目")]
    public GameObject topic;

    /// <summary>痛苦反應時間 </summary>
    private float fltPainfulReaction = 0.5f;
    [Header("驚嘆號")]
    public Sprite imgMarvel;
    [Header("問號")]
    public Sprite imgQuestion;
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
    }
    #endregion

    #region 重複
    void Update()
    {
        if (GM.onGetDialoguePeople() != "DcotorsSeven") return;
        onClickMouseDown();
    }
    #endregion

    #region 點擊滑鼠或畫面
    /// <summary>
    /// 點擊滑鼠或畫面
    /// </summary>
    private void onClickMouseDown()
    {
        if (!isNexDialogue) return;

        if (Input.GetMouseButtonDown(0))
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
                        return;
                    }
                    else if(dlgeSchedule == 2)  //劇情演示
                    {
                        StartCoroutine(plotPressentationFirstAct());
                        dlge.onDisplayWindow(false);
                        dlge.setName(null);
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
                        return;
                    }
                    dlge.setConten(npcData.lose[dlgeSchedule]);
                    break;
                case TaskState.finished:
                    if (dlgeSchedule >= npcData.finshed.Length)
                    {
                        if (!GM.getArrTaskSchedule(7))
                        {
                            GM.finallyTask(7);
                        }
                        dlge.onDisplayWindow(false);
                        dlge.setName(null);
                        dlgeSchedule = 0;
                        GM.onReturnControl();
                        symbol.gameObject.SetActive(false);
                        return;
                    }
                    dlge.setConten(npcData.finshed[dlgeSchedule]);
                    break;
                default:
                    break;
            }
        }
    }
    #endregion

    #region (按鈕)設定準備對話的對象
    /// <summary>
    /// 設定準備對話的對象
    /// </summary>
    public void btnSetReadlyDialogue()
    {
        GM.onSetReadlyDialogue("DcotorsSeven");
    }
    #endregion

    #region 進入房間劇情控制
    /// <summary>進入房間劇情開始</summary>
    public void plotSeRoomStart()
    {
        if (GM.onGetSceneState() != SceneState.sevenStart) return;

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
        if (GM.onGetSceneState() != SceneState.sevenStart) return;
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
        if (GM.onGetDialoguePeople() != "DcotorsSeven") return;

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
        dlge.onDisplayWindow(true);
        dlge.setName(npcData._name);

        switch (npcData._TaskState)
        {
            case TaskState.start:
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

    #region 按鈕,回答問題
    /// <summary>
    /// 回答問題
    /// </summary>
    public void btnAnswerQuestion(string answer)
    {
        switch (answer)
        {
            case "A":       //過關
                npcData._TaskState = TaskState.finished;
                StartCoroutine(plotPressentationSecondAct());
                break;
            default:        //失敗
                npcData._TaskState = TaskState.lose;
                GM.onReturnControl();
                break;
        }
        isNexDialogue = false;
        dlgeSchedule = 0;
        topic.SetActive(false);
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
