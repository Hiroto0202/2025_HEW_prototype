using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float fMoveSpeed = 7.0f;
    public GameObject BulletObj;
    public float speed=5.0f;
    Vector2 moveVec2 = new Vector2(1.0f, 0.0f);
    Vector2 playerMoveVec;
    Rigidbody2D player_rb;
    Rigidbody2D testRb;
    GameObject testObj;
    public float aimSpeed = 10f; // エイムの速度
    private Vector2 aimInput; // 入力されたエイムの方向

    [SerializeField] private float time = 0;

    void Start()
    {
        testObj = Instantiate(BulletObj);
        testRb = testObj.GetComponent<Rigidbody2D>();
        InvokeRepeating("RepeatMethod", 1.0f, 1.0f);
        player_rb=GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;
        playerMoveVec = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (Input.GetButtonDown("Fire1")) // Fire1 はデフォルトで A ボタンにマッピング
            {

                //a = 1;

                GameObject cloneObj;//クローンの変数
           cloneObj= Instantiate(BulletObj, transform.position, Quaternion.identity);//複製
            Rigidbody2D rb;
            rb= cloneObj.GetComponent<Rigidbody2D>();//座標を取得する
            rb.AddForce(moveVec2*fMoveSpeed, ForceMode2D.Impulse);//力を加える
            }


        if (time > 0.1f)
        {
            time = 0;
            
            testRb.AddForce(moveVec2 * 10, ForceMode2D.Force);
        }

        Debug.Log(playerMoveVec);
        player_rb.velocity = playerMoveVec*speed;

        
    }
    public void OnAim(InputAction.CallbackContext context)
    {
        aimInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        if (aimInput.sqrMagnitude > 0.1f) // 入力がある場合のみエイムを更新
        {
            Vector3 aimDirection = new Vector3(aimInput.x, 0, aimInput.y) * aimSpeed * Time.deltaTime;
            transform.LookAt(transform.position + aimDirection);
        }


        void RepeatMethod()
    {

    }
}
