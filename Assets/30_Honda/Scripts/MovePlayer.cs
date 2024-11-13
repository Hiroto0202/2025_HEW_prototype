using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    Rigidbody2D m_rb;
    Vector2 m_moveForward;             // 移動方向
    public float m_moveSpeed = 70.0f;  // 移動速度
    public int m_pocket = 0;           // 所持金

    // 移動キー
    KeyCode m_upKey = KeyCode.UpArrow;
    KeyCode m_downKey = KeyCode.DownArrow;
    KeyCode m_leftKey = KeyCode.LeftArrow;
    KeyCode m_rightKey = KeyCode.RightArrow;

    // Start is called before the first frame update
    void Start()
    {
        // コンポーネントの取得
        m_rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // ゲームマネージャーのカウントダウン後に実行
        if (GameObject.Find("GameManager").GetComponent<GameManager>().m_countdown <= 0.0f)
        {
            m_moveForward = new Vector2(0.0f, 0.0f);    // 移動方向のセット

            // 各キーで移動方向を変える
            if (Input.GetKey(m_upKey))
            {
                m_moveForward.y = 1.0f;
            }
            if (Input.GetKey(m_downKey))
            {
                m_moveForward.y = -1.0f;
            }
            if (Input.GetKey(m_leftKey))
            {
                m_moveForward.x = -1.0f;
            }
            if (Input.GetKey(m_rightKey))
            {
                m_moveForward.x = 1.0f;
            }

            m_rb.velocity = Vector2.zero;   // キーが押されている間だけ移動するようにする
            ++m_pocket; // 所持金を増やす
        }
    }
    private void FixedUpdate()
    {
        // 移動方向に指定された速度で移動
        m_rb.AddForce(m_moveForward * m_moveSpeed, ForceMode2D.Force);

    }
}
