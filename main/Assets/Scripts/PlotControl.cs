using UnityEngine;

/// <summary>
/// 設定委派
/// </summary>
public delegate void deleM();

/// <summary>
/// 劇情狀態機
/// </summary>
public class PlotControl : MonoBehaviour
{
    //宣告事件
    public static event deleM onCall;

    void Start()
    {
        //執行
        onCall();
    }
    
    void Update()
    {
        
    }

    public void deleM()
    {

    }
}
