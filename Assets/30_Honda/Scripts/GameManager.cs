using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;
using System.Runtime.CompilerServices;
//using System;

public class GameManager : MonoBehaviour
{
    [Header("ゲームオブジェクトの設定")]
    public int m_maxEnemyNum = 20;                          // 敵の最大出現数
    public GameObject m_player = null;
    public GameObject m_enemyPrefab = null;                 
    GameObject m_newEnemy = null;                           // 生成された敵
    List<GameObject> m_enemyList = new List<GameObject>();  // 現在出現している敵のリスト
    public GameObject m_pauseMenu = null;                   // ポーズメニュー
    public GameObject m_buildMenu = null;                   // ビルドメニュー
    public GameObject m_phaseMenu = null;                   // フェーズメニュー

    [Header("時間関係の設定")]
    float m_timerElapsedTime = 0.0f;                // タイマーの経過時間
    float m_elapsedTime = 0.0f;                     // 経過時間
    public float m_timeLimit = 10.0f;               // 制限時間
    public float m_countdown = 3.0f;                // カウントダウンする秒数
    int m_count = 0;                                // カウントダウン表示用
    bool m_counterFg = false;                       // 時間計測フラグ
    public float m_deleteEnemyTime = 5.0f;          // 敵を削除するか調べる間隔

    // 敵の出現範囲
    [Header("敵の出現範囲")]
    public float m_enemyMinX = 0;
    public float m_enemyMaxX = 0;
    public float m_enemyMinY = 0;
    public float m_enemyMaxY = 0;

    // UI
    [Header("UI")]
    public TextMeshProUGUI m_timerText = null;      // タイマーのテキスト
    public TextMeshProUGUI m_timeLimitText = null;  // 経過時間のテキスト
    public TextMeshProUGUI m_countdownText = null;  // カウントダウンのテキスト
    public TextMeshProUGUI m_phaseText = null;      // フェーズのテキスト
    public TextMeshProUGUI m_weatherText = null;    // 天気のテキスト
    public TextMeshProUGUI m_pocketText = null;     // 所持金のテキスト
    public TextMeshProUGUI m_moneyText = null;      // すべての所持金のテキスト
    public TextMeshProUGUI m_endText = null;        // 終了メッセージのテキスト

    // キー設定
    [Header("キー設定")]
    public InputAction m_startTimer;  // タイマーをスタートする
    public InputAction m_nextPhase;   // ビルド画面から次のシーンに移る
    public InputAction m_pause;       // ポーズ画面に移る

    bool m_pauseFg = false;     // ポーズメニューかどうか
    bool m_buildFg = false;     // ビルドメニューかどうか

    int m_money = 0;            // すべての所持金
    int m_addMoneyCnt = 0;      // お金を合算した回数

    int m_phaseCnt = 0;         // 何フェーズ目かのカウント      
    hitJudge m_hitJudge;


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        // カウントダウンする
        if (m_countdown > 0.0f)
        {
            m_countdown -= Time.deltaTime;      // カウントダウン
            m_count = (int)m_countdown;         // 整数で表示
            m_countdownText.text = m_count.ToString("0.00s"); 
        }
        // カウントダウン後にゲームの処理
        else
        {
            m_phaseMenu.SetActive(false);       // フェーズメニューを非表示
            m_countdownText.enabled = false;    // カウントダウンを非表示
            m_timeLimitText.enabled = true;     // 制限時間を表示
            m_timerText.enabled = true;         // タイマーを表示
            m_pocketText.enabled = true;        // タイマーを表示

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
            }

            // UIを表示
            m_timeLimitText.text = m_timeLimit.ToString("TimeLimit:0.00s");      // 制限時間
            m_timerText.text = m_timerElapsedTime.ToString("Timer:0.00s");       // タイマー
            m_pocketText.text = m_hitJudge.m_pocket.ToString("Pocket:$000000");// 所持金


            // ----- 削除フラグの立っている敵をフェードアウト後に消す ----- //
            DeleteEnemy();

            // ----- 敵をランダムな位置に作成する処理 ----- //
            if (m_enemyList.Count < m_maxEnemyNum)
            {
                //StartCoroutine(FadeIn());
                CreateEnemy();
            }

            // ----- ポーズ画面の状態切り替え ----- //
            TogglePause();

