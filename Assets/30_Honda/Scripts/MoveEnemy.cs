using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public bool m_deleteFg = false;
    float m_elapsedTime = 0.0f;
    public float m_deleteTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �Q�[���}�l�[�W���[�̃J�E���g�_�E����Ɏ��s
        if (GameObject.Find("GameManager").GetComponent<GameManager>().m_countdown <= 0.0f)
        {
            m_elapsedTime += Time.deltaTime;
            //Debug.Log("�G���o�����Ă���̕b��:" + (m_elapsedTime).ToString());

            if (m_elapsedTime > m_deleteTime)
            {
                m_deleteFg = true;
                //Debug.Log("�G���폜:" + (m_deleteFg).ToString());
                //m_elapsedTime = 0.0f;
            }
        }
    }
}
