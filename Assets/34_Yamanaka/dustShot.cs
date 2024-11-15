using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dustShot : MonoBehaviour
{
    public GameObject m_prefub;

    Vector2 m_moveForward;
    public float m_speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Shot()
    {
        m_moveForward.y = 1.0f;

        Vector3 _vec = new Vector3(this.transform.position.x, this.transform.position.y, -0.01f);
        GameObject m_obj = Instantiate(m_prefub, _vec, Quaternion.identity);

        m_obj.GetComponent<Rigidbody2D>().AddForce(m_moveForward * m_speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
