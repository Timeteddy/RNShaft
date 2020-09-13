using UnityEngine;

public enum TaskState
{
    start, ing, lose, finished
}

//腳本化物件
[CreateAssetMenu(fileName = "NPC資料", menuName = "角色/NPC 資料")]
public class NpcData : ScriptableObject
{
    [Header("Npc對話狀態")]
    public TaskState _TaskState = TaskState.start;

    public string _name = "";

    [Header("開始對話狀態")]
    public string[] start;
    [Header("進行對話狀態")]
    public string[] ing;
    [Header("玩成對話狀態")]
    public string[] finshed;
    [Header("失敗對話狀態")]
    public string[] lose;
}
