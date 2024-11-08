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
        // 投げるキーが押された時、ゴミを投げる
        if(Input.GetKeyDown(m_throwKey))
        {
            ThrowGarbage(m_garbagePrefab);
        }

    }

    // ゴミを投げる処理
    private void ThrowGarbage(GameObject _garbage)
    {
        GameObject _newGarbage;                         // 作成されたオブジェクト
        Rigidbody2D _rb;
        Vector2 _moveForward = new Vector2(1.0f, 0.0f); // 移動方向

        // プレハブからインスタンスを指定位置に作成
        _newGarbage = Instantiate(_garbage, this.transform.position, Quaternion.identity);
        _rb = _newGarbage.GetComponent<Rigidbody2D>();
        _rb.AddForce(_moveForward * m_throwSpeed, ForceMode2D.Impulse); // 瞬時に移動速度分移動 

        Destroy(_newGarbage, 3.0f); // 3秒後に削除
    }
}
