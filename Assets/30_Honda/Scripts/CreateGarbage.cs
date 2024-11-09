using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGarbage : MonoBehaviour
{
    public GameObject m_garbagePrefab;
    public float m_throwSpeed = 10.0f;
    KeyCode m_throwKey = KeyCode.Space;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ������L�[�������ꂽ���A�S�~�𓊂���
        if(Input.GetKeyDown(m_throwKey))
        {
            ThrowGarbage(m_garbagePrefab);
        }

    }

    // �S�~�𓊂��鏈��
    private void ThrowGarbage(GameObject _garbage)
    {
        GameObject _newGarbage;                         // �쐬���ꂽ�I�u�W�F�N�g
        Rigidbody2D _rb;
        Vector2 _moveForward = new Vector2(1.0f, 0.0f); // �ړ�����

        // �v���n�u����C���X�^���X���w��ʒu�ɍ쐬
        _newGarbage = Instantiate(_garbage, this.transform.position, Quaternion.identity);
        _rb = _newGarbage.GetComponent<Rigidbody2D>();
        _rb.AddForce(_moveForward * m_throwSpeed, ForceMode2D.Impulse); // �u���Ɉړ����x���ړ� 

        Destroy(_newGarbage, 3.0f); // 3�b��ɍ폜
    }
}
