using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("玩家資料")]
    public PlayerData playerData;
    public GameObject userOne;
    public GameObject userTwo;
    [Header("場景控制器")]
    public GameManager GM;
    [Header("角色動畫控制器")]
    public Animator anim; 
    
    [Header("背包控制器")]
    public Backpack backpackSrc;

    private string front = "front";
    private string back = "back";
    private string left = "left";
    private string right = "right";

    private string nowAnim = "idel";

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

    /// <summary>
    /// 要移動的位置
    /// </summary>
    Vector2 point;

    /// <summary>
    /// 紀錄進入碰撞的位置
    /// </summary>
    Vector2 enterPoint;

    /// <summary>
    /// 停止確認
    /// </summary>
    bool isPlayStop = true;
    /// <summary>
    /// 準備進入的房間
    /// </summary>
    private int readlyIntoRoom;

    private void Awake()
    {
        //註冊事件
        //PlotControl.onCall += test;

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
            backpackSrc.onForgoProps();
            playerData._actionState = ActionState.Idle;
            point = getMousePoint();
            directionControlelr();
            isPlayStop = false;

            if (!hit) return;
            print(hit.collider.name);
        }

        //  如果目前不能播放停止動畫時
        if (!isPlayStop)
        {
            // 如果發現停止
            if (playPoint == point)
            {
                animStopJudge();
                
                if(playerData._actionState == ActionState.getProps)
                {
                    backpackSrc.onPutBackpack();
                }else if(playerData._actionState == ActionState.intRoom)
                {
                    GM.intoRoom(readlyIntoRoom);
                    point = transform.position;
                }

                isPlayStop = true;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, point, speed * Time.deltaTime);
    }
    
    /// <summary>
    /// 開啟背包
    /// </summary>
    public void openBackpack()
    {
        isOpenBackpack = true;
        backpackSrc.onOpenBackPack(); 
    }
    /// <summary>
    /// 關閉背包
    /// </summary>
    public void offBackPack()
    {
        isOpenBackpack = false;
        backpackSrc.onCloseBackPack();
        backpack.SetActive(false);
    }

    /// <summary>
    /// 在ui上時
    /// </summary>
    public void initUi()
    {
        walk = false;
    }
    /// <summary>
    /// 離開ui時
    /// </summary>
    public void exetUi()
    {
        walk = true;
    }
    /// <summary>
    /// 準備撿取物品時
    /// </summary>
    public void btnGetProps()
    {
        playerData._actionState = ActionState.getProps;
    }
    /// <summary>
    /// 完成撿取物品
    /// </summary>
    public void finishGetProps()
    {
        playerData._actionState = ActionState.Idle;
    }

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

    /// <summary>
    /// 依照角度判斷前進方向
    /// </summary>
    private void directionControlelr()
    {
        float angle = getAngle();

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

    /// <summary>
    /// 碰撞事件(進入)
    /// </summary>
    /// <param name="evt"></param>
    void OnCollisionEnter2D(Collision2D evt)
    {
        if(evt.gameObject.tag == "wall")
        {
            enterPoint = transform.position;
            point = enterPoint;
        }
    }

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

    public int getReadlyIntoRoom()
    {
        return readlyIntoRoom;
    }
    /// <summary>
    /// 進入觸發區
    /// </summary>
    /// <param name="evt"></param>
    void OnTriggerEnter2D(Collider2D evt)
    {
        readlyIntoRoom = int.Parse(evt.name);
    }

    /// <summary>
    /// 離開觸發區
    /// </summary>
    /// <param name="evt"></param>
    void OnTriggerExit2D(Collider2D evt)
    {
        readlyIntoRoom = -1;
    }

}
