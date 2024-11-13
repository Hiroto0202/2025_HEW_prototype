using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    Rigidbody2D m_rb;
    Vector2 m_moveForward;             // �ړ�����
    public float m_moveSpeed = 70.0f;  // �ړ����x
    public int m_pocket = 0;           // ������

    // �ړ��L�[
    KeyCode m_upKey = KeyCode.UpArrow;
    KeyCode m_downKey = KeyCode.DownArrow;
    KeyCode m_leftKey = KeyCode.LeftArrow;
    KeyCode m_rightKey = KeyCode.RightArrow;

    // Start is called before the first frame update
    void Start()
    {
        // �R���|�[�l���g�̎擾
        m_rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // �Q�[���}�l�[�W���[�̃J�E���g�_�E����Ɏ��s
        if (GameObject.Find("GameManager").GetComponent<GameManager>().m_countdown <= 0.0f)
        {
            m_moveForward = new Vector2(0.0f, 0.0f);    // �ړ������̃Z�b�g

            // �e�L�[�ňړ�������ς���
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

            m_rb.velocity = Vector2.zero;   // �L�[��������Ă���Ԃ����ړ�����悤�ɂ���
            ++m_pocket; // �������𑝂₷
        }
    }
    private void FixedUpdate()
    {
        // �ړ������Ɏw�肳�ꂽ���x�ňړ�
        m_rb.AddForce(m_moveForward * m_moveSpeed, ForceMode2D.Force);

    }
}
