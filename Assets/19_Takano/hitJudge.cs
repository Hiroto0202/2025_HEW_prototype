using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class hitJudge : MonoBehaviour
{
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
        
    }
}
