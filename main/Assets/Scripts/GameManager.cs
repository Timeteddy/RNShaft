using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// 遊戲整體狀態
/// </summary>
public enum SceneState
{
    checkIn,
    oneStart, oneIng,
    twoStart, twoeIng,
    threeStart, threeeIng,
    fourStart, foureIng,
    fiveStart, fiveeIng,
    sixStart, sixeIng,
    sevenStart, sevenIng,
    eightStart, eightIng,
    nineStart, nineIng,
    tenStart, teneIng,
    finish, 
    loss
}

/// <summary>
/// 地圖轉移
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("玩家")]
    public Player player;
    [Header("護理長")]
    public Leader leader;
    [Header("攝影機")]
    public CameraControl myCamera;
    [Header("劇情機器")]
    public PlotControl plotControl;

    /// <summary>
    /// 遊戲主要狀態
    /// </summary>
    private SceneState sceneState;

    /// <summary>
    /// 各個房間的入口
    /// </summary>
    public Transform[] arrRoomEntrance = new Transform[10];

    /// <summary>
    /// 敲的門號
    /// </summary>
    private int doorNumber;

    /// <summary>
    /// 準備對話的對象
    /// </summary>
    private string readlyDialoguePeople;

    private void Awake()
    {
        sceneState = SceneState.checkIn;

        PlotControl.SE_CHECKIN_START += checkInStart;
        PlotControl.SE_CHECKIN_END += checkInEnd;

        myCamera.SE_FLLW_START += cameraFollowStart;
        myCamera.SE_FLLW_END += cameraFollowEnd;

    }

    /// <summary>
    /// 準備進行對話
    /// </summary>
    /// <param name="people">準備對話的對象</param>
    public void onSetReadlyDialogue(string people)
    {
        readlyDialoguePeople = people;
    }

    /// <summary>
    /// 準備進行對話
    /// </summary>
    /// <param name="people">準備對話的對象</param>
    public string onGetDialoguePeople()
    {
        return readlyDialoguePeople;
    }

    /// <summary>
    /// 開始對話
    /// </summary>
    public void onStartDialogue()
    {
        switch (readlyDialoguePeople)
        {
            case "Leder":
                leader.onStartDialogue();
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
        player.onCheckInStart();
        myCamera.onCheckInStart(leader.transform);
    }

    /// <summary>
    /// 報到結束
    /// </summary>
    public void checkInEnd()   //TODO 之後要修改為隨機題目
    {
        player.onReturnControl();
        sceneState = SceneState.oneStart;

    }

    public void btnInitRoom(int value)
    {
        player.playerData._actionState = ActionState.intRoom;
        doorNumber = value;
    }

    /// <summary>
    /// 敲門
    /// </summary>
    /// <param name="value">準備進入的門牌</param>
    public void intoRoom(int value)
    {
        if (doorNumber != value) return;
        myCamera.transform.position = arrRoomEntrance[value].transform.position;
        player.transform.position = arrRoomEntrance[value].transform.position;
        player.playerData._actionState = ActionState.Idle;
    }

    /// <summary>
    /// 攝影機開始跟隨
    /// </summary>
    public void cameraFollowStart()
    {
        print("開始跟隨");
    }

    /// <summary>
    /// 攝影機跟隨結束
    /// </summary>
    public void cameraFollowEnd()
    {
        StartCoroutine(onReturnControl(1.0f));
    }

    /// <summary>
    /// 歸還控制權
    /// </summary>
    /// <param name="value">等待秒數</param>
    /// <returns></returns>
    IEnumerator onReturnControl(float value)
    {
        yield return new WaitForSeconds(value);
        player.onReturnControl();
        myCamera.onReturnControl();
    }
}
