using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discover : MonoBehaviour
{
    float m_startTime;

    public bool m_battleflg = false;
    public bool m_targetflg = false;

    // Start is called before the first frame update
    void Start()
    {
        m_startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Dust")
        {
            m_battleflg = true;

        }
        if (col.transform.tag == "Player")
        {
            m_targetflg = true;
        }
        else
        {
            m_targetflg = false;
        }

    }

}
