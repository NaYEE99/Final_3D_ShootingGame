using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    [Header("移動速度"), Range(0, 10f)]
    public float speed = 1;
    [Header("血量"), Range(0, 200)]
    public float hp = 100;
    [Header("攻擊值"), Range(0, 100)]
    public float attack = 33;
    [Header("攻擊停止距離"), Range(0, 10f)]
    public float distanceAttack = 1.5f;
    [Header("攻擊冷卻時間"), Range(0, 10f)]
    public float attackCD = 1.5f;
    private float cdtimer;  // 攻擊計時器

    [Header("面向/轉向玩家的速度"), Range(0, 10)]
    public float turnPlayer = 5f;

    [Header("掉落物")]
    public Transform prop;
    [Header("掉落機率：0.1 = 10%"), Range(0f, 1f)]
    public float propDrop = 0.25f;


    private Animator ani;       // 抓取動畫控制器用
    private NavMeshAgent nav;   // 抓取導覽器用
    private Transform player;   // 抓取玩家座標

    private Rigidbody rig;  // 抓取剛體用


    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();     // 導覽代理器抓取
        nav.speed = speed;                      // 使代理器的 speed 由 C# 控制
        nav.stoppingDistance = distanceAttack;  // 使停止距離 = 攻擊停止距離
        ani = GetComponent<Animator>();         // 抓取物件上的動畫控制器

        player = GameObject.Find("Player").transform;   // 抓取 "Player" 的座標資訊
        nav.SetDestination(player.position);            // 避免 物件 一開始就偷打，先設定目的地

        rig = GetComponent<Rigidbody>();        // 抓取剛體
    }

    private void Update()
    {
        Move();
    }


    /// <summary>
    /// 敵人移動：追蹤"Player"座標資訊
    /// </summary>
    private void Move()
    {
        nav.SetDestination(player.position);            // nav.SD 設定目的地(玩家座標)，會產生加速度 float
        ani.SetFloat("移動", nav.velocity.magnitude);   // 設定移動動畫 導覽器.加速度 較值

        if (nav.remainingDistance < distanceAttack) Attack();   // 如果 剩餘距離<攻擊距離，則 Attack();
    }

    /// <summary>
    /// 玩家受傷：觸發受傷動畫、玩家扣血
    /// </summary>
    public void GetHit(float damage, Transform direction)
    {
        hp -= damage;                   // 玩家 HP 遞減 傷害量
        ani.SetTrigger("受傷觸發");     // 觸發受傷動畫
        rig.AddForce(direction.forward * 10 + direction.up * 5);       // 控制 Player.rig 被打擊退+擊飛

        if (hp == 0) Dead();            // 如果 HP = 0，觸發死亡
    }

    /// <summary>
    /// 死亡：關閉腳本、撥放死亡動畫
    /// </summary>
    private void Dead()
    {
        if (hp <= 0) ani.SetBool("死亡開關", true);     // 當 HP > = 0 時，觸發死亡開關
        this.enabled = false;   // 死亡時關閉腳本

        float r = Random.Range(0f, 1f); // 給予一個隨機值
        if (r <= propDrop) Instantiate(prop, transform.position + Vector3.up * 2, transform.rotation);   // 觸發生成掉落物

        GetComponent<Collider>().enabled = false;   // 關閉碰撞器
    }


    /// <summary>
    /// 著色顯示：顯示攻擊停止距離
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.35f);               // 新增出一個顏色(Vector3, 半徑)
        Gizmos.DrawSphere(transform.position, distanceAttack);  // 產生球形(產生座標, 半徑)

    }

    private void Attack()
    {
        Quaternion look = Quaternion.LookRotation(player.position - transform.position);
        // 設定向量資訊 = 向量.看向方向(玩家座標 - 自己座標)
        Quaternion.Slerp(transform.rotation, look, Time.deltaTime * turnPlayer);
        // 角度 = 插值(角度, 面相角度 ,速度)

        cdtimer += Time.deltaTime;          // 攻擊計時器，每秒累加

        if (cdtimer >= attackCD)            // 當 CD時間 >= 攻擊冷卻時間
        {
            cdtimer = 0;                    // 清空計時器
            ani.SetTrigger("攻擊觸發");     // 觸發攻擊
        }


    }

    /// <summary>
    /// 當擊中指定物件：傳送傷害值、自身座標資訊
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {

        if (other.name == "Player") // 當碰撞到的物件 = Player
        {
            float range = Random.Range(-10f, 10f);      // 隨機攻擊力 +-10   
            other.GetComponent<Player>().GetHit(attack + range, transform);    
            // 指定 Player物件，呼叫 受傷(回傳攻擊值 + 隨機值, 自身座標)


        }

    }


}
