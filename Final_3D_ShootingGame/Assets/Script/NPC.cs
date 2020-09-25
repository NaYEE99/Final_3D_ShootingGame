using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC : MonoBehaviour
{

    #region 欄位區域

    [Header("NPC 資料")]
    public NPCData data;
    [Header("對話區塊")]
    public GameObject panelDialog;
    [Header("內容")]
    public Text textContent;                // 在下方 Dialog() 有定義
    [Header("打字速度"), Range(0.01f, 10)]
    public float printSpeed;
    [Header("打字音效")]
    public AudioClip soundPrint;
    
    private AudioSource aud;    // 放置音效
    private Player player;      // 抓取玩家腳本    

    // ==新增

    [Header("任務區塊")]
    public RectTransform panelMission;
    [Header("任務計數器")]
    public int lCrys;       // 光結晶 任務計數器
    public int dCrys;       // 暗結晶 任務計數器

    [Header("任務用文字區塊")]
    public Text textLCrys;  // 光結晶 任務文字更新用
    public Text textDCrys;  // 暗結晶 任務文字更新用

    [Header("光祝福")]
    [Header("< 祝福狀態 >")]
    public int lBuff;       // 回傳給 Player 計 光祝福 能力
    [Header("暗祝福")]
    public int bBuff;       // 回傳給 Player 計 暗祝福 能力

    [Header("光汙染")]
    [Header("< 汙染狀態 >")]
    public bool lPollution = false;    // 設定汙染狀態
    [Header("暗汙染")]
    public bool dPollution = false;     // 設定汙染狀態



    // 欄位區域結束
    #endregion


    private void Awake()    // 開始時執行
    {
        aud = GetComponent<AudioSource>();      // 尋找物件上的音效來源

        player = FindObjectOfType<Player>();    // 透過類型尋找物件，僅限場景只有一個類型

        data._NPCState = NPCState.Missioning;   // 設定 NPCdata 為 任務中
    }

    private void Update()
    {
        
    }


    #region 方法區域

    /// <summary>
    /// 任務介面更新：設定 "任務UI" 為顯示，讀取 NPCdata 內的字串內容
    /// </summary>
    public void Dialog()
    {
        panelDialog.SetActive(true);        // SetActive 使[]設定為顯示

        textContent.text = data.dialogs[0];     // 使 "內容" 的文字 = 指定字串的文字

        StartCoroutine(Print());            // 啟動()內的協程，需使用到 System.Collections
    }

    /// <summary>
    /// 文字處理器：附打字效果，逐漸更新至最後一個字
    /// </summary>
    /// <returns></returns>
    private IEnumerator Print()
    {
        string dialog = data.dialogs[(int)data._NPCState];      // 對話內容 = NPC 資料.對話[(轉為整數)data內的資料序號]
        textContent.text = "";                                  // 清空字串

        // 使用迴圈逐格更新文字
        for (int i = 0; i < dialog.Length; i++)         // i < dialog.Length 
        {                                               // 到此字串.最後一個字元

            textContent.text += dialog[i];                  // 對話內容.文字 += 對話[]的字元
            aud.PlayOneShot(soundPrint, 0.5f);              // 撥放打字音效
            yield return new WaitForSeconds(printSpeed);    // 打字延遲時間調整
        }

    }

    /// <summary>
    /// 更新任務計數：光結晶
    /// </summary>
    public void UpdateTextLCrys()
    {
        lCrys++;    // 每次獲得+1

        if (lCrys >= 5) player.RestoreHP();     // 當 光結晶 > = 5 時，觸發回血效果
        // 觸發回血動畫 2D? 3D?

        textLCrys.text = " " +lCrys + " / " + data.lCrys;    // 使文字更新 = 當前數量 / data需求

    }


    /// <summary>
    /// 更新任務計數：暗結晶
    /// </summary>
    public void UpdateTextDCrys()
    {
        dCrys++;    // 每次獲得+1

        if (dCrys >= 5) player.AttackUP();      // 當 暗結晶 > = 5 時，觸發傷害提升
        // 觸發增傷動畫 2D> 3D?

        textDCrys.text = " " + dCrys + " / " + data.dCrys;    // 使文字更新 = 當前數量 / data需求


    }














    // 方法區域結束
    #endregion










}