            // ----- 制限時間経過後に3フェーズ中の時ビルドメニューを表示 ----- //
            ToggleBuild();
        }
    }

    //====================================================
    // 初期化処理
    //====================================================
    private void Init()
    {
        // 敵が存在する場合
        if(m_enemyList.Count > 0)
        {
            // すべての敵を削除する
            for (int i = m_enemyList.Count - 1; i >= 0; --i)
            {
                Destroy(m_enemyList[i]);
                m_enemyList.RemoveAt(i);
            }
        }

        m_hitJudge = m_player.GetComponent<hitJudge>();
        m_pauseFg = false;          // ポーズメニューにしない
        m_buildFg = false;          // ビルドメニューにしない       
        m_timerElapsedTime = 0.0f;  // タイマーの経過時間
        m_elapsedTime = 0.0f;       // 経過時間
        m_timeLimit = 10.0f;        // 制限時間
        m_countdown = 3.0f;         // カウントダウンする秒数
        m_count = 0;                // カウントダウン表示用
        m_addMoneyCnt = 0;          // 合算カウントをリセット
        m_hitJudge.m_pocket = 0;  // 所持金をリセット
        m_player.transform.position = new Vector3(0.0f, 0.0f, 0.0f);    // 位置をリセット

        // 何フェーズ目かによって表示を変更
        if(m_phaseCnt == 0)
        {
            m_phaseText.text = "Asa";
            m_phaseCnt = 1;   // フェーズを進める
        }
        else if(m_phaseCnt == 1)
        {
            m_phaseText.text = "Hiru";
            m_phaseCnt = 2;   // フェーズを進める
        }
        else if(m_phaseCnt == 2)
        {
            m_phaseText.text = "Yoru";
            m_phaseCnt = 3;   // フェーズを進める
        }

        // 天気を変更
        m_weatherText.text = "Hare";

        m_phaseMenu.SetActive(true);    // フェーズメニューを表示
        m_countdownText.enabled = true; // カウントダウンを表示
        m_timeLimitText.enabled = false;// 制限時間を非表示
        m_timerText.enabled = false;    // タイマーを非表示
        m_pocketText.enabled = false;   // 所持金を非表示
        m_endText.enabled = false;      // 終了メッセージを非表示
        m_pauseMenu.SetActive(false);   // ポーズメニューを非表示
        m_buildMenu.SetActive(false);   // ビルドメニューを非表示

        Time.timeScale = 1.0f;          // 時間を進める
        Debug.Log("Init実行！");
    }

    //====================================================
    // 敵をランダムな位置に生成する
    //====================================================
    private void CreateEnemy()
    {
        m_newEnemy = Instantiate(m_enemyPrefab);                // 生成
        m_enemyList.Add(m_newEnemy);                            // リストに追加

        // 決められた範囲内で生成
        float _x = Random.Range(m_enemyMinX, m_enemyMaxX);
        float _y = Random.Range(m_enemyMinY, m_enemyMaxY);
        m_newEnemy.transform.position = new Vector2(_x, _y);    // ランダムな位置にする
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
                //StartCoroutine(FadeOut(m_enemyList[_array], _array));
                //フェードアウト後に敵を削除
                Destroy(m_enemyList[_array]);
                m_enemyList.RemoveAt(_array);
                Debug.Log(_array.ToString() + "番目を削除した");
            }
            m_timerElapsedTime = 0;
        }
    }

    //====================================================
    // ポーズ状態の切り替え
    //====================================================
    private void TogglePause()
    {
        // ESCキーでポーズ状態の切り替え
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_pauseFg = !m_pauseFg;
        }

        // ポーズする
        if (m_pauseFg == true)
        {
            Time.timeScale = 0.0f;  // 時間を止める
            m_pauseMenu.SetActive(true);
            //Debug.Log("ポーズ中");

            // ビルドメニューでなければbackspaceキーでゲームを終了
            if (Input.GetKeyDown(KeyCode.Backspace) && m_buildFg == false)
            {
                // UnityEditorでの実行なら再生モードを解除
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                // ビルド後ならアプリケーションを終了
#else
    Application.Quit();
#endif
            }
        }
        // ポーズを解除
        else
        {
            m_pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;  // 時間を再開
            //Debug.Log("ポーズ解除");
        }
    }

    //====================================================
    // ビルド状態の切り替え
    //====================================================
    private void ToggleBuild()
    {
        if (m_timeLimit < 0)
        {
            m_buildFg = true;
            if (m_phaseCnt < 3)
            {
                // 一回のみ
                if (m_addMoneyCnt == 0)
                {
                    m_money += m_hitJudge.m_pocket;    // 所持金をすべての所持金に合算
                    ++m_addMoneyCnt;
                }
                Time.timeScale = 0.0f;      // 時間を止める
                m_moneyText.text = m_money.ToString("Money:$000000");       // すべての所持金
                m_buildMenu.SetActive(true);

                // エンターキーが押されたら初期化する
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Init();
                }
            }
            // 終了メッセージを表示
            else
            {
                Time.timeScale = 0.0f;      // 時間を止める
                m_endText.enabled = true;

                // エンターキーが押されたら初期化する
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    m_phaseCnt = 0;
                    Init();
                }
            }
        }
    }

    ////====================================================
    //// オブジェクトをフェードインさせるコルーチン
    ////====================================================
    //private IEnumerator FadeIn()
    //{
    //    CreateEnemy();

    //    SpriteRenderer _spriteRenderer = m_newEnemy.GetComponent<SpriteRenderer>();
    //    Color _spriteColor = _spriteRenderer.color;             // 現在のRGBAを取得
    //    _spriteColor.a = 0.0f;                                  // 透明にする

    //    float _duration = 0.5f;                                 // フェードにかける時間
    //    float _targetAlpha = 1.0f;                              // 最終のアルファ値

    //    // 現在のアルファ値が指定された値でない間
    //    while (!Mathf.Approximately(_spriteColor.a, _targetAlpha))
    //    {
    //        float _changeSpeed = Time.deltaTime / _duration;   // 透明度の変化値を求める

    //        // 求めた変化値ごとにアルファ値を変更する
    //        _spriteColor.a = Mathf.MoveTowards(_spriteColor.a, _targetAlpha, _changeSpeed);
    //        _spriteRenderer.color = _spriteColor;
    //        yield return null;
    //    }
    //}

    ////====================================================
    //// オブジェクトをフェードアウトさせるコルーチン
    ////====================================================
    //private IEnumerator FadeOut(GameObject _target, int _array)
    //{
    //    SpriteRenderer _spriteRenderer = _target.GetComponent<SpriteRenderer>();
    //    Color _spriteColor = _spriteRenderer.color;     // 現在のRGBAを取得
    //    float _duration = 0.5f;                         // フェードにかける時間
    //    float _targetAlpha = 0.0f;                      // 最終のアルファ値

    //    // 現在のアルファ値が指定された値でない間
    //    while (!Mathf.Approximately(_spriteColor.a, _targetAlpha))
    //    {
    //        float _changeSpeed = Time.deltaTime / _duration;   // 透明度の変化値を求める

    //        // 求めた変化値ごとにアルファ値を変更する
    //        _spriteColor.a = Mathf.MoveTowards(_spriteColor.a, _targetAlpha, _changeSpeed);
    //        _spriteRenderer.color = _spriteColor;
    //        yield return null;
    //    }
    //    // フェードアウト後に敵を削除
    //    Destroy(_target);
    //    m_enemyList.RemoveAt(_array);
    //    Debug.Log(_array.ToString() + "番目を削除した");
    //}

    //====================================================
    // インプットアクション
    //====================================================
    //private void OnEnable()
    //{
    //    m_pause.Enable();   // アクションを有効化し、キーが押されたかを見る
    //    m_pause.performed += TogglePause;   // アクションにTogglePause関数を割り当てる
    //}

    //private void OnDisable()
    //{
    //    m_pause.Disable();  // アクションを無効化する
    //    m_pause.performed -= TogglePause;   // イベントから削除する
    //}

    //// ポーズ状態の切り替え
    //private void TogglePause(InputAction.CallbackContext context)
    //{
    //    // ポーズ状態の切り替え
    //    m_pauseFg = !m_pauseFg;

    //    // ポーズする
    //    if (m_pauseFg == true)
    //    {
    //        Time.timeScale = 0.0f;  // 時間を止める
    //        m_pauseMenu.SetActive(true);
    //        Debug.Log("ポーズ中");
    //    }
    //    // ポーズを解除
    //    else
    //    {
    //        m_pauseMenu.SetActive(false);
    //        Time.timeScale = 1.0f;  // 時間を再開
    //        Debug.Log("ポーズ解除");
    //    }
    //}
}
