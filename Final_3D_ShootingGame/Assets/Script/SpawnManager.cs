using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    [Header("怪物")]
    public Transform enemy;
    [Header("生成點")]
    public GameObject[] points;
    [Header("間隔時間"), Range(0f, 5f)]
    public float interval = 1.5f;     //生成時間

    private void Start()
    {
        points = GameObject.FindGameObjectsWithTag("生成點"); // 自動抓取 tag= 生成點的物件

        InvokeRepeating("Spawn", 0, interval);  // InvokeRepeating：指定頻率，重複執行 Invoke 功能
                                                // ("指定方法, 延遲時間, 重複時間")

    }

    private void Spawn()    // 生成方法
    {
        int r = Random.Range(0, points.Length);             // 隨機抓陣列內的編號
        Transform point = points[r].transform;              // 儲存生成點編號
        Instantiate(enemy, point.position, point.rotation); // 生成(物件, 座標, 角度)
    }



}
