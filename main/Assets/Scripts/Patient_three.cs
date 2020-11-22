using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Patient_three : MonoBehaviour
{
    #region 宣告
    public Animator anim;
    [Header("主治醫生")]
    public Doctors_three doctors;
    /// <summary>能否開啟題目 </summary>
    private bool isOpenTopic;
    [Header("Npc資料")]
    public NpcData npcData;

    [Header("特別題目(手)")]
    public GameObject conditon;
    [Header("特別題目(本體)")]
    public GameObject answers;

    [Header("對話進度")]
    public int dlgeSchedule;
    [Header("能否進行點擊觀看下個對話")]
    public bool isNexDialogue = false;
    [Header("對話系統")]
    public DialogueSystem dlge;
    [Header("遊戲控制器")]
    public GameManager GM;
    [Header("關閉題目按鈕")]
    public Button btnConditon;

    /// <summary>護士開始對話 </summary>
    public bool isTock = false;
    #endregion

    #region 啟動
    void Awake()
    {
        dlgeSchedule = 0;

        npcData._TaskState = TaskState.start;

        GameMachine.SE_TYPWRTR_START += typewriterStart;
        GameMachine.SE_TYPWRTR_END += typewriterEnd;
        isTock = false;
        btnConditon.interactable = false;
    }
    #endregion

    #region 重複
    void Update()
    {
        if (GM.onGetDialoguePeople() != "PatientThree") return;
        onClickMouseDown();
    }
    #endregion

    #region 點擊滑鼠或畫面
    /// <summary>
    /// 點擊滑鼠或畫面
    /// </summary>
    private void onClickMouseDown()
    {
        if (!isNexDialogue) return;

        if (Input.GetMouseButtonDown(0))
        {
            switch (npcData._TaskState)
            {
                case TaskState.start:
                    if (dlgeSchedule >= npcData.start.Length)
                    {
                        npcData._TaskState = TaskState.ing;
                        dlge.onDisplayWindow(false);
                        dlge.setName(null);
                        dlgeSchedule = 0;
                        doctors.topic.SetActive(true);
                        conditon.SetActive(true);
                        StartCoroutine(isOpenConditon());
                        return;
                    }
                    dlge.setConten(npcData.start[dlgeSchedule]);
                    break;
                case TaskState.ing:
                    
                    break;
                case TaskState.lose:
                   
                    break;
                case TaskState.finished:
                    
                    break;
                default:
                    break;
            }
        }
    }
    #endregion

    #region 痛苦動畫開始
    /// <summary>
    /// 痛苦動畫開始
    /// </summary>
    public void onJitterStart()
    {
        anim.SetBool("Jitter", true);
        doctors.GM.audioS.clip = doctors.GM.mscBgmTnsn;
        doctors.GM.audioS.Play();
    }
    #endregion

    #region 痛苦動畫結束
    /// <summary>
    /// 痛苦動畫開始
    /// </summary>
    public void onJitterEnd()
    {
        anim.SetBool("Jitter", false);
        doctors.GM.audioS.clip = doctors.GM.mscBgmBass;
        doctors.GM.audioS.Play();
    }
    #endregion

    #region 進入觸發區
    void OnTriggerEnter2D(Collider2D evt)
    {
        if (doctors.npcData._TaskState != TaskState.ing) return;
        isOpenTopic = true;
    }
    #endregion

    #region 進入觸發區
    void OnTriggerExit2D(Collider2D evt)
    {
        if (doctors.npcData._TaskState != TaskState.ing) return;
        isOpenTopic = false;
    }
    #endregion

    #region 打字效果開始
    /// <summary>
    /// 打字效果開始
    /// </summary>
    private void typewriterStart()
    {
        isNexDialogue = false;
    }
    #endregion

    #region 打字效果結束
    /// <summary>
    /// 打字效果結束
    /// </summary>
    private void typewriterEnd()
    {
        if (GM.onGetDialoguePeople() != "PatientThree") return;

        dlgeSchedule++;

        isNexDialogue = true;
    }
    #endregion

    #region 劇情演出
    /// <summary>
    /// 開啟題目
    /// </summary>
    public void btnOpenTopic()
    {
        if (!isTock) return;
        if (!isOpenTopic) return;
        if (doctors.npcData._TaskState != TaskState.ing) return;
        onJitterStart();
        GM.onSetReadlyDialogue("PatientThree");
        dlge.onDisplayWindow(true);
        dlge.setName(npcData._name);
        doctors.GM.onChangePlayerStatePlot();
        dlge.setConten(npcData.start[dlgeSchedule]);
    }
    #endregion

    #region 3秒後才可關閉圖片
    IEnumerator isOpenConditon()
    {
        yield return new WaitForSeconds(3.0f);
        btnConditon.interactable = true;
    }
    #endregion

    #region 按鈕，題目顯示
    public void btnOpenReelTopic()
    {
        doctors.topic.SetActive(true);
        answers.SetActive(true);
        conditon.SetActive(false);
        doctors.GM.btnMscMenu();
    }
    #endregion
}
