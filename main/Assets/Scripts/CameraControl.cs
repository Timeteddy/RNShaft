using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void followStart();
public delegate void followEnd();

/// <summary>
/// 遊戲整體狀態
/// </summary>
public enum CameraState
{
    player, ingPoit // 跟隨玩家, 劇情演示
}

public class CameraControl : MonoBehaviour
{
    /// <summary>
    /// 跟隨模式開始
    /// </summary>
    public event followStart SE_FLLW_START;

    /// <summary>
    /// 跟隨模式關閉
    /// </summary>
    public event followEnd SE_FLLW_END;

    /// <summary>
    /// 玩家
    /// </summary>
    private Transform player;

    /// <summary>
    /// 跟隨的目標
    /// </summary>
    private Transform point;

    /// <summary>
    /// 攝影機狀態
    /// </summary>
    public CameraState cameraState;

    /// <summary>
    /// 完成跟隨距離
    /// </summary>
    private float followFinalDistance = 1;

    /// <summary>
    /// 是否正在進行動畫演示跟隨
    /// </summary>
    private bool isPoitFollow = false;

    [Header("攝影機移動速度")]
    public float speed;

    void Start()
    {
        cameraState = CameraState.player;
        player = GameObject.Find("player").transform;
    }

    /// <summary>
    /// 報到開始
    /// </summary>
    /// <param name="type">更隨目標</param>
    public void onCheckInStart(Transform type)
    {
        onSetPoint(type);
        cameraState = CameraState.ingPoit;
        isPoitFollow = true;
    }

    /// <summary>
    /// 歸還主角控制權
    /// </summary>
    public void onReturnControl()
    {
        cameraState = CameraState.player;
    }

    void Update()
    {
        switch (cameraState)
        {
            case CameraState.player:
                speed = 10.0f;
                Track(player);
                break;
            case CameraState.ingPoit:
                speed = 1.0f;
                Track(point);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 設定跟隨目標
    /// </summary>
    /// <param name="type">要跟隨的目標</param>
    public void onSetPoint(Transform type)
    {
        SE_FLLW_START();
        point = type;
    }

    /// <summary>
    /// 攝影機跟隨玩家
    /// </summary>
    /// <param name="type">跟隨的目標</param>
    private void Track(Transform type)
    {
        Vector3 pointType = new Vector3(type.position.x, type.position.y, -10);
        Vector3 pointCamera = new Vector3(transform.position.x, transform.position.y, -10);

        transform.position = Vector3.Lerp(pointCamera, pointType, 0.5f * Time.deltaTime * speed);

        //如果目前為劇情動畫演試階段，則判斷是否完成跟隨
        if (cameraState != CameraState.ingPoit) return;

        float distance = Vector3.Distance(pointType, pointCamera);
        //距離小於設定的完成距離時
        if(distance < followFinalDistance && isPoitFollow)
        {
            SE_FLLW_END();
            isPoitFollow = false;
        }
    }



}
