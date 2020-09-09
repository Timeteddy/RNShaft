using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// 地圖轉移
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("玩家")]
    public Player player;
    [Header("攝影機")]
    public CameraControl myCamera;

    /// <summary>
    /// 各個房間的入口
    /// </summary>
    public Transform[] arrRoomEntrance = new Transform[10];



    public void betRoom0()
    {
    }

    public void betRoom1()
    {
    }

    public void betRoom2()
    {
    }

     public void betRoom3()
     {
     }

     public void betRoom4()
     {
     }

    public void test()
    {
        print("test");//王依柔
    }
}
