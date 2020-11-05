using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueProcessing : MonoBehaviour
{
    #region 宣告
    public NpcData[] npcData;

    public Player player;
    #endregion

    #region 起始
    void Start()
    {
        setName("@", player.playerData._name);
    }
    #endregion

    #region 按鈕，文章名稱回復
    public void btnReturnOriginal()
    {
        setName(player.playerData._name, "@");
    }
    #endregion

    #region 玩家名稱設定
    /// <summary>
    /// 玩家名稱設定
    /// </summary>
    private void setName(string originalName, string _name)
    {
        int npcLen = npcData.Length;
        for (int i = 0; i < npcLen; i++)
        {
            //開始
            int npcSLen = npcData[i].start.Length;
            for (int s = 0; s < npcSLen; s++)
            {
                //當前對話
                string readStr = npcData[i].start[s];
                //替換後的對話
                string finshedStr = readStr.Replace(originalName, _name);
                //改變當前對話
                npcData[i].start[s] = finshedStr;
            }

            //進行
            int npcILen = npcData[i].ing.Length;
            for (int s = 0; s < npcILen; s++)
            {
                //當前對話
                string readStr = npcData[i].ing[s];
                //替換後的對話
                string finshedStr = readStr.Replace(originalName, _name);
                //改變當前對話
                npcData[i].ing[s] = finshedStr;
            }

            //完成
            int npcFLen = npcData[i].finshed.Length;
            for (int s = 0; s < npcFLen; s++)
            {
                //當前對話
                string readStr = npcData[i].finshed[s];
                //替換後的對話
                string finshedStr = readStr.Replace(originalName, _name);
                //改變當前對話
                npcData[i].finshed[s] = finshedStr;
            }

            //失敗
            int npcLLen = npcData[i].lose.Length;
            for (int s = 0; s < npcLLen; s++)
            {
                //當前對話
                string readStr = npcData[i].lose[s];
                //替換後的對話
                string finshedStr = readStr.Replace(originalName, _name);
                //改變當前對話
                npcData[i].lose[s] = finshedStr;
            }
        }
    }
    #endregion
}
