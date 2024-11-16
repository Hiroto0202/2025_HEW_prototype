using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class Enemy : MonoBehaviour
{
    Rigidbody2D m_rb;               // Rigidbody2D取得
    public Vector2 m_moveForward;   // 移動度
    public float m_speed = 0.5f;    // 移動速度

    public Vector2 m_position;      // これのポジション

    float m_startTime = 0;          // 生成時の時間
    float m_elapsedTime = 0;        // 生成されてから経過した時間
    public float m_deleteTime = 5.0f;   // 消えるまでの時間

    public bool m_deleteFlg = false;        // 消すフラグ

    public GameObject m_prefub;     // prefub生成用インスタンス 
    GameObject m_obj;               // prefub保存用インスタンス
    public float m_discSize = 5.0f; // 認識範囲の大きさ
    public float m_multi = 2.0f;    // 目標を発見したときの認識範囲の倍率

    GameObject m_target;            // 目標入力用インスタンス
    Transform m_targetTrans;        // 目標の情報

    public InputAction m_throw;     // ごみを投げる

    public bool _dFlg = false;              // ゴミとの接触判定
    bool _pFlg = false;              // プレイヤーとの接触判定

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody2D取得
        m_rb = GetComponent<Rigidbody2D>();

        // 生成時の時間を保存
        m_startTime = Time.time;

        // prefub(認識範囲)生成
        Vector3 _spawnVec = new Vector3(this.transform.position.x, this.transform.position.y, 0.01f);
        m_obj = Instantiate(m_prefub, _spawnVec, Quaternion.identity);
        m_obj.transform.localScale = Vector3.one * m_discSize;


        // 移動度初期化
        m_moveForward.x = 1.0f;

        // 目標を探して情報取得
        m_target = GameObject.Find("Player");
        m_targetTrans = m_target.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // 経過時間を取得
        m_elapsedTime += Time.deltaTime;

        // m_deleteTime秒経過したなら
        if (m_elapsedTime > m_deleteTime)
        {
            m_deleteFlg = true;
        }

        // これのポジションを取って、prefubのポジションに入れる
        m_position = this.transform.position;
        m_obj.transform.position = m_position;


        //if(_pFlg==true)
        //{
        //    Discovery();
        //}

        // 接触判定が真なら
        if (_dFlg == true)
        {

            if (_pFlg != true)
            {
                Battle();
            }
            else
            {
                Discovery();
            }
        }

    }

    private void FixedUpdate()
    {
    }

    void Battle()
    {
        m_obj.transform.localScale = Vector3.one * m_discSize * 2;

    }

    // 目標を見つけた時の処理
    void Discovery()
    {
        m_obj.transform.localScale = Vector3.one * m_discSize * m_multi;


        transform.position = Vector3.MoveTowards(transform.position, m_targetTrans.position, m_speed * Time.deltaTime);
        transform.LookAt2D(m_targetTrans.position, Vector2.up);

    }

    // 指定したキーが押された時
    private void OnEnable()
    {
        m_throw.Enable();
        m_throw.performed += Throw;
    }

    // 指定したキーが押されていないとき
    private void OnDisable()
    {
        m_throw.Disable();
        m_throw.performed -= Throw;
    }

    // 目標を発見
    private void Throw(InputAction.CallbackContext context)
    {
        _dFlg = m_obj.GetComponent<Discover>().m_battleflg;
        _pFlg = m_obj.GetComponent<Discover>().m_targetflg;
    }
}



public static class TransformExtensions
{
    // LookAt2D関数(自分のTransform,目標のTransform,正面のVector2)
    public static void LookAt2D(this Transform _self, Transform _target, Vector2 _forward)
    {
        LookAt2D(_self, _target, _forward);
    }

    // LookAt2D関数(自分のTransform,目標のVector3,目標のVector2)
    public static void LookAt2D(this Transform _self, Vector3 _target, Vector2 _forward)
    {
        var _forwardDiff = GetForwardDiffPoint(_forward);
        Vector3 _direction = _target - _self.position;
        float _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        _self.rotation = Quaternion.AngleAxis(_angle - _forwardDiff, Vector3.forward);
    }

    // 目標に対して上を向けるか右を向けるか
    static private float GetForwardDiffPoint(Vector2 _forward)
    {
        if (Equals(_forward, Vector2.up)) return 90;
        if (Equals(_forward, Vector2.right)) return 0;

        return 0;
    }

}