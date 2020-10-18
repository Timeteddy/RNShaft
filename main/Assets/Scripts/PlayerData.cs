using UnityEngine;

/// <summary>
/// 玩家選擇的形象
/// </summary>
public enum RoleState
{
    not, Baotou, FullyArmed
}
/// <summary>
/// 玩家動作狀態
/// </summary>
public enum ActionState
{
    Idle, getProps, intRoom, ingPolt, readyDialogue, ingDialogue//待機, 撿取物品, 進入房間, 劇情演示, 準備對話, 對話中
}

//腳本化物件
[CreateAssetMenu(fileName = "玩家資料", menuName ="角色/玩家 資料")]
public class PlayerData : ScriptableObject
{
    [Header("玩家形象")]
    public RoleState _RoleState = RoleState.not;
    [Header("玩家動作狀態")]
    public ActionState _actionState = ActionState.Idle;
    [Header("玩家名稱")]
    public string _name = "";
}
