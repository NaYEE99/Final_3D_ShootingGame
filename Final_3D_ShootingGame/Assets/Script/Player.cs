
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
    private float hp = 100;
    [Header("攻擊值"), Range(0, 100)]
    private float attack = 33;


    [Header("白結晶"), Range(0, 5)]
    public int lCrys = 0;           // 預設上限為5
    [Header("暗結晶"), Range(0, 5)]
    public int dCrys = 0;           // 預設上限為5


    private Rigidbody rig;      // 抓取剛體用 
    private Animator ani;       // 抓取動畫控制器用
    private AudioSource aud;    // 抓取音源用
    private Transform cam;      // 設定攝影機根物件 座標資訊


    // 欄位區域結束
    #endregion
    //＝＝＝＝＝＝＝＝＝＝＞ 分隔線 say Hi~ (OwO) ＜
    #region 事件區域

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
        cam = GameObject.Find("主攝影機").transform;  // 在場景直接搜尋("")中的物件

    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();     // 持續更新移動
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

    private void StopMove()
    {

    }










    // 方法區域結束
    #endregion

}
