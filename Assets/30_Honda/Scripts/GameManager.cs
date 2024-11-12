using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int m_maxEnemyNum = 20;                          // �G�̍ő�o����
    public GameObject m_player = null;
    public GameObject m_enemyPrefab = null;
    public GameObject m_build = null;
    GameObject m_newEnemy = null;                           // �������ꂽ�G
    List<GameObject> m_enemyList = new List<GameObject>();  // ���ݏo�����Ă���G�̃��X�g

    float m_elapsedTime = 0.0f;                 // �o�ߎ���
    float m_timerElapsedTime = 0.0f;            // �^�C�}�[�̌o�ߎ���
    bool m_counterFg = false;                   // ���Ԍv���t���O
    public TextMeshProUGUI m_timeText = null;   // �o�ߎ��Ԃ̃e�L�X�g
    public float m_createEnemyTime = 1.0f;      // �G�𐶐����邩���ׂ�Ԋu
    public float m_deleteEnemyTime = 5.0f;      // �G���폜���邩���ׂ�Ԋu

    // �G�̏o���͈�
    [Header("�G�̏o���͈�")]
    public float m_enemyMinX = 0;
    public float m_enemyMaxX = 0;
    public float m_enemyMinY = 0;
    public float m_enemyMaxY = 0;

    // Start is called before the first frame update
    void Start()
    {
        // ----- �ŏ��ɍő吔�G��z�u���� ----- //
        for(int i = 0; i < m_maxEnemyNum; ++i)
        {
            CreateEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ----- ���Ԃ��v�����鏈�� ----- //
           m_elapsedTime += Time.deltaTime;    // �o�ߎ��Ԃ̉��Z

        // �G���^�[�L�[�������ꂽ���A�v���̐؂�ւ�
        if (Input.GetKeyDown(KeyCode.Return))
        {
            m_counterFg = !m_counterFg; // ���΂̒l�ɕύX
        }

        // ���Ԃ��v��
        if(m_counterFg == true)
        {
            m_timerElapsedTime += Time.deltaTime;    // �o�ߎ��Ԃ̉��Z
            //Debug.Log("�v����:" + (m_elapsedTime).ToString());
        }

        // ���Ԃ�\��
        //m_timeText.text = m_elapsedTime.ToString();
        m_timeText.text = m_timerElapsedTime.ToString();


        // ----- �폜�t���O�̗����Ă���G���t�F�[�h�A�E�g��ɏ��� ----- //
        DeleteEnemy();

        // ----- �G�������_���Ȉʒu�ɍ쐬���鏈�� ----- //
        if (m_enemyList.Count < m_maxEnemyNum)
        {
            CreateEnemy();
        }

        // ----- ��莞�Ԍo�ߌ�Ƀr���h�v�f��\�� ----- //
        if(m_elapsedTime > 10.0f)
        {

        }
    }

    // �����_���Ȉʒu�𐶐�����
    private Vector2 GetRandomPosition()
    {
        // ���߂�ꂽ�͈͓��Ő���
        float _x = Random.Range(m_enemyMinX, m_enemyMaxX);
        float _y = Random.Range(m_enemyMinY, m_enemyMaxY);

        return new Vector2(_x, _y); // ���W��Ԃ�
    }

    // 
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
        if (m_timerElapsedTime > 1.0f)
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
}
