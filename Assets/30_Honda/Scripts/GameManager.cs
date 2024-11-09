using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject m_player = null;
    public GameObject m_enemyPrefab = null;
    float m_elapsedTime = 0.0f; // �o�ߎ���
    bool m_counterFg = false;   // ���Ԍv���t���O


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �G���^�[�L�[�������ꂽ���A�v���̐؂�ւ�
        if(Input.GetKeyDown(KeyCode.Return))
        {
            m_counterFg = !m_counterFg; // ���΂̒l�ɕύX
        }

        // ���Ԃ��v��
        if(m_counterFg == true)
        {
            m_elapsedTime += Time.deltaTime;    // �o�ߎ��Ԃ̉��Z
            Debug.Log("�v����:" + (m_elapsedTime).ToString());
        }

        if(m_elapsedTime % 100.0f == 0)
        {
            CreateObject(m_enemyPrefab);
        }
    }

    // �I�u�W�F�N�g�𐶐�����
    private void CreateObject(GameObject _gameObject)
    {
        GameObject _newObject;
        _newObject = Instantiate(_gameObject, this.transform.position, Quaternion.identity);
    }
}
