using UnityEngine;

/// <summary>
/// 設定委派
/// </summary>
public delegate void checkInStart();
public delegate void checkInEnd();

/// <summary>
/// 劇情狀態機
/// </summary>
public class PlotControl : MonoBehaviour
{
    /// <summary>
    /// 報到劇情開始
    /// </summary>
    public static event checkInStart SE_CHECKIN_START;

    /// <summary>
    /// 報到劇情結束
    /// </summary>
    public static event checkInEnd SE_CHECKIN_END;


    void Start()
    {
        SE_CHECKIN_START();
    }

    public void onSeChechin_End()
    {
        SE_CHECKIN_END();
    }
    
    void Update()
    {
        
    }
}
