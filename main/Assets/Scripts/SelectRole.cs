using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public enum StateStart
{
    initRole, gaMeQuestion, setName, role, initGame
}

/// <summary>
/// 選擇角色
/// </summary>
public class SelectRole : MonoBehaviour
{
    /// <summary>
    /// 狀態機
    /// </summary>
    private StateStart stateStart = StateStart.initRole;
    public SceneData sceneData;
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
    [Header("劇情文字")]
    public Text textPlot;
    [Header("劇情布幕")]
    public Image screenPlot;

    /// <summary>
    /// 近退場的速度
    /// </summary>
    private float screenOutSpeed = 0.1f;

    /// <summary>
    /// 換場景的停頓時間
    /// </summary>
    private float changeScenesSpeed = 1.0f;

    [Header("命名視窗")]
    public InputField setNameWindow;

    /// <summary>
    /// 是否能點擊滑鼠
    /// </summary>
    private bool isClickMouse = false;

    [Header("音效控制器")]
    public AudioSource audioS;
    [Header("按鈕音效")]
    public AudioClip mscButton;

    void Start()
    {
        //Screen.SetResolution(1920, 1080, true);

        playerData._name = "";
        follow = originalPoint;
        playerData._RoleState = RoleState.not;
        stateStart = StateStart.initRole;
        btnEnter.gameObject.SetActive(false);
        setNameWindow.gameObject.SetActive(false);

        //監聽
        GameMachine.SE_TYPWRTR_START += SeTypwrtrStart;
        GameMachine.SE_TYPWRTR_END += SeTypwrtrEnd;

        GameMachine.Typewriter(textPlot, sceneData.selectRole[0]);
    }

    void Update()
    {
        Track();

        onClickMouseDown();

    }

    /// <summary>
    /// 點擊滑鼠或畫面
    /// </summary>
    private void onClickMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isClickMouse) return;
            switch (stateStart)
            {
                case StateStart.initRole:
                    break;
                case StateStart.gaMeQuestion:
                    GameMachine.Typewriter(textPlot, sceneData.selectRole[1]);
                    break;
                case StateStart.setName:
                    break;
                case StateStart.role:
                    break;
                case StateStart.initGame:
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 打字機效果開始
    /// </summary>
    private void SeTypwrtrStart()
    {
        isClickMouse = false;
        switch (stateStart)
        {
            case StateStart.initRole:
                break;
            case StateStart.gaMeQuestion:
                break;
            case StateStart.setName:
                break;
            case StateStart.role:
                break;
            case StateStart.initGame:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 打字機效果結束
    /// </summary>
    private void SeTypwrtrEnd()
    {
        isClickMouse = true;
        switch (stateStart)
        {
            case StateStart.initRole:
                //近入遊戲問候結束後跳到問題階段
                stateStart = StateStart.gaMeQuestion;
                break;
            case StateStart.gaMeQuestion:
                //問題階段結束後進入命名階段
                stateStart = StateStart.setName;
                setNameWindow.gameObject.SetActive(true);
                btnEnter.gameObject.SetActive(true);
                break;
            case StateStart.setName:
                //命名階段結束後進入選擇角色階段
                stateStart = StateStart.role;
                break;
            case StateStart.role:
                stateStart = StateStart.initGame;
                StartCoroutine(initGame());
                break;
            case StateStart.initGame:
                break;
            default:
                break;
        }
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
        if (stateStart != StateStart.role) return;

        btnEnter.gameObject.SetActive(true);
        textPrompt.text = sceneData.selectRole[3];
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

    /// <summary>
    /// 換場景的停頓時間
    /// </summary>
    /// <returns></returns>
    IEnumerator initGame()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// 劇情布幕近場
    /// </summary>
    /// <returns></returns>
    IEnumerator screenInit()
    {
        float alfa = screenPlot.color.a;
        screenPlot.gameObject.SetActive(true);
        do
        {
            alfa += 0.05f;
            screenPlot.color = new Color(0, 0, 0, alfa);
            yield return new WaitForSeconds(screenOutSpeed);
        }
        while (screenPlot.color.a < 1);

        GameMachine.Typewriter(textPlot, sceneData.selectRole[4]);
        yield return null;
    }

    /// <summary>
    /// 劇情布幕退場
    /// </summary>
    /// <returns></returns>
    IEnumerator screenOut()
    {
        float alfa = screenPlot.color.a;
        do
        {
            alfa -= 0.05f;
            screenPlot.color = new Color(0, 0, 0, alfa);
            yield return new WaitForSeconds(screenOutSpeed);
        }
        while (screenPlot.color.a > 0);

        screenPlot.gameObject.SetActive(false);
        GameMachine.Typewriter(textPrompt, sceneData.selectRole[2]);
        yield return null;
    }

    public void enter()
    {
        switch (stateStart)
        {
            case StateStart.setName:
                string name = setNameWindow.text;
                int len = name.Length;
                if (len > 0)
                {
                    playerData._name = name;

                    //淡出動畫，已淡出動畫做為下一階段的開場
                    textPlot.text = "";
                    setNameWindow.gameObject.SetActive(false);
                    btnEnter.gameObject.SetActive(false);

                    StartCoroutine(screenOut());
                }
                else
                {
                    playerData._name = "護理師";

                    //淡出動畫，已淡出動畫做為下一階段的開場
                    textPlot.text = "";
                    setNameWindow.gameObject.SetActive(false);
                    btnEnter.gameObject.SetActive(false);

                    StartCoroutine(screenOut());
                    //textPlot.text = sceneData.selectRole[5];
                }
                break;
            case StateStart.role:
                btnEnter.gameObject.SetActive(false);
                StartCoroutine(screenInit());
                break;
            case StateStart.initGame:
                break;
            default:
                break;
        }
    }

    #region 音效播放
    /// <summary>
    /// 按鈕音效
    /// </summary>
    public void btnMscButton()
    {
        audioS.PlayOneShot(mscButton, 1.0f);
    }
    #endregion
}

