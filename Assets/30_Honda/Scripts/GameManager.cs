using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
//using System;

public class GameManager : MonoBehaviour
{
    [Header("�Q�[���I�u�W�F�N�g�̐ݒ�")]
    public int m_maxEnemyNum = 20;                          // �G�̍ő�o����
    public GameObject m_player = null;
    public GameObject m_enemyPrefab = null;                 
    GameObject m_newEnemy = null;                           // �������ꂽ�G
    List<GameObject> m_enemyList = new List<GameObject>();  // ���ݏo�����Ă���G�̃��X�g
    public GameObject m_pauseMenu = null;                   // �|�[�Y�j���[
    public GameObject m_buildMenu = null;                   // �r���h���j���[

    [Header("���Ԋ֌W�̐ݒ�")]
    float m_timerElapsedTime = 0.0f;                // �^�C�}�[�̌o�ߎ���
    float m_elapsedTime = 0.0f;                     // �o�ߎ���
    public float m_timeLimit = 10.0f;               // ��������
    public float m_countdown = 3.0f;                // �J�E���g�_�E������b��
    int m_count = 0;                                // �J�E���g�_�E���\���p
    bool m_counterFg = false;                       // ���Ԍv���t���O
    public float m_deleteEnemyTime = 5.0f;          // �G���폜���邩���ׂ�Ԋu
    public TextMeshProUGUI m_timerText = null;      // �^�C�}�[�̃e�L�X�g
    public TextMeshProUGUI m_timeLimitText = null;  // �o�ߎ��Ԃ̃e�L�X�g
    public TextMeshProUGUI m_countdownText = null;  // �J�E���g�_�E���̃e�L�X�g
    public TextMeshProUGUI m_pocketText = null;     // �������̃e�L�X�g
    public TextMeshProUGUI m_moneyText = null;      // ���ׂĂ̏������̃e�L�X�g

    // �G�̏o���͈�
    [Header("�G�̏o���͈�")]
    public float m_enemyMinX = 0;
    public float m_enemyMaxX = 0;
    public float m_enemyMinY = 0;
    public float m_enemyMaxY = 0;

    // �L�[�ݒ�
    [Header("�L�[�ݒ�")]
    public InputAction m_startTimer;  // �^�C�}�[���X�^�[�g����
    public InputAction m_nextPhase;   // �r���h��ʂ��玟�̃V�[���Ɉڂ�
    public InputAction m_pause;       // �|�[�Y��ʂɈڂ�

    bool m_pauseFg = false;           // �|�[�Y���j���[���ǂ���
    //bool m_buildFg = false;         // �r���h���j���[���ǂ���

