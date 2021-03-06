﻿using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public GameObject main;
    public Text tName;
    public Text tConten;

    [Header("音效控制器")]
    public AudioSource audioS;
    [Header("選單")]
    public AudioClip mscOpenMenu;

    private void Awake()
    {
        onDisplayWindow(false);
    }

    /// <summary>
    /// 是否開啟視窗
    /// </summary>
    /// <param name="isOpen">開啟?</param>
    public void onDisplayWindow(bool isOpen)
    {
        if(isOpen) audioS.PlayOneShot(mscOpenMenu, 1.0f);
        main.SetActive(isOpen);
    }

    /// <summary>
    /// 設定名稱
    /// </summary>
    /// <param name="name">名子</param>
    public void setName(string name)
    {
        tName.text = name;
    }

    /// <summary>
    /// 設定對話內容
    /// </summary>
    /// <param name="conten">對話內容</param>
    public void setConten(string conten)
    {
        GameMachine.Typewriter(tConten, conten);
    }
}
