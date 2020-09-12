﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 選擇角色
/// </summary>
public class SelectRole : MonoBehaviour
{
    public PlayerData playerData;
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

    [Header("確認按鈕")]
    public Button btnEnter;
    [Header("提示文字")]
    public Text textPrompt;

    void Awake()
    {
        follow = originalPoint;
        playerData._RoleState = RoleState.not;
        btnEnter.gameObject.SetActive(false);
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
        btnEnter.gameObject.SetActive(true);
        textPrompt.text = "確認選擇這個形象嗎?";
        switch (role)
        {
            case "one":
                follow = userOnePoint;
                playerData._RoleState = RoleState.Baotou;
                break;

            case "two":
                follow = userTwoPoint;
                playerData._RoleState = RoleState.FullyArmed;
                break;
        }
    }

    public void enter()
    {
        SceneManager.LoadScene(1);
    }
}
