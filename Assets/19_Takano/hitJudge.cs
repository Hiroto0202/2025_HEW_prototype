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
        // �Փ˂����I�u�W�F�N�g�̃^�O�� "Target" ���m�F
        if (collision.gameObject.tag == "money")
        {
            Debug.Log("Target�ƏՓ˂��܂����I");
            Destroy(collision.gameObject);
            // �����ɏՓˎ��̏���������
        }
    }
}
