using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int m_maxEnemyNum = 20;                    // 敵の最大出現数
    public GameObject m_player = null;
    public GameObject m_enemyPrefab = null;
    GameObject m_newEnemy = null;                           // 生成された敵
    List<GameObject> m_enemyList = new List<GameObject>();  // 現在出現している敵のリスト

    float m_elapsedTime = 0.0f;     // 経過時間
    bool m_counterFg = false;       // 時間計測フラグ
    public TextMeshProUGUI m_timeText = null;

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
            m_newEnemy = Instantiate(m_enemyPrefab);
            m_newEnemy.transform.position = GetRandomPosition();

            m_enemyList.Add(m_newEnemy);    // リストに追加
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ----- 時間を計測する処理 ----- //
        // エンターキーが押された時、計測の切り替え
        if (Input.GetKeyDown(KeyCode.Return))
        {
            m_counterFg = !m_counterFg; // 反対の値に変更
        }

        // 時間を計測
        if(m_counterFg == true)
        {
            m_elapsedTime += Time.deltaTime;    // 経過時間の加算
            //Debug.Log("計測中:" + (m_elapsedTime).ToString());
        }

        // 時間を表示
        m_timeText.text = m_elapsedTime.ToString();


        // ----- 削除フラグの立っている敵をフェードアウト後に消す ----- //
        DeleteEnemy();

        // ----- 敵をランダムな位置に作成する処理 ----- //
        //if (m_elapsedTime >= 5.0f&& m_enemyList.Count < m_maxEnemyNum)
        if (m_elapsedTime % 5.0f < 1.0f && m_enemyList.Count < m_maxEnemyNum)
        {
            m_newEnemy = Instantiate(m_enemyPrefab);    
            m_newEnemy.transform.position = GetRandomPosition();
            //m_elapsedTime = 0.0f;
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

    // 削除フラグの立っている敵を削除
    private void DeleteEnemy()
    {
        // およそ1秒ごとに調べる
        if(m_elapsedTime % 1.0f < 1.0f)
        {
            // すべての要素について
            for (int i = m_enemyList.Count - 1; i > 0; --i)
            {
                // スクリプト内の削除フラグが立っているかつ、敵が最大数いる時
                if (m_enemyList[i].GetComponent<MoveEnemy>().m_deleteFg  == true && m_enemyList.Count == m_maxEnemyNum) 
                {
                    m_enemyList.RemoveAt(i);
                    Destroy(m_enemyList[i]);
                }
            }
        }
    }
}
