using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update
   // public InputAction inputAction;

    public float fMoveSpeed = 20.0f;//弾のスピード
    public GameObject BulletObj;//弾のオブジェクト
    public float speed=5.0f;//プレイヤーの速度
    Vector2 playerMoveVec;//プレイヤーの進む向き
    Rigidbody2D player_rb;
    Rigidbody2D testRb;
    GameObject testObj;
    //Rigidbody2D _bullet;

    public float aimSpeed = 10f; // エイムの速度
    private Vector2 aimInput; // 入力されたエイムの方向
    public InputAction m_aim;
    private Gamepad gamepad;
    public float deceleration = 0.98f; // 減速率（調整可能）


    [SerializeField] private float time = 0;

    void Start()
    {   
        testObj = Instantiate(BulletObj);
        testRb = testObj.GetComponent<Rigidbody2D>();
        InvokeRepeating("RepeatMethod", 1.0f, 1.0f);
        player_rb=GetComponent<Rigidbody2D>();
        time = 2.0f;

    }

    private void OnEnable()
    {
        // InputActionの有効化
        m_aim.Enable();
    }

    private void OnDisable()
    {
        // InputActionの無効化
        m_aim.Disable();
    }


    // Update is called once per frame
    void Update()
    {

        Key();




        time += Time.deltaTime;
        //スティックの読み込み
        playerMoveVec = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    
        // moveから右スティックの入力を取得
        Vector2 moveInput = m_aim.ReadValue<Vector2>();
        Debug.Log("Right Stick Input: " + moveInput);
        //角度を計算
        //if (moveInput.sqrMagnitude > 0.1f)
        {
            float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg; ;

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        //time = 0;

        //testRb.AddForce(moveVec2 * 10, ForceMode2D.Force);


        Debug.Log(playerMoveVec);
        player_rb.velocity = playerMoveVec*speed;//左スティック

        // スティックの入力がある場合に発射   Fire1はAボタン
        if (moveInput.sqrMagnitude > 0.1f && gamepad.leftTrigger.wasPressedThisFrame)
        {
            Shoot(moveInput);
        }

    }




    //弾発射の関数
    private void Shoot(Vector2 aimDirection)    
    {
        
        //if (time > 2.0f)
        {
            //弾のインスタンス生成
            GameObject cloneObj;//クローンの変数
        cloneObj = Instantiate(BulletObj, transform.position, Quaternion.identity);//複製
        Rigidbody2D rb;
        Vector2 shootDirection = aimDirection.normalized;// 弾の向きを計算して設定
        rb = cloneObj.GetComponent<Rigidbody2D>();//座標を取得する
        rb.AddForce(shootDirection * fMoveSpeed, ForceMode2D.Impulse);//力を加える
            time = 0.0f;
        }
      

    }
    

    void RepeatMethod()
    {

    }
    void Key()
    {
        // 接続されたゲームパッドを取得
        gamepad = Gamepad.current;
        if (gamepad == null)
        {
            Debug.Log("ゲームパッドが接続されていません");
            return;
        }

        // ゲームパッドの各ボタンをチェックし、押されている場合にログ出力
        if (gamepad.buttonSouth.wasPressedThisFrame) Debug.Log("Aボタンが押されました");
        if (gamepad.buttonEast.wasPressedThisFrame) Debug.Log("Bボタンが押されました");
        if (gamepad.buttonWest.wasPressedThisFrame) Debug.Log("Xボタンが押されました");
        if (gamepad.buttonNorth.wasPressedThisFrame) Debug.Log("Yボタンが押されました");

        if (gamepad.leftShoulder.wasPressedThisFrame) Debug.Log("左肩ボタンが押されました");
        if (gamepad.rightShoulder.wasPressedThisFrame) Debug.Log("右肩ボタンが押されました");

        if (gamepad.leftTrigger.wasPressedThisFrame) Debug.Log("左トリガーが押されました");
        if (gamepad.rightTrigger.wasPressedThisFrame) Debug.Log("右トリガーが押されました");

        if (gamepad.startButton.wasPressedThisFrame) Debug.Log("スタートボタンが押されました");
        if (gamepad.selectButton.wasPressedThisFrame) Debug.Log("セレクトボタンが押されました");

        if (gamepad.dpad.up.wasPressedThisFrame) Debug.Log("D-Pad 上が押されました");
        if (gamepad.dpad.down.wasPressedThisFrame) Debug.Log("D-Pad 下が押されました");
        if (gamepad.dpad.left.wasPressedThisFrame) Debug.Log("D-Pad 左が押されました");
        if (gamepad.dpad.right.wasPressedThisFrame) Debug.Log("D-Pad 右が押されました");
    }
}
