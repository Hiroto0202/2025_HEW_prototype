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
        // �Փ˂����I�u�W�F�N�g�̃^�O�� "Target" ���m�F
        if (collision.gameObject.tag == "money")
        {
            Debug.Log("money�ƏՓ˂��܂����I");
            Destroy(collision.gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        m_pocket++;
    }
}