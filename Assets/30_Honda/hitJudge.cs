using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class hitJudge : MonoBehaviour
{
    public int m_money = 0;
    public int m_pocket = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突したオブジェクトのタグが "Target" か確認
        if (collision.gameObject.tag == "money")
        {
            Debug.Log("moneyと衝突しました！");
            Destroy(collision.gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        m_pocket++;
    }
}
