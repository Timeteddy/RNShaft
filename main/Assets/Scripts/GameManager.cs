using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// 地圖轉移
/// </summary>
public class GameManager : MonoBehaviour
{

    public void betRoom0()
    {
        SceneManager.LoadScene(0);
    }

    public void betRoom1()
    {
        SceneManager.LoadScene(1);
    }

    public void betRoom2()
    {
        SceneManager.LoadScene(2);
    }

     public void betRoom3()
     {
        SceneManager.LoadScene(3);
     }

     public void betRoom4()
     {
        SceneManager.LoadScene(4);
     }

    public void test()
    {
        print("test");//王依柔
    }
}
