using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

#region 遊戲整體狀態
/// <summary>
/// 遊戲整體狀態
/// </summary>
public enum SceneState
{
    zeroStart,
    oneStart, 
    twoStart, 
    threeStart,
    fourStart,
    fiveStart,
    sixStart, 
    sevenStart,
    eightStart,
    nineStart,

    zeroIng,
    oneIng,
    twoeIng,
    threeeIng,
    foureIng,
    fiveeIng,
    sixeIng,
    sevenIng,
    eightIng,
    nineIng,

    checkIn,
    finish, 
    loss
}
#endregion

/// <summary>
/// 地圖轉移
/// </summary>
public class GameManager : MonoBehaviour
{
    #region 宣告
    [Header("玩家")]
    public Player player;
    [Header("護理長")]
    public Leader leader;
    [Header("醫生0")]
    public Doctors_zero doctorsZero;
    [Header("醫生1")]
    public Doctors_one doctorsOne;
    [Header("醫生2")]
    public Doctors_two doctorsTwo;
    [Header("醫生3")]
    public Doctors_three doctorsThree;
    [Header("醫生4")]
    public Doctors_four doctorsFour;
    [Header("醫生5")]
    public Doctors_five doctorsFive;
    [Header("醫生6")]
    public Doctors_six doctorsSix;
    [Header("醫生7")]
    public Doctors_seven doctorsSeven;
    [Header("醫生8")]
    public Doctors_eight doctorsEight;
    [Header("醫生9")]
    public Doctors_nine doctorsNine;
    [Header("攝影機")]
    public CameraControl myCamera;
    [Header("劇情機器")]
    public PlotControl plotControl;

    /// <summary>
    /// 遊戲主要狀態
    /// </summary>
    private SceneState sceneState;

    /// <summary>
    /// 各個房間的出口
    /// </summary>
    public Transform[] arrRoomExport = new Transform[10];
    
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

    /// <summary>所有房間的任務完成狀態 </summary>
    private bool[] arrRoomTask = new bool[10];
    /// <summary>玩家完成進度 </summary>
    private int roomTaskSchedule = 0;
    /// <summary>玩家任務排程表(任務順序) </summary>
    private int[] arrTaskSchedule = new int[10];
    /// <summary>當前進度 </summary>
    private int nowSchedule = 0;
    /// <summary>攝影機在房間內的視角 </summary>
    private float fltInRoom = 6.0f;
    /// <summary>攝影機在房間外的視角 </summary>
    private float fltOutRoom = 8.0f;
    /// <summary>場景動畫 </summary>
    public AnimScene animScene;
    [Header("音效控制器")]
    public AudioSource audioS;
    [Header("正常BGM")]
    public AudioClip mscBgmBass;
    [Header("緊張BGM")]
    public AudioClip mscBgmTnsn;
    [Header("按鈕音效")]
    public AudioClip mscButton;
    [Header("選單")]
    public AudioClip mscOpenMenu;
    [Header("門")]
    public AudioClip mscDoor;
    [Header("撿取道具")]
    public AudioClip mscPickUp;
    [Header("跳")]
    public AudioClip mscJump;
    #endregion

    #region 起始
    [System.Obsolete]
    private void Awake()
    {
        sceneState = SceneState.checkIn;

        PlotControl.SE_CHECKIN_START += checkInStart;
        PlotControl.SE_CHECKIN_END += checkInEnd;

        myCamera.SE_FLLW_START += cameraFollowStart;
        myCamera.SE_FLLW_END += cameraFollowEnd;

        //所有房間都是未完成狀態
        int lenRt = arrRoomTask.Length;
        for(int i = 0; i < lenRt; i++)
        {
            arrRoomTask[i] = false;
        }
        
        int lenTs = arrTaskSchedule.Length;
        for (int i = 0; i < lenTs; i++)
        {
            arrTaskSchedule[i] = i;
        }
        //將順序打亂 TODO正式版要打開
        for (int i = 0; i < lenTs; i++)
        {
            int original = arrTaskSchedule[i];
            int rdm = Random.RandomRange(0, 10);
            arrTaskSchedule[i] = arrTaskSchedule[rdm];
            arrTaskSchedule[rdm] = original;
        }
    }

    public void Start()
    {
        checkInStart();
    }
    #endregion

