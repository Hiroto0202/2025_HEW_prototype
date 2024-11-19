using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_slow : MonoBehaviour
{
    public float deceleration = 0.98f; // �������i�����\�j
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // �e�̑��x�����X�Ɍ���������
        rb.velocity *= deceleration;

        // �e�̑��x�����ȉ��ɂȂ�����폜
        if (rb.velocity.sqrMagnitude < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
