using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 m_moveForward;
    public KeyCode W = KeyCode.W;
    public KeyCode A = KeyCode.A;
    public KeyCode S = KeyCode.S;
    public KeyCode D = KeyCode.D;
    public float m_speed = 30.0f;

    public KeyCode Space = KeyCode.Space;

    public GameObject m_prefub=null;
    GameObject m_obj = null;
    Vector2 m_move;
    public float m_power = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        m_move.y = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_moveForward = new Vector2(0.0f, 0.0f);

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

        if (Input.GetKeyDown(Space))
        {

            Vector3 _vec = new Vector3(this.transform.position.x, this.transform.position.y, -0.01f);
            m_obj = Instantiate(m_prefub, _vec, Quaternion.identity);

        }

        rb.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
        rb.AddForce(m_moveForward * m_speed, ForceMode2D.Force);


        if (m_power > 0)
        {
            m_obj.GetComponent<Rigidbody2D>().AddForce(m_move * m_power, ForceMode2D.Impulse);

            m_power -= 0.005f;
        }
        else
        {
            m_power = 0.5f;
        }
    }
}
