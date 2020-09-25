
using UnityEngine;

public class Player : MonoBehaviour
{
    // 第一人稱用

    #region 欄位區域
    [Header("速度"), Range(0, 10f)]
    public float speed = 1;
    [Header("旋轉速度"), Range(0, 5)]
    public float turn = 1;
    [Header("血量"), Range(0, 200)]
    public float hp = 100;
    [Header("攻擊值"), Range(0, 100)]
    public float attack = 33;


    [Header("白結晶"), Range(0, 5)]
    public int lCrys = 0;           // 預設上限為5
    [Header("暗結晶"), Range(0, 5)]
    public int dCrys = 0;           // 預設上限為5


    private Rigidbody rig;      // 抓取剛體用 
    private Animator ani;       // 抓取動畫控制器用
    private AudioSource aud;    // 抓取音源用
    private Transform cam;      // 設定攝影機根物件 座標資訊
    private NPC npc;            // 抓取 NPC 類型用


    // 使此欄位在屬性面板上隱藏
    [HideInInspector]
    /// <summary>
    /// 停止使玩家不能移動
    /// </summary>
    public bool stop = false;   // 給 NPC 進行控制



    // 欄位區域結束
    #endregion

    //＝＝＝＝＝＝＝＝＝＝＞ 分隔線 say Hi~ (OwO) ＜
   
    #region 事件區域

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();    // 抓取物件上的 鋼體
        ani = GetComponent<Animator>();     // 抓取物件上的 動畫控制器
        cam = GameObject.Find("主攝影機").transform;  // 在場景直接搜尋("")中的物件

        npc = FindObjectOfType<NPC>();      // 尋找 NPC 類型
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(stop == true) return;    // 假設stop啟用，則跳過移動。

        Move();     // 更新移動
        
    }


    // 事件區域結束
    #endregion

    //＝＝＝＝＝＝＝＝＝＝＞ 分隔線 OTZ ＜

    #region 方法區域

    /// <summary>
    /// 移動方法：使用 Input.GetAxis("Vertical"、"Horizontal")來達成鍵位偵測
    /// </summary>
    private void Move()
    {
        float v = Input.GetAxis("Vertical");                            // Vertical為讀取玩家的↑↓ＷＳ的鍵位  
        float h = Input.GetAxis("Horizontal");                          // Horizontal為讀取玩家的→←ＡＤ的鍵位  
        Vector3 pos = cam.forward * v + cam.right * h;                  // 移動座標pos = 攝影機.前方 * v + 攝影機.右方 * h
        rig.MovePosition(transform.position + pos * speed / 8);        // 移動座標(原本座標 + pos 乘上 速度)
      
        if (v != 0 || h != 0)
        {
            pos.y = 0;      // 設定座標 y 固定不動，以防人物模型傾斜
            Quaternion angle = Quaternion.LookRotation(pos);                            // B 角度angle = 面向角度
            transform.rotation = Quaternion.Slerp(transform.rotation, angle, turn / 10);     // A 角度 = 角度.插值(A角度, B角度, 旋轉速度)
        }

    }

    /// <summary>
    /// 玩家移動限制器
    /// </summary>
    private void StopMove()
    {
        stop = true;    // 使玩家停止移動
    }
    private void ReStop()
    {
        stop = false;   // 使玩家恢復移動
    }

    /// <summary>
    /// 奔跑，源於 Move 調整 speed 加速
    /// </summary>
    private void Run()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift)||Input.GetAxis("Vertical") != 0||Input.GetAxis("Vertical") > 0)
        {
            if (speed < 2) speed = speed * 1.1f;

            Mathf.Clamp(speed, 0, 2);
        }

        

    }


    /// <summary>
    /// 玩家攻擊：觸發攻擊動畫、怪物扣血、
    /// </summary>
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ani.SetTrigger("攻擊觸發");

            
        }
    }

    /// <summary>
    /// 玩家受傷：觸發受傷動畫、玩家扣血
    /// </summary>
    private void GetHit()
    {
        
    }

    /// <summary>
    /// 玩家死亡：判斷是否死亡、觸發死亡動畫、跳出結算畫面
    /// </summary>
    private void Dead()
    {
        if (hp <= 0) ani.SetBool("死亡開關", true);     // 當 HP > = 0 時，觸發死亡開關
    }

    /// <summary>
    /// 使用技能：確認是否達成條件、觸發"NPC"破壞
    /// </summary>
    public void Skill()
    {
        // 粒子效果：職業技能
        // 消除：光汙染
        // 消除：暗汙染


    }

    /// <summary>
    /// 光祝福：觸發回血、設定回復量為定值(可*2)、計數器 ++ (max = 2)
    /// </summary>
    public void RestoreHP()
    {
        // 增加 HP
        // 更新 Bar條
    }
    /// <summary>
    /// 停止回血：取消光祝福更新、清空計數器
    /// </summary>
    private void StopResHP()
    {
        // 停止RHP
    }

    /// <summary>
    /// 暗祝福：觸發攻擊力上升、設定攻擊力為( 1.5/2 倍)、計數器 ++ (max = 2)
    /// </summary>
    public void AttackUP()
    {
        // 增加 attack 傷害值
    }
    /// <summary>
    /// 停止增傷：取消暗祝福更新、清空計數器
    /// </summary>
    private void StopAttackUP()
    {
        // 停止增傷
    }

    /// <summary>
    /// 光汙染：使 HP 每秒 -5
    /// </summary>
    private void LPollution()
    {
        // HP = HP-5
    }
    /// <summary>
    /// 暗汙染：直接觸發 Dead，結束遊戲
    /// </summary>
    private void DPollution()
    {
        // if(time >= 20) Dead();
    }



    /// <summary>
    /// 碰觸到任務道具時，消除道具，計數器+1
    /// </summary>
    /// <param name="collision">標籤為：光結晶</param>
    private void OnCollisionEnter(Collision LightCrystal)  // 當碰撞器進入時，帶入參數
    {
        if (LightCrystal.gameObject.tag == "光結晶") GetProp(LightCrystal.gameObject);
        // 如果 碰撞器標籤 == "光結晶"，觸發 獲取物品
        
        //if (LightCrystal.gameObject.tag == "暗結晶") GetProp(DarkCrystal.gameObject);
        // 如果 碰撞器標籤 == "暗結晶"，觸發 獲取物品
    }

    /// <summary>
    /// 獲取光結晶：消除物品、更新 NPC計數器
    /// </summary>
    /// <param name="prop">光結晶</param>
    private void GetProp(GameObject prop)
    {
        Destroy(prop);  // 指定 prop 為物件，碰到時即刪除物件
        //aud.PlayOneShot(soundProp);
        npc.UpdateTextLCrys();      // 更新計數
        npc.UpdateTextDCrys();      // 更新計數

        //npc.UpdateTextMission();
    }



    // 方法區域結束
    #endregion

}
