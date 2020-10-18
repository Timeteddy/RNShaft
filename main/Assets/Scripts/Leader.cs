using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 護理長
/// </summary>
public class Leader : NPC
{
    void Awake()
    {
        dlgeSchedule = 0;

        npcData._TaskState = TaskState.start;

        PlotControl.SE_CHECKIN_START += checkInStart;
        PlotControl.SE_CHECKIN_END += checkInEnd;

        myCamera.SE_FLLW_START += cameraFollowStart;
        myCamera.SE_FLLW_END += cameraFollowEnd;

        GameMachine.SE_TYPWRTR_START += typewriterStart;
        GameMachine.SE_TYPWRTR_END += typewriterEnd;
    }
    
    void Update()
    {
        onClickMouseDown();
    }

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
                        checkInEnd();
                        return;
                    }
                    dlge.setConten(npcData.start[dlgeSchedule]);
                    break;
                case TaskState.ing:
                    GM.player.onReturnControl();
                    dlge.onDisplayWindow(false);
                    dlge.setName(null);
                    break;
                case TaskState.lose:
                    break;
                case TaskState.finished:
                    break;
                default:
                    break;
            }

        }
    }

    /// <summary>
    /// 設定準備對話的對象
    /// </summary>
    public void btnSetReadlyDialogue()
    {
        GM.onSetReadlyDialogue("Leder");
    }

    /// <summary>
    /// 打字效果開始
    /// </summary>
    private void typewriterStart()
    {
        isNexDialogue = false;
    }

    /// <summary>
    /// 打字效果結束
    /// </summary>
    private void typewriterEnd()
    {
        if (GM.onGetDialoguePeople() != "Leder") return;
        //由於進行中時 是依照隨機的結果選擇對話，因使不需要累加

        switch (npcData._TaskState)
        {
            case TaskState.start:
                dlgeSchedule++;
                break;
            case TaskState.ing:
                dlgeSchedule = Random.Range(0, npcData.ing.Length);
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

    /// <summary>
    /// 開始對話
    /// </summary>
    public void onStartDialogue()
    {
        dlge.onDisplayWindow(true);
        dlge.setName("護理長");

        switch (npcData._TaskState)
        {
            case TaskState.start:
                dlge.setConten(npcData.start[dlgeSchedule]);
                break;
            case TaskState.ing:
                dlge.setConten(npcData.ing[dlgeSchedule]);
                break;
            case TaskState.lose:
                break;
            case TaskState.finished:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 報到開始
    /// </summary>
    private void checkInStart()
    {
    }
    /// <summary>
    /// 報到結束
    /// </summary>
    private void checkInEnd()
    {
        npcData._TaskState = TaskState.ing;
        dlgeSchedule = Random.Range(0, npcData.ing.Length);
        dlge.onDisplayWindow(false);
        dlge.setName(null);
        symbol.gameObject.SetActive(false);
        GM.checkInEnd();
    }
    /// <summary>
    /// 攝影機跟隨開始
    /// </summary>
    private void cameraFollowStart()
    {

    }
    /// <summary>
    /// 攝影機跟隨結束
    /// </summary>
    private void cameraFollowEnd()
    {
        anim.SetTrigger("nurse_posture_wave0");
        symbol.gameObject.SetActive(true);
    }


}
