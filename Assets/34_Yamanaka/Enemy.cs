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

    public static Vector3 m_position;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();

        Vector3 _spawnVec = new Vector3(this.transform.position.x, this.transform.position.y, 0.01f);
        Instantiate(m_prefub, _spawnVec, Quaternion.identity);

        m_moveForward.x = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_position = this.transform.position;

        m_rb.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
        m_rb.AddForce(m_moveForward * m_speed, ForceMode2D.Force);
    }
}
