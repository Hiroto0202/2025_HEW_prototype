using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class Enemy : MonoBehaviour
{
    Rigidbody2D m_rb;               // Rigidbody2D�擾
    public Vector2 m_moveForward;   // �ړ��x
    public float m_speed = 0.5f;    // �ړ����x

    public Vector2 m_position;      // ����̃|�W�V����

    float m_startTime = 0;          // �������̎���
    float m_elapsedTime = 0;        // ��������Ă���o�߂�������
    public float m_deleteTime = 5.0f;   // ������܂ł̎���

    public bool m_deleteFlg = false;        // �����t���O

    public GameObject m_prefub;     // prefub�����p�C���X�^���X 
    GameObject m_obj;               // prefub�ۑ��p�C���X�^���X
    public float m_discSize = 5.0f; // �F���͈͂̑傫��
    public float m_multi = 2.0f;    // �ڕW�𔭌������Ƃ��̔F���͈͂̔{��

    GameObject m_player;            // �ڕW���͗p�C���X�^���X
    Transform m_playerTrans;        // �ڕW�̏��

    GameObject m_dust;            // �ڕW���͗p�C���X�^���X
    Transform m_dustTrans;        // �ڕW�̏��

    public InputAction m_throw;     // ���݂𓊂���
    KeyCode m_space = KeyCode.Space;

    public bool _dFlg = false;              // �S�~�Ƃ̐ڐG����
    bool _pFlg = false;              // �v���C���[�Ƃ̐ڐG����

    GameObject m_create;

    SpriteRenderer m_fade;
    Color m_spriteColor;

    float m_duration;                             // �t�F�[�h�A�E�g�ɂ����鎞��
    float m_targetAlpha;                             // �ڕW�A���t�@�l

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody2D�擾
        m_rb = GetComponent<Rigidbody2D>();

        // �������̎��Ԃ�ۑ�
        m_startTime = Time.time;



        // �ړ��x������
        m_moveForward.x = 1.0f;

        // �v���C���[��T��
        m_player = GameObject.Find("Player");
        m_playerTrans = m_player.transform;

        //m_create = GameObject.Find("GameManager");

        m_fade = GetComponent<SpriteRenderer>();
        m_spriteColor = m_fade.color;
        m_spriteColor.a = 0;                                 // �����ɂ���

        m_duration = 1f;
        m_targetAlpha = 1;

    }

    // Update is called once per frame
    void Update()
    {
        // �o�ߎ��Ԃ��擾
        m_elapsedTime += Time.deltaTime;

        //�t�F�[�h�C��
        if (m_spriteColor.a < m_targetAlpha)
        {
            float _changeSpeed = Time.deltaTime / m_duration;    // �����x�̕ω����x�����߂�

            // ���߂��ω����x���ƂɃA���t�@�l��ύX����
            m_spriteColor.a += _changeSpeed;
            m_fade.color = m_spriteColor;

            if(m_spriteColor.a >= m_targetAlpha)
            {
                // prefub(�F���͈�)����
                Vector3 _spawnVec = new Vector3(this.transform.position.x, this.transform.position.y, 0.01f);
                m_obj = Instantiate(m_prefub, _spawnVec, Quaternion.identity);
                m_obj.transform.localScale = Vector3.one * m_discSize;
            }
        }
        else
        {
            // ����̃|�W�V����������āAprefub�̃|�W�V�����ɓ����
            m_position = this.transform.position;
            m_obj.transform.position = m_position;


            if(Input.GetKeyDown(m_space))
            {
                _dFlg = true;
                //_pFlg = true;
            }

            // �ڐG���肪�^�Ȃ�
            if (_dFlg == true)
            {

                if (_pFlg != true)
                {
                    //Battle();
                }
                else
                {
                    Discovery();
                }
            }

        }


    }



    private void FixedUpdate()
    {

        //m_deleteTime�b�o�߂����Ȃ�
        if (m_elapsedTime > m_deleteTime)
        {
            m_duration = 0.5f;
            m_targetAlpha = 0;

            //�t�F�[�h�A�E�g
            if (m_spriteColor.a >= m_targetAlpha)
            {
                float _changeSpeed = Time.deltaTime / m_duration;    // �����x�̕ω����x�����߂�

                // ���߂��ω����x���ƂɃA���t�@�l��ύX����
                m_spriteColor.a -= _changeSpeed;
                m_fade.color = m_spriteColor;
            }
            else
            {
                m_deleteFlg = true;
            }

        }

    }


    void Battle()
    {
        m_obj.transform.localScale = Vector3.one * m_discSize * 1.5f;

        // �S�~��T��
        m_dust = GameObject.Find("Enemy");
        m_dustTrans = m_dust.transform;

        transform.position = Vector3.MoveTowards(transform.position, m_dustTrans.position, m_speed * Time.deltaTime);
        transform.LookAt2D(m_dustTrans.position, Vector2.up);

    }

    // �ڕW�����������̏���
    void Discovery()
    {
        m_obj.transform.localScale = Vector3.one * m_discSize * m_multi;


        transform.position = Vector3.MoveTowards(transform.position, m_playerTrans.position, m_speed * Time.deltaTime);
        transform.LookAt2D(m_playerTrans.position, Vector2.up);

    }

    // �w�肵���L�[�������ꂽ��
    private void OnEnable()
    {
        m_throw.Enable();
        m_throw.performed += Throw;
    }

    // �w�肵���L�[��������Ă��Ȃ��Ƃ�
    private void OnDisable()
    {
        m_throw.Disable();
        m_throw.performed -= Throw;
    }

    // �ڕW�𔭌�
    private void Throw(InputAction.CallbackContext context)
    {
        _dFlg = m_obj.GetComponent<Discover>().m_battleflg;
        _pFlg = m_obj.GetComponent<Discover>().m_targetflg;
    }

}



public static class TransformExtensions
{
    // LookAt2D�֐�(������Transform,�ڕW��Transform,���ʂ�Vector2)
    public static void LookAt2D(this Transform _self, Transform _target, Vector2 _forward)
    {
        LookAt2D(_self, _target, _forward);
    }

    // LookAt2D�֐�(������Transform,�ڕW��Vector3,�ڕW��Vector2)
    public static void LookAt2D(this Transform _self, Vector3 _target, Vector2 _forward)
    {
        var _forwardDiff = GetForwardDiffPoint(_forward);
        Vector3 _direction = _target - _self.position;
        float _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        _self.rotation = Quaternion.AngleAxis(_angle - _forwardDiff, Vector3.forward);
    }

    // �ڕW�ɑ΂��ď�������邩�E�������邩
    static private float GetForwardDiffPoint(Vector2 _forward)
    {
        if (Equals(_forward, Vector2.up)) return 90;
        if (Equals(_forward, Vector2.right)) return 0;

        return 0;
    }

}

