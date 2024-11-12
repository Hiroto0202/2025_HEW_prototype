using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public bool m_deleteFg = false;
    float m_elapsedTime = 0.0f;
    public float m_deleteTime = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_elapsedTime += Time.deltaTime;
        Debug.Log("“G‚ªoŒ»‚µ‚Ä‚©‚ç‚Ì•b”:" + (m_elapsedTime).ToString());

        if(m_elapsedTime > m_deleteTime)
        {
            m_deleteFg = true;
            //m_elapsedTime = 0.0f;
        }
    }
}
