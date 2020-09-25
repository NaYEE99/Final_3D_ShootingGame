
using UnityEngine;

public class NPC : MonoBehaviour
{

    #region 欄位區域
    public GameObject panelDialog;
    [Header("名稱")]
    //public Text textName;
    [Header("內容")]
    //public Text textContent;
    [Header("打字速度"), Range(0.01f, 10)]
    public float printSpeed;
    [Header("打字音效")]

    public AudioClip soundPrint;
    [Header("任務區塊")]
    public RectTransform panelMission;
    [Header("任務數量")]
    //public Text textMission;
    [Header("傳送門")]
    public GameObject[] doors;  

    private AudioSource aud;    // 放置音效
    private Player player;      // 抓取玩家腳本    

    [Header("任務計數器")]
    public int lCrys;       // 白結晶 任務計數器
    public int dCrys;       // 暗結晶 任務計數器

    [Header("光祝福")]
    [Header("祝福狀態")]
    public int lBuff;       // 回傳給 Player 計 光祝福 能力
    [Header("暗祝福")]
    public int bBuff;       // 回傳給 Player 計 暗祝福 能力

    [Header("光汙染")]
    [Header("汙染狀態")]
    public bool lPollution = false;    // 設定汙染狀態
    [Header("暗汙染")]
    public bool dPollution = false;     // 設定汙染狀態



    // 欄位區域結束
    #endregion


    private void Awake()
    {
        
    }

    private void Update()
    {
        
    }


    #region 方法區域















    // 方法區域結束
    #endregion










}
