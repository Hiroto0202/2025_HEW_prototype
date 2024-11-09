using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D m_rb;
    public static Vector2 m_moveForward;
    public GameObject m_prefub;
    public float m_speed = 20.0f;


    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        
        GameObject obj = Instantiate(m_prefub, this.transform.position, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {

        m_rb.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
        m_rb.AddForce(m_moveForward * 1.0f, ForceMode2D.Force);
    }
}
