using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 m_moveForward;
    public KeyCode W = KeyCode.W;
    public KeyCode A = KeyCode.A;
    public KeyCode S = KeyCode.S;
    public KeyCode D = KeyCode.D;
    public float m_speed = 30.0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        m_moveForward = new Vector3(0.0f, 0.0f, 0.0f);

        if (Input.GetKey(W))
        {
            m_moveForward.y = 1.0f;
        }

        if (Input.GetKey(A))
        {
            m_moveForward.x = -1.0f;
        }

        if (Input.GetKey(S))
        {
            m_moveForward.y = -1.0f;
        }

        if (Input.GetKey(D))
        {
            m_moveForward.x = 1.0f;
        }

        rb.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        rb.AddForce(m_moveForward * m_speed, ForceMode2D.Force);
    }
}
