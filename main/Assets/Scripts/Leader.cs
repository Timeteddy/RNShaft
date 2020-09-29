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
        PlotControl.SE_CHECKIN_START += checkInStart;
        PlotControl.SE_CHECKIN_END += checkInEnd;

        myCamera.SE_FLLW_START += cameraFollowStart;
        myCamera.SE_FLLW_END += cameraFollowEnd;
    }
    
    void Update()
    {
        
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
