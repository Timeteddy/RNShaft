using UnityEngine;

//腳本化物件
[CreateAssetMenu(fileName = "場景資料", menuName = "場景/Scene 資料")]
public class SceneData : ScriptableObject
{
    [Header("選擇角色")]
    public string[] selectRole;

}
