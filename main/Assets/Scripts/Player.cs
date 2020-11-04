using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region 宣告
    [Header("玩家資料")]
    public PlayerData playerData;
    public GameObject userOne;
    public GameObject userTwo;
    [Header("場景控制器")]
    public GameManager GM;
    [Header("角色動畫控制器")]
    public Animator anim;
    
    [Header("對話系統")]
    public DialogueSystem dlge;
    /// <summary>
    /// 是否與自己對話
    /// </summary>
    private bool isDlgeMyself = false;
    /// <summary>
    /// 對話進度
    /// </summary>
    private int dlgeSchedule = 0;
    /// <summary>
    /// 能否進行點擊觀看下個對話
    /// </summary>
    private bool isNexDialogue = false;

    [Header("背包控制器")]
    public Backpack backpackSrc;

    private string front = "front";
    private string back = "back";
    private string left = "left";
    private string right = "right";

    public string nowAnim = "idel";

    [Header("移動速度")]
    public float speed;
    [Header("玩家背包")]
    public GameObject backpack;
    private bool isOpenBackpack = false;

    private string props;
    [Header("能夠拿取的道具")]
    public LayerMask canHit;

    /// <summary>
    /// 能否走路
    /// </summary>
    private bool walk = true;

    public RaycastHit hit;//偵測賭局

    /// <summary> 要移動的位置 </summary>
    Vector2 point;

    /// <summary>紀錄進入碰撞的位置 </summary>
    Vector2 enterPoint;

    /// <summary>停止確認 </summary>
    bool isPlayStop = true;
    /// <summary>準備進入的房間 </summary>
    public int readlyIntoRoom;

    /// <summary>是否在門前面(站在觸發區) </summary>
    [SerializeField]
    private bool isReadlyIntoDoor;
    /// <summary>是否點擊門 </summary>
    [SerializeField]
    private bool isHitDoor;
    /// <summary>是否站在人的前面(站在觸發區) </summary>
    private bool isReadlyTalk;
    /// <summary>是否點擊人 </summary>
    public bool isHitTalk;

    #endregion

    #region 起始
    private void Awake()
    {
        //註冊事件
        GameMachine.SE_TYPWRTR_START += typewriterStart;
        GameMachine.SE_TYPWRTR_END += typewriterEnd;

        switch (playerData._RoleState)
        {
            case RoleState.not:
                Debug.LogError("尚未選擇角色");
                userOne.SetActive(false);
                userTwo.SetActive(false);
                break;
            case RoleState.Baotou:
                userTwo.SetActive(false);
                anim = userOne.GetComponent<Animator>();
                break;
            case RoleState.FullyArmed:
                userOne.SetActive(false);
                anim = userTwo.GetComponent<Animator>();
                break;
        }
    }

    void Start()
    {
        point = transform.position;
        playerData._actionState = ActionState.Idle;
        readlyIntoRoom = -1;
    }
    #endregion

    #region 重複
    void Update()
    {
        RaycastHit2D hit = getRayObj();
        Vector2 playPoint = new Vector2(transform.position.x, transform.position.y);
        //animJudge();

        if (Input.GetMouseButtonDown(0))
        {
            if (isOpenBackpack) return;
            if (!walk) return;
            if (playerData._actionState == ActionState.ingPolt) return;
            if (playerData._actionState == ActionState.ingDialogue) 
            {
                if (!isDlgeMyself) return;
                if (!isNexDialogue) return;

                if(dlgeSchedule > 0)
                {
                    onReturnControl(); 
                    dlge.onDisplayWindow(false);
                    dlge.setName(null);
                    dlgeSchedule = 0;
                    isDlgeMyself = true;
                }
                return;
            }
            backpackSrc.onForgoProps();
            playerData._actionState = ActionState.Idle;
            point = getMousePoint();
            directionControlelr();
            isHitDoor = false;
            isHitTalk = false;
        }

        //  如果目前不能播放停止動畫時
        if (!isPlayStop)
        {
            // 如果發現停止
            if (playPoint == point)
            {
                animStopJudge();

                switch (playerData._actionState)
                {
                    case ActionState.Idle:
                        break;
                    case ActionState.getProps:
                        //backpackSrc.onPutBackpack();
                        break;
                    case ActionState.intRoom:
                        break;
                    case ActionState.leaveRoom:
                        GM.leaveRoom();
                        point = transform.position;
                        break;
                    case ActionState.ingPolt:
                        break;
                    case ActionState.readyDialogue:
                        
                        break;
                    case ActionState.ingDialogue:
                        break;
                    default:
                        break;
                }
                isPlayStop = true;
            }
        }

        //當停留在觸發區的時候點擊門時
        if (isHitDoor && isReadlyIntoDoor)
        {
            playerData._actionState = ActionState.intRoom;
            GM.intoRoom(readlyIntoRoom);
            point = transform.position;
        }

        if(isHitTalk && isReadlyTalk)
        {
            playerData._actionState = ActionState.ingDialogue;
            GM.onStartDialogue();
            isHitTalk = false;
        }

        transform.position = Vector2.MoveTowards(transform.position, point, speed * Time.deltaTime);
    }
    #endregion

    #region 報到開始
    /// <summary>
    /// 報到開始
    /// </summary>
    public void onCheckInStart()
    {
        playerData._actionState = ActionState.ingPolt;
        anim.SetTrigger("nurse_idle_back");
    }
    #endregion

    #region 歸還主角控制權
    /// <summary>
    /// 歸還主角控制權
    /// </summary>
    public void onReturnControl()
    {
        playerData._actionState = ActionState.Idle;
    }
    #endregion

    #region 由外界控制主角移動
    /// <summary>
    /// 由外界控制主角移動
    /// </summary>
    /// <param name="value">要到達的位置</param>
    public void onSetPoint(Vector2 value)
    {
        point = value;
    }
    #endregion

    #region 獲取主角的目標位置
    /// <summary>
    /// 獲取主角的目標位置
    /// </summary>
    /// <returns></returns>
    public Vector2 onGetPoint()
    {
        return point;
    }
    #endregion

    #region 開啟背包
    /// <summary>
    /// 開啟背包
    /// </summary>
    public void openBackpack()
    {
        isOpenBackpack = true;
        backpackSrc.onOpenBackPack();
        backpack.SetActive(true);
    }
    #endregion

    #region 關閉背包
    /// <summary>
    /// 關閉背包
    /// </summary>
    public void offBackPack()
    {
        isOpenBackpack = false;
        backpackSrc.onCloseBackPack();
        backpack.SetActive(false);
    }
    #endregion

    #region 在ui上時
    /// <summary>
    /// 在ui上時
    /// </summary>
    public void initUi()
    {
        walk = false;
    }
    #endregion

    #region 離開ui時
    /// <summary>
    /// 離開ui時
    /// </summary>
    public void exetUi()
    {
        walk = true;
    }
    #endregion

    #region 準備撿取物品時
    /// <summary>
    /// 準備撿取物品時
    /// </summary>
    public void btnGetProps()
    {
        //playerData._actionState = ActionState.getProps;
    }
    #endregion

    #region 完成撿取物品
    /// <summary>
    /// 完成撿取物品
    /// </summary>
    public void finishGetProps()
    {
        playerData._actionState = ActionState.Idle;
    }
    #endregion

    #region 判斷射線是否擊中
    /// <summary>
    /// 判斷射線是否擊中
    /// </summary>
    /// <returns></returns>
    private RaycastHit2D getRayObj()
    {
        //滑鼠位置
        Vector2 mousePosition = getMousePoint();
        //玩家位置
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);
        //射線長度
        float distance = Vector2.Distance(mousePosition, transform.position);
        //射線本體
        RaycastHit2D hit = Physics2D.Raycast(origin, mousePosition, distance, canHit);

        //畫出一條重玩家的位置到射線位置的線
        Debug.DrawLine(origin, mousePosition, Color.red);

        return hit;
    }
    #endregion

    #region 獲取滑鼠位置
    /// <summary>
    /// 獲取滑鼠位置
    /// </summary>
    /// <returns></returns>
    private Vector2 getMousePoint()
    {
        //滑鼠位置
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint
            (Input.mousePosition).x, Camera.main.ScreenToWorldPoint
            (Input.mousePosition).y);

        return mousePosition;
    }
    #endregion

    #region 或取玩家前進方向的角度
    /// <summary>
    /// 或取玩家前進方向的角度
    /// </summary>
    /// <returns></returns>
    private float getAngle()
    {
        Vector2 mousePoint = getMousePoint();
        Vector2 playPoint = new Vector2(transform.position.x, transform.position.y);
        Vector2 tage = mousePoint - playPoint;

        float angle = Mathf.Atan2(tage.y, tage.x) * Mathf.Rad2Deg;

        return angle;
    }
    #endregion

    #region 依照角度判斷前進方向
    /// <summary>
    /// 依照角度判斷前進方向
    /// </summary>
    public void directionControlelr()
    {
        float angle = getAngle();

        isPlayStop = false;

        anim.SetBool("nurse_run_front", false);
        anim.SetBool("nurse_run_back", false);
        anim.SetBool("nurse_run_left", false);
        anim.SetBool("nurse_run_right", false);

        if (angle >= 0)
        {
            if(angle >= 0 && angle < 45)
            {
                anim.SetBool("nurse_run_right", true);
                nowAnim = right;
                return;
            }
            else if(angle >= 135)
            {
                anim.SetBool("nurse_run_left", true);
                nowAnim = left;
                return;
            }
            anim.SetBool("nurse_run_back", true);
            nowAnim = back;
        }
        else
        {
            if (angle < 0 && angle > -45)
            {
                anim.SetBool("nurse_run_right", true);
                nowAnim = right;
                return;
            }
            else if (angle < -135)
            {
                anim.SetBool("nurse_run_left", true);
                nowAnim = left;
                return;
            }
            anim.SetBool("nurse_run_front", true);
            nowAnim = front;
        }
    }
    #endregion

    #region 根據動畫目前動畫決定結束時該播放的動畫
    /// <summary>
    /// 根據動畫目前動畫決定結束時該播放的動畫
    /// </summary>
    void animStopJudge()
    {
        switch (nowAnim)
        {
            case "front":
                anim.SetBool("nurse_run_front", false);
                break;
            case "back":
                anim.SetBool("nurse_run_back", false);
                break;
            case "left":
                anim.SetBool("nurse_run_left", false);
                break;
            case "right":
                anim.SetBool("nurse_run_right", false);
                break;
            default:
                break;
        }
        nowAnim = "idle";
    }
    #endregion

    #region 碰撞事件(進入)
    /// <summary>
    /// 碰撞事件(進入)
    /// </summary>
    /// <param name="evt"></param>
    void OnCollisionEnter2D(Collision2D evt)
    {
        if(evt.gameObject.tag == "wall" || evt.gameObject.tag == "people")
        {
            float angle = getAngle();
            enterPoint = transform.position;

            switch (nowAnim)
            {
                case "front":
                    enterPoint = new Vector2(enterPoint.x, enterPoint.y + 0.3f);
                    break;
                case "back":
                    enterPoint = new Vector2(enterPoint.x, enterPoint.y - 0.3f);
                    break;
                case "left":
                    enterPoint = new Vector2(enterPoint.x + 0.3f, enterPoint.y);
                    break;
                case "right":
                    enterPoint = new Vector2(enterPoint.x - 0.3f, enterPoint.y);
                    break;
                default:
                    break;
            }

            point = enterPoint;
        }
    }
    #endregion

    #region 碰撞事件(停留)
    /// <summary>
    /// 碰撞事件(停留)
    /// </summary>
    /// <param name="evt"></param>
    void OnCollisionStay2D(Collision2D evt)
    {
        if (evt.gameObject.tag == "wall")
        {
            point = enterPoint;
        }
    }
    #endregion

    #region 按鈕,準備進行對話
    /// <summary>
    /// 準備進行對話
    /// </summary>
    public void btnReadlyDialogue()
    {
        //切換狀態為準備對話
        playerData._actionState = ActionState.readyDialogue;
    }
    #endregion

    #region 按鈕,修改主角狀態為 劇情演示
    /// <summary>
    /// 按鈕,修改主角狀態為 劇情演示
    /// </summary>
    public void btnChangePlayerSataeInPlot()
    {
        playerData._actionState = ActionState.ingPolt;
    }
    #endregion

    #region 玩家跟自己對話
    /// <summary>
    /// 玩家跟自己對話
    /// </summary>
    /// <param name="content">對話內容</param>
    public void onDlgeMyself(string content)
    {
        playerData._actionState = ActionState.ingDialogue;
        isDlgeMyself = true;
        dlge.setName(playerData._name);
        dlge.onDisplayWindow(true);
        dlge.setConten(content);
    }
    #endregion

    #region 抓取準備進入的房間
    public int getReadlyIntoRoom()
    {
        return readlyIntoRoom;
    }
    #endregion

    #region 進入觸發區
    /// <summary>
    /// 進入觸發區
    /// </summary>
    /// <param name="evt"></param>
    void OnTriggerEnter2D(Collider2D evt)
    {
        if (evt.name == "Main Camera") return;

        switch (playerData._actionState)
        {
            case ActionState.Idle:
                break;
            case ActionState.getProps:
                break;
            case ActionState.intRoom:
                break;
            case ActionState.leaveRoom:
                break;
            case ActionState.ingPolt:
                break;
            case ActionState.readyDialogue:
                break;
            case ActionState.ingDialogue:
                break;
            default:
                break;
        }
    }
    #endregion

    #region 按鈕，點擊門
    public void btnHitDoor()
    {
        isHitDoor = true;
    }
    #endregion

    #region 按鈕，點擊人
    public void btnHitPeople()
    {
        isHitTalk = true;
    }
    #endregion

    #region 停留觸發區
    void OnTriggerStay2D(Collider2D evt)
    {
        if (evt.tag == "door")
        {
            isReadlyIntoDoor = true;
        }
        else if(evt.tag == "people")
        {
            isReadlyTalk = true;
        }
    }
    #endregion

    #region 離開觸發區
    /// <summary>
    /// 離開觸發區
    /// </summary>
    /// <param name="evt"></param>
    void OnTriggerExit2D(Collider2D evt)
    {
        readlyIntoRoom = -1;
        isReadlyIntoDoor = false;
    }
    #endregion

    #region 對話開始
    /// <summary>
    /// 對話開始
    /// </summary>
    public void typewriterStart()
    {
        if (!isDlgeMyself) return;
        isNexDialogue = false;
    }
    #endregion

    #region 對話結束
    /// <summary>
    /// 對話結束
    /// </summary>
    public void typewriterEnd()
    {
        if (!isDlgeMyself) return;
        dlgeSchedule++;
        isNexDialogue = true;
    }
    #endregion
}
