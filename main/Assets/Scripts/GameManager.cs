using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// 遊戲整體狀態
/// </summary>
public enum SceneState
{
    checkIn, one, two, three, four, five, six, seven, eight, nine, ten, finish, loss
}

/// <summary>
/// 地圖轉移
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("玩家")]
    public Player player;
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

    private void Awake()
    {
        sceneState = SceneState.checkIn;
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

    public void test()
    {
        print("test");//王依柔
    }
}