    int m_money = 0;                  // ���ׂĂ̏�����
    int m_addMoneyCnt = 0;            // ���������Z������

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        // �J�E���g�_�E������
        if (m_countdown > 0.0f)
        {
            m_countdown -= Time.deltaTime;      // �J�E���g�_�E��
            m_count = (int)m_countdown;         // �����ŕ\��
            m_countdownText.text = m_count.ToString("0.00s"); 
        }
        // �J�E���g�_�E����ɃQ�[���̏���
        else
        {
            m_countdownText.enabled = false;    //�J�E���g�_�E�����\��
            m_timeLimitText.enabled = true;     // �������Ԃ�\��
            m_timerText.enabled = true;         // �^�C�}�[��\��
            m_pocketText.enabled = true;        // �^�C�}�[��\��

            // ----- ���Ԃ��v�����鏈�� ----- //
            m_elapsedTime += Time.deltaTime;    // �o�ߎ��Ԃ̉��Z
            m_timeLimit -= Time.deltaTime;      // �c�莞�Ԃ̌��Z

            // �G���^�[�L�[�������ꂽ���A�v���̐؂�ւ�
            if (Input.GetKeyDown(KeyCode.Return))
            {
                m_counterFg = !m_counterFg; // ���΂̒l�ɕύX
            }

            // ���Ԃ��v��
            if (m_counterFg == true)
            {
                m_timerElapsedTime += Time.deltaTime;    // �o�ߎ��Ԃ̉��Z
                                                         //Debug.Log("�v����:" + (m_elapsedTime).ToString());
            }

            // UI��\��
            m_timeLimitText.text = m_timeLimit.ToString("TimeLimit:0.00s");      // ��������
            m_timerText.text = m_timerElapsedTime.ToString("Timer:0.00s");  // �^�C�}�[
            m_pocketText.text = m_player.GetComponent<MovePlayer>().m_pocket.ToString("Pocket:$000000");  // ������


            // ----- �폜�t���O�̗����Ă���G���t�F�[�h�A�E�g��ɏ��� ----- //
            DeleteEnemy();

            // ----- �G�������_���Ȉʒu�ɍ쐬���鏈�� ----- //
            if (m_enemyList.Count < m_maxEnemyNum)
            {
                CreateEnemy();
            }

            // ----- �������Ԍo�ߌ�Ƀr���h���j���[��\�� ----- //
            if (m_timeLimit < 0)
            {
                // ���̂�
                if(m_addMoneyCnt == 0)
                {
                    m_money += m_player.GetComponent<MovePlayer>().m_pocket;    // �����������ׂĂ̏������ɍ��Z
                    ++m_addMoneyCnt;
                }
                Time.timeScale = 0.0f;      // ���Ԃ��~�߂�
                m_moneyText.text = m_money.ToString("Money:$000000");       // ���ׂĂ̏�����
                m_buildMenu.SetActive(true);

                // �G���^�[�L�[�������ꂽ�珉��������
                if(Input.GetKeyDown(KeyCode.Return))
                {
                    Init();
                }
            }
        }
    }


    //====================================================
    // �T�u���[�`��
    //====================================================
    // ����������
    private void Init()
    {
        // �G�����݂���ꍇ
        if(m_enemyList.Count > 0)
        {
            // ----- ���ׂĂ̓G���폜���� ----- //
            for (int i = m_enemyList.Count - 1; i >= 0; --i)
            {
                Destroy(m_enemyList[i]);
                m_enemyList.RemoveAt(i);
            }
        }

        m_timerElapsedTime = 0.0f;  // �^�C�}�[�̌o�ߎ���
        m_elapsedTime = 0.0f;       // �o�ߎ���
        m_timeLimit = 10.0f;        // ��������
        m_countdown = 3.0f;         // �J�E���g�_�E������b��
        m_count = 0;                // �J�E���g�_�E���\���p
        m_addMoneyCnt = 0;          // ���Z�J�E���g�����Z�b�g
        m_player.GetComponent<MovePlayer>().m_pocket = 0;               // �����������Z�b�g
        m_player.transform.position = new Vector3(0.0f, 0.0f, 0.0f);    // �ʒu�����Z�b�g

        m_countdownText.enabled = true; // �J�E���g�_�E����\��
        m_timeLimitText.enabled = false;// �������Ԃ��\��
        m_timerText.enabled = false;    // �^�C�}�[���\��
        m_pocketText.enabled = false;   // ���������\��
        m_pauseMenu.SetActive(false);   // �|�[�Y���j���[���\��
        m_buildMenu.SetActive(false);   // �r���h���j���[���\��

        Time.timeScale = 1.0f;          // ���Ԃ�i�߂�
    }

    // �����_���Ȉʒu�𐶐�����
    private Vector2 GetRandomPosition()
    {
        // ���߂�ꂽ�͈͓��Ő���
        float _x = Random.Range(m_enemyMinX, m_enemyMaxX);
        float _y = Random.Range(m_enemyMinY, m_enemyMaxY);

        return new Vector2(_x, _y); // ���W��Ԃ�
    }

    // �G�������_���Ȉʒu�ɐ�������
    private void CreateEnemy()
    {
        m_newEnemy = Instantiate(m_enemyPrefab);                // ����
        m_enemyList.Add(m_newEnemy);                            // ���X�g�ɒǉ�
        m_newEnemy.transform.position = GetRandomPosition();    // �����_���Ȉʒu�ɂ���
    }

    // �폜�t���O�̗����Ă���G��1�̂��폜
    private void DeleteEnemy()
    {
        // ���悻���b�Ԋu���Ƃɒ��ׂ�
        if (m_timerElapsedTime > m_deleteEnemyTime)
        {
            int _array = Random.Range(0, m_enemyList.Count - 1);
            // �G�̃X�N���v�g���̍폜�t���O�������Ă��鎞
            if (m_enemyList[_array].GetComponent<MoveEnemy>().m_deleteFg == true)
            {
                Destroy(m_enemyList[_array]);
                m_enemyList.RemoveAt(_array);
                Debug.Log(_array.ToString() + "�Ԗڂ��폜����");
            }
            m_timerElapsedTime = 0;
        }
    }

    // �C���v�b�g�A�N�V����
    private void OnEnable()
    {
        m_pause.Enable();   // �A�N�V������L�������A�L�[�������ꂽ��������
        m_pause.performed += TogglePause;   // �A�N�V������TogglePause�֐������蓖�Ă�
    }

    private void OnDisable()
    {
        m_pause.Disable();  // �A�N�V�����𖳌�������
        m_pause.performed -= TogglePause;   // �C�x���g����폜����
    }

    // �|�[�Y��Ԃ̐؂�ւ�
    private void TogglePause(InputAction.CallbackContext context)
    {
        // �|�[�Y��Ԃ̐؂�ւ�
        m_pauseFg = !m_pauseFg;

        // �|�[�Y����
        if (m_pauseFg == true)
        {
            Time.timeScale = 0.0f;  // ���Ԃ��~�߂�
            m_pauseMenu.SetActive(true);
            Debug.Log("�|�[�Y��");
        }
        // �|�[�Y������
        else
        {
            m_pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;  // ���Ԃ��ĊJ
            Debug.Log("�|�[�Y����");
        }
    }
}
