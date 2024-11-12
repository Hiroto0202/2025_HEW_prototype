using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int m_maxEnemyNum = 20;                          // 敵の最大出現数
    public GameObject m_player = null;
    public GameObject m_enemyPrefab = null;
    public GameObject m_build = null;
    GameObject m_newEnemy = null;                           // 生成された敵
    List<GameObject> m_enemyList = new List<GameObject>();  // 現在出現している敵のリスト

    float m_elapsedTime = 0.0f;                 // 経過時間
    float m_timerElapsedTime = 0.0f;            // タイマーの経過時間
    bool m_counterFg = false;                   // 時間計測フラグ
    public TextMeshProUGUI m_timeText = null;   // 経過時間のテキスト
    public float m_createEnemyTime = 1.0f;      // 敵を生成するか調べる間隔
    public float m_deleteEnemyTime = 5.0f;      // 敵を削除するか調べる間隔

    // 敵の出現範囲
    [Header("敵の出現範囲")]
    public float m_enemyMinX = 0;
    public float m_enemyMaxX = 0;
    public float m_enemyMinY = 0;
    public float m_enemyMaxY = 0;

    // Start is called before the first frame update
    void Start()
    {
        // ----- 最初に最大数敵を配置する ----- //
        for(int i = 0; i < m_maxEnemyNum; ++i)
        {
            CreateEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ----- 時間を計測する処理 ----- //
           m_elapsedTime += Time.deltaTime;    // 経過時間の加算

        // エンターキーが押された時、計測の切り替え
        if (Input.GetKeyDown(KeyCode.Return))
        {
            m_counterFg = !m_counterFg; // 反対の値に変更
        }

        // 時間を計測
        if(m_counterFg == true)
        {
            m_timerElapsedTime += Time.deltaTime;    // 経過時間の加算
            //Debug.Log("計測中:" + (m_elapsedTime).ToString());
        }

        // 時間を表示
        //m_timeText.text = m_elapsedTime.ToString();
        m_timeText.text = m_timerElapsedTime.ToString();


        // ----- 削除フラグの立っている敵をフェードアウト後に消す ----- //
        DeleteEnemy();

        // ----- 敵をランダムな位置に作成する処理 ----- //
        if (m_enemyList.Count < m_maxEnemyNum)
        {
            CreateEnemy();
        }

        // ----- 一定時間経過後にビルド要素を表示 ----- //
        if(m_elapsedTime > 10.0f)
        {

        }
    }

    // ランダムな位置を生成する
    private Vector2 GetRandomPosition()
    {
        // 決められた範囲内で生成
        float _x = Random.Range(m_enemyMinX, m_enemyMaxX);
        float _y = Random.Range(m_enemyMinY, m_enemyMaxY);

        return new Vector2(_x, _y); // 座標を返す
    }

    // 
    private void CreateEnemy()
    {
        m_newEnemy = Instantiate(m_enemyPrefab);                // 生成
        m_enemyList.Add(m_newEnemy);                            // リストに追加
        m_newEnemy.transform.position = GetRandomPosition();    // ランダムな位置にする
    }

    // 削除フラグの立っている敵を1体ずつ削除
    private void DeleteEnemy()
    {
        // およそ一定秒間隔ごとに調べる
        if (m_timerElapsedTime > 1.0f)
        {
            int _array = Random.Range(0, m_enemyList.Count - 1);
            // 敵のスクリプト内の削除フラグが立っている時
            if (m_enemyList[_array].GetComponent<MoveEnemy>().m_deleteFg == true)
            {
                Destroy(m_enemyList[_array]);
                m_enemyList.RemoveAt(_array);
                Debug.Log(_array.ToString() + "番目を削除した");
            }
            m_timerElapsedTime = 0;
        }
    }
}
