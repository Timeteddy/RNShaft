using UnityEngine;

/// <summary>
/// 玩家選擇的形象
/// </summary>
public enum RoleState
{
    not, Baotou, FullyArmed
}

//腳本化物件
[CreateAssetMenu(fileName = "玩家資料", menuName ="角色/玩家 資料")]
public class PlayerData : ScriptableObject
{
    [Header("玩家形象")]
    public RoleState _RoleState = RoleState.not;
}
