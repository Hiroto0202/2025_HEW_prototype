//using System.Diagnostics;
//using System.Diagnostics;
//using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class hitJudge : MonoBehaviour
{
    void Update()
    {

        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突したオブジェクトのタグが "Target" か確認
        if (collision.gameObject.tag == "money")
        {
            Debug.Log("Targetと衝突しました！");
            Destroy(collision.gameObject);
            // ここに衝突時の処理を書く
        }
    }
}
