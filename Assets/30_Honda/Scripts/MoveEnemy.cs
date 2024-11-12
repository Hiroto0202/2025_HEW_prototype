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
        // ゲームマネージャーのカウントダウン後に実行
        if (GameObject.Find("GameManager").GetComponent<GameManager>().m_countdown <= 0.0f)
        {
            m_elapsedTime += Time.deltaTime;
            //Debug.Log("敵が出現してからの秒数:" + (m_elapsedTime).ToString());

            if (m_elapsedTime > m_deleteTime)
            {
                m_deleteFg = true;
                //Debug.Log("敵を削除:" + (m_deleteFg).ToString());
                //m_elapsedTime = 0.0f;
            }
        }
    }
}
