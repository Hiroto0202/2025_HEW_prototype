using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    Rigidbody2D m_rb;
    public Vector2 m_moveForward;
    public GameObject m_prefub;
    public float m_speed = 0.5f;

    public Vector2 m_position;

    float m_startTime;
    public float m_despawn = 5.0f;


    float É∆ = 0.1f;

    GameObject m_obj;
    GameObject m_target;

    Transform m_playerTrans;



    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();

        m_startTime = Time.time;

        Vector3 _spawnVec = new Vector3(this.transform.position.x, this.transform.position.y, 0.01f);
        m_obj = Instantiate(m_prefub, _spawnVec, Quaternion.identity);

        m_moveForward.x = 1.0f;

        m_target = GameObject.Find("Player");


        m_playerTrans = m_target.transform;
    }

    // Update is called once per frame
    void Update()
    {
        m_position = this.transform.position;

        m_obj.transform.position = m_position;



        if (Discover.m_targetflg == 1)
        {
            Discovery();
        }
        m_rb.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
        //m_rb.AddForce(m_moveForward * m_speed, ForceMode2D.Force);
    }

    void Discovery()
    {
        transform.position = Vector3.MoveTowards(transform.position, m_playerTrans.position, m_speed * Time.deltaTime);
        transform.LookAt2D(m_playerTrans.position, Vector2.up);

    }

}


public static class TransformExtensions
{
    public static void LookAt2D(this Transform _self, Transform _target, Vector2 _forward)
    {
        LookAt2D(_self, _target, _forward);
    }

    public static void LookAt2D(this Transform _self, Vector3 _target, Vector2 _forward)
    {
        var _forwardDiff = GetForwardDiffPoint(_forward);
        Vector3 _direction = _target - _self.position;
        float _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        _self.rotation = Quaternion.AngleAxis(_angle - _forwardDiff, Vector3.forward);
    }

    static private float GetForwardDiffPoint(Vector2 _forward)
    {
        if (Equals(_forward, Vector2.up)) return 90;
        if (Equals(_forward, Vector2.right)) return 0;

        return 0;
    }

}