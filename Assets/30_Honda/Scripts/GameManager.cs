using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("ゲームオブジェクトの設定")]
    public int m_maxEnemyNum = 20;                          // 敵の最大出現数
    public GameObject m_player = null;
    public GameObject m_enemyPrefab = null;                 
    GameObject m_newEnemy = null;                           // 生成された敵
    List<GameObject> m_enemyList = new List<GameObject>();  // 現在出現している敵のリスト
    public GameObject m_pauseMenu = null;                   // ポーズニュー
    public GameObject m_buildMenu = null;                   // ビルドメニュー

    [Header("時間関係の設定")]
    float m_timerElapsedTime = 0.0f;                // タイマーの経過時間
    float m_elapsedTime = 0.0f;                     // 経過時間
    public float m_timeLimit = 10.0f;               // 制限時間
    public float m_countdown = 3.0f;                // カウントダウンする秒数
    int m_count = 0;                                // カウントダウン表示用
    bool m_counterFg = false;                       // 時間計測フラグ
    public float m_deleteEnemyTime = 5.0f;          // 敵を削除するか調べる間隔
    public TextMeshProUGUI m_timerText = null;      // タイマーのテキスト
    public TextMeshProUGUI m_timeText = null;       // 経過時間のテキスト
    public TextMeshProUGUI m_countdownText = null;  // カウントダウンのテキスト

    // 敵の出現範囲
    [Header("敵の出現範囲")]
    public float m_enemyMinX = 0;
    public float m_enemyMaxX = 0;
    public float m_enemyMinY = 0;
    public float m_enemyMaxY = 0;

    // キー設定
    [Header("キー設定")]
    public InputAction m_startTimer;  // タイマーをスタートする
    public InputAction m_nextPhase;   // ビルド画面から次のシーンに移る
    public InputAction m_pause;        // ポーズ画面に移る

    bool m_pauseFg = false;           // ポーズかどうか

    // Start is called before the first frame update
    void Start()
    {
        m_countdownText.enabled = true; //カウントダウンを表示
        m_pauseMenu.SetActive(false);   // ポーズメニューを非表示
        m_buildMenu.SetActive(false);   // ビルドメニューを非表示

        // ----- 最初に最大数敵を配置する ----- //
        for (int i = 0; i < m_maxEnemyNum; ++i)
        {
            CreateEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // カウントダウンする
        if (m_countdown > 0.0f)
        {
            m_countdown -= Time.deltaTime;      // カウントダウン
            m_count = (int)m_countdown;         // 整数で表示
            m_countdownText.text = m_count.ToString();
        }
        // カウントダウン後にゲームの処理
        else
        {
            m_countdownText.enabled = false;    //カウントダウンを非表示

            // ----- 時間を計測する処理 ----- //
            m_elapsedTime += Time.deltaTime;    // 経過時間の加算
            m_timeLimit -= Time.deltaTime;      // 残り時間の減算

            // エンターキーが押された時、計測の切り替え
            if (Input.GetKeyDown(KeyCode.Return))
            {
                m_counterFg = !m_counterFg; // 反対の値に変更
            }

            // 時間を計測
            if (m_counterFg == true)
            {
                m_timerElapsedTime += Time.deltaTime;    // 経過時間の加算
                                                         //Debug.Log("計測中:" + (m_elapsedTime).ToString());
            }

            // 時間を表示
            m_timeText.text = m_timeLimit.ToString();
            m_timerText.text = m_timerElapsedTime.ToString();


            // ----- 削除フラグの立っている敵をフェードアウト後に消す ----- //
            DeleteEnemy();

            // ----- 敵をランダムな位置に作成する処理 ----- //
            if (m_enemyList.Count < m_maxEnemyNum)
            {
                CreateEnemy();
            }

            // ----- 制限時間経過後にビルドメニューを表示 ----- //
            if (m_timeLimit < 0)
            {
                Time.timeScale = 0.0f;      // 時間を止める
                m_buildMenu.SetActive(true);
            }
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

    // 敵をランダムな位置に生成する
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
        if (m_timerElapsedTime > m_deleteEnemyTime)
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

    private void OnEnable()
    {
        m_pause.Enable();   // アクションを有効化し、キーが押されたかを見る
        m_pause.performed += TogglePause;   // アクションにTogglePause関数を割り当てる
    }

    private void OnDisable()
    {
        m_pause.Disable();  // アクションを無効化する
        m_pause.performed -= TogglePause;   // イベントから削除する
    }

    private void TogglePause(InputAction.CallbackContext context)
    {
        // ポーズ状態の切り替え
        m_pauseFg = !m_pauseFg;

        // ポーズする
        if (m_pauseFg == true)
        {
            Time.timeScale = 0.0f;  // 時間を止める
            m_pauseMenu.SetActive(true);
            Debug.Log("ポーズ中");
        }
        // ポーズを解除
        else
        {
            m_pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;  // 時間を再開
            Debug.Log("ポーズ解除");
        }
    }
}
