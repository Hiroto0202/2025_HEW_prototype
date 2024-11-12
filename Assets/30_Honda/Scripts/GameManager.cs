using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int m_maxEnemyNum = 20;                    // �G�̍ő�o����
    public GameObject m_player = null;
    public GameObject m_enemyPrefab = null;
    GameObject m_newEnemy = null;                           // �������ꂽ�G
    List<GameObject> m_enemyList = new List<GameObject>();  // ���ݏo�����Ă���G�̃��X�g

    float m_elapsedTime = 0.0f;     // �o�ߎ���
    bool m_counterFg = false;       // ���Ԍv���t���O
    public TextMeshProUGUI m_timeText = null;

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
            m_newEnemy = Instantiate(m_enemyPrefab);
            m_newEnemy.transform.position = GetRandomPosition();

            m_enemyList.Add(m_newEnemy);    // ���X�g�ɒǉ�
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ----- ���Ԃ��v�����鏈�� ----- //
        // �G���^�[�L�[�������ꂽ���A�v���̐؂�ւ�
        if (Input.GetKeyDown(KeyCode.Return))
        {
            m_counterFg = !m_counterFg; // ���΂̒l�ɕύX
        }

        // ���Ԃ��v��
        if(m_counterFg == true)
        {
            m_elapsedTime += Time.deltaTime;    // �o�ߎ��Ԃ̉��Z
            //Debug.Log("�v����:" + (m_elapsedTime).ToString());
        }

        // ���Ԃ�\��
        m_timeText.text = m_elapsedTime.ToString();


        // ----- �폜�t���O�̗����Ă���G���t�F�[�h�A�E�g��ɏ��� ----- //
        DeleteEnemy();

        // ----- �G�������_���Ȉʒu�ɍ쐬���鏈�� ----- //
        //if (m_elapsedTime >= 5.0f&& m_enemyList.Count < m_maxEnemyNum)
        if (m_elapsedTime % 5.0f < 1.0f && m_enemyList.Count < m_maxEnemyNum)
        {
            m_newEnemy = Instantiate(m_enemyPrefab);    
            m_newEnemy.transform.position = GetRandomPosition();
            //m_elapsedTime = 0.0f;
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

    // �폜�t���O�̗����Ă���G���폜
    private void DeleteEnemy()
    {
        // ���悻1�b���Ƃɒ��ׂ�
        if(m_elapsedTime % 1.0f < 1.0f)
        {
            // ���ׂĂ̗v�f�ɂ���
            for (int i = m_enemyList.Count - 1; i > 0; --i)
            {
                // �X�N���v�g���̍폜�t���O�������Ă��邩�A�G���ő吔���鎞
                if (m_enemyList[i].GetComponent<MoveEnemy>().m_deleteFg  == true && m_enemyList.Count == m_maxEnemyNum) 
                {
                    m_enemyList.RemoveAt(i);
                    Destroy(m_enemyList[i]);
                }
            }
        }
    }
}
