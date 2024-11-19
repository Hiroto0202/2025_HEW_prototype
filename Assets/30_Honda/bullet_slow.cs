using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_slow : MonoBehaviour
{
    public float deceleration = 0.98f; // 減速率（調整可能）
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 弾の速度を徐々に減速させる
        rb.velocity *= deceleration;

        // 弾の速度が一定以下になったら削除
        if (rb.velocity.sqrMagnitude < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