    //功能
    #region 設定準備進行對話的對象
    /// <summary>
    /// 設定準備進行對話的對象
    /// </summary>
    /// <param name="people">設定準備進行對話的對象</param>
    public void onSetReadlyDialogue(string people)
    {
        readlyDialoguePeople = people;
    }
    #endregion

    #region 抓取準備進行對話的對象
    /// <summary>
    /// 抓取準備進行對話的對象
    /// </summary>
    /// <param name="people">準備對話的對象</param>
    public string onGetDialoguePeople()
    {
        return readlyDialoguePeople;
    }
    #endregion

    #region 開始對話
    /// <summary>
    /// 開始對話
    /// </summary>
    public void onStartDialogue()
    {
        audioS.PlayOneShot(mscOpenMenu, 1.0f);
        switch (readlyDialoguePeople)
        {
            case "Leder":
                leader.onStartDialogue();
                break;
            case "MyselfCheckin":
                player.onDlgeMyself("先去找護理長報到吧!");
                break;
            case "MyselfWrongDoor":
                player.onDlgeMyself("走錯地方了，病人還再等我們呢!");
                break;
            case "DcotorsZero":
                doctorsZero.onStartDialogue();
                break;
            case "DcotorsOne":
                doctorsOne.onStartDialogue();
                break;
            case "DcotorsTwo":
                doctorsTwo.onStartDialogue();
                break;
            case "DcotorsThree":
                doctorsThree.onStartDialogue();
                break;
            case "DcotorsFour":
                doctorsFour.onStartDialogue();
                break;
            case "DcotorsFive":
                doctorsFive.onStartDialogue();
                break;
            case "DcotorsSix":
                doctorsSix.onStartDialogue();
                break;
            case "DcotorsSeven":
                doctorsSeven.onStartDialogue();
                break;
            case "DcotorsEight":
                doctorsEight.onStartDialogue();
                break;
            case "DcotorsNine":
                doctorsNine.onStartDialogue();
                break;
            default:
                break;
        }
    }
    #endregion

    #region 報到開始
    /// <summary>
    /// 報到開始
    /// </summary>
    private void checkInStart()
    {
        player.onCheckInStart();
        myCamera.onCheckInStart(leader.transform);
    }
    #endregion

    #region 報到結束
    /// <summary>
    /// 報到結束
    /// </summary>
    public void checkInEnd()
    {
        //讓攝影機移動到特定的房間
        sceneState = (SceneState)arrTaskSchedule[roomTaskSchedule];

        myCamera.onCheckInStart(arrRoomEntrance[arrTaskSchedule[roomTaskSchedule]]);

        nowSchedule = arrTaskSchedule[roomTaskSchedule];

        //sceneState = SceneState.nineStart;
        //myCamera.onCheckInStart(arrRoomEntrance[9]);
    }
    #endregion

    #region 按鈕.準備進入的房間
    public void btnInitRoom(int value)
    {
        doorNumber = value;
        player.readlyIntoRoom = value;
    }
    #endregion

    #region 按鈕.準備離開的房間
    public void btnLeventRoom()
    {
        player.playerData._actionState = ActionState.leaveRoom;
    }
    #endregion

    #region 敲門 進入房間
    /// <summary>
    /// 播放過場動畫(進入房間)
    /// </summary>
    /// <param name="value">準備進入的門牌</param>
    public void intoRoom(int value)
    {
        //在報到狀態
        if (sceneState == SceneState.checkIn)
        {
            player.btnDialogue.gameObject.SetActive(true);
            onSetReadlyDialogue("MyselfCheckin");
            onStartDialogue();
            return;
        }

        //走錯門
        if((int)sceneState != value)
        {
            player.btnDialogue.gameObject.SetActive(true);
            onSetReadlyDialogue("MyselfWrongDoor");
            onStartDialogue();
            return;
        }

        if (doorNumber != value) return;
        animScene.onAnimPlayPassIntoRoom();
    }

    /// <summary>可通過門(進入房間) </summary>
    public void onPassIntoRoom()
    {
        sceneState = (SceneState)doorNumber; //切換遊戲主狀態
        plotControl.onSeRoomStart();
        myCamera.GetComponent<Camera>().orthographicSize = fltInRoom;
        myCamera.transform.position = arrRoomExport[doorNumber].transform.position;
        player.transform.position = arrRoomExport[doorNumber].transform.position;
        player.onSetPoint(player.transform.position);
        player.playerData._actionState = ActionState.Idle;
    }
    #endregion

