using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    Rigidbody2D m_rb;
    public Vector2 m_moveForward;
    public GameObject m_prefub;
    public float m_speed = 20.0f;

    public Vector2 m_position;

    float m_startTime;

    public float m_despawn = 5.0f;

    GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();

        m_startTime = Time.time;

        Vector3 _spawnVec = new Vector3(this.transform.position.x, this.transform.position.y, 0.01f);
        obj = Instantiate(m_prefub, _spawnVec, Quaternion.identity);

        m_moveForward.x = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_position = this.transform.position;


        obj.transform.position = m_position;

        m_rb.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        //if(m_startTime+m_despawn<Time.time)
        //{
        //    Destroy(this.gameObject);
        //}

        if(Discover.m_targetflg==1)
        {
            Destroy(this.gameObject);
            Discovery();
        }
    }

    private void FixedUpdate()
    {
        //m_rb.AddForce(m_moveForward * m_speed, ForceMode2D.Force);
    }

    void Discovery()
    {
        GameObject _target = GameObject.Find("Player");
        
        Vector2 _pla=_target.transform.position;
        Vector2 _ene=this.transform.position;

        float _x = Mathf.Pow(_pla.x - _ene.x, 2);
        float _y = Mathf.Pow(_pla.y - _ene.y, 2);

        float _z = Mathf.Pow(_x + _y, 0.5f);
    }
}
