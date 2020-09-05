using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 選擇角色
/// </summary>
public class SelectRole : MonoBehaviour
{
    [Header("攝影機")]
    public Transform myCamera;

    [Header("原點")]
    public Transform originalPoint;
    [Header("角色One")]
    public Transform userOnePoint;
    [Header("角色Two")]
    public Transform userTwoPoint;

    public Transform follow;

    [Header("攝影機移動速度")]
    public float speed;

    void Awake()
    {
        follow = originalPoint;
    }

    void Update()
    {
        Track();
    }

    /// <summary>
    /// 攝影機跟隨玩家
    /// </summary>
    private void Track()
    {
        Vector3 pointPlayer = new Vector3(follow.position.x, 0.95f, -10);
        Vector3 pointCamera = new Vector3(myCamera.transform.position.x, 0.95f, -10);

        myCamera.transform.position = Vector3.Lerp(pointCamera, pointPlayer, 0.5f * Time.deltaTime * speed);

    }

    public void betChangeRole(string role)
    {
        switch (role)
        {
            case "one":
                follow = userOnePoint;
                break;

            case "two":
                follow = userTwoPoint;
                break;
        }
    }
}