    #region 離開房間
    /// <summary>
    /// 播放過場動畫(離開房間)
    /// </summary>
    public void leaveRoom()
    {
        animScene.onAnimPlayPassLeventRoom();
    }

    /// <summary>可通過門(離開房間) </summary>
    public void onPassLeventRoom()
    {
        myCamera.GetComponent<Camera>().orthographicSize = fltOutRoom;
        myCamera.transform.position = arrRoomEntrance[doorNumber].transform.position;
        player.transform.position = arrRoomEntrance[doorNumber].transform.position;
        player.onSetPoint(player.transform.position);

        if (arrRoomTask[nowSchedule] == true)
        {
            if (roomTaskSchedule == 10)
            {
                myCamera.onCheckInStart(leader.transform);
                leader.npcData._TaskState = TaskState.finished;
                return;
            }
            sceneState = (SceneState)arrTaskSchedule[roomTaskSchedule];

            myCamera.onCheckInStart(arrRoomEntrance[arrTaskSchedule[roomTaskSchedule]]);

            nowSchedule = arrTaskSchedule[roomTaskSchedule];
            return;
        }
        player.playerData._actionState = ActionState.Idle;
    }
    #endregion

    #region 抓取房間任務當前狀態
    /// <summary>
    /// 抓取房間任務當前狀態
    /// </summary>
    /// <param name="index">房號</param>
    /// <returns></returns>
    public bool getArrTaskSchedule(int index)
    {
        return arrRoomTask[index];
    }
    #endregion

    #region 任務完成
    /// <summary>
    /// 任務完成
    /// </summary>
    /// <param name="value">完成的房間</param>
    public void finallyTask(int value)
    {
        arrRoomTask[value] = true;
        roomTaskSchedule++;
    }
    #endregion

    #region 攝影機開始跟隨
    /// <summary>
    /// 攝影機開始跟隨
    /// </summary>
    public void cameraFollowStart()
    {
        player.btnChangePlayerSataeInPlot();
    }
    #endregion

    #region 攝影機跟隨結束
    /// <summary>
    /// 攝影機跟隨結束
    /// </summary>
    public void cameraFollowEnd()
    {
        StartCoroutine(onReturnControl(1.0f));
    }
    #endregion

    #region 歸還控制權
    /// <summary>
    /// 歸還控制權
    /// </summary>
    /// <param name="value">等待秒數</param>
    /// <returns></returns>
    IEnumerator onReturnControl(float value)
    {
        yield return new WaitForSeconds(value);
        onReturnControl();
    }
    /// <summary>
    /// 歸還控制權
    /// </summary>
    public void onReturnControl()
    {
        player.onReturnControl();
        myCamera.onReturnControl();
    }

    /// <summary>
    /// 歸還玩家控制權
    /// </summary>
    /// <param name="value">等待秒數</param>
    /// <returns></returns>
    IEnumerator onReturnPlayerControl(float value)
    {
        yield return new WaitForSeconds(value);
        player.onReturnControl();
    }
    #endregion

    #region 抓取當前遊戲主狀態
    /// <summary>抓取當前遊戲主狀態</summary>
    public SceneState onGetSceneState()
    {
        return sceneState;
    }
    #endregion

    #region 修改主角為劇情狀態
    /// <summary>修改主角為劇情狀態 </summary>
    public void onChangePlayerStatePlot()
    {
        player.playerData._actionState = ActionState.ingPolt;
    }
    #endregion

    #region 音效播放(按鈕)
    /// <summary>
    /// 按鈕音效
    /// </summary>
    public void btnMscButton()
    {
        audioS.PlayOneShot(mscButton, 1.0f);
    }
    /// <summary>
    /// 頁面音效
    /// </summary>
    public void btnMscMenu()
    {
        audioS.PlayOneShot(mscOpenMenu, 1.0f);
    }
    /// <summary>
    /// 道具撿取
    /// </summary>
    public void btnMscPickUp()
    {
        audioS.PlayOneShot(mscPickUp, 1.0f);
    }
    /// <summary>
    /// 道具撿取
    /// </summary>
    public void btnMscDoor()
    {
        audioS.PlayOneShot(mscDoor, 1.0f);
    }
    #endregion
}
