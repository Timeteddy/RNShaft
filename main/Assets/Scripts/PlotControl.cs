using UnityEngine;

/// <summary>
/// 設定委派
/// </summary>
public delegate void checkInStart();
public delegate void checkInEnd();

public delegate void roomStart();
public delegate void roomEnd();

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

    /// <summary>房間劇情開始</summary>
    public static event roomStart SE_ROOM_START;

    /// <summary>房間劇情中</summary>
    public static event roomStart SE_ROOM_ING;

    /// <summary>房間劇情結束</summary>
    public static event roomEnd SE_ROOM_END;


    void Start()
    {
        //SE_CHECKIN_START();
    }

    public void onSeChechin_End()
    {
        SE_CHECKIN_END();
    }

    /// <summary>房間劇情開始</summary>
    public void onSeRoomStart()
    {
        SE_ROOM_START();
    }

    /// <summary>房間劇情結束</summary>
    public void onSeRoomIng()
    {
        SE_ROOM_ING();
    }

    /// <summary>房間劇情結束</summary>
    public void onSeRoomEnd()
    {
        SE_ROOM_END();
    }
}
