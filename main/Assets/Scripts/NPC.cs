using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("對話進度")]
    public int dlgeSchedule;
    [Header("能否進行點擊觀看下個對話")]
    public bool isNexDialogue = false;
    [Header("遊戲控制器")]
    public GameManager GM;
    [Header("對話系統")]
    public DialogueSystem dlge;
    [Header("Npc資料")]
    public NpcData npcData;
    [Header("劇情控制器")]
    public PlotControl plotControl;
    [Header("動畫")]
    public Animator anim;
    [Header("要顯示的符號(驚嘆號,問號)")]
    public Renderer symbol;
    [Header("攝影機")]
    public CameraControl myCamera;
}
