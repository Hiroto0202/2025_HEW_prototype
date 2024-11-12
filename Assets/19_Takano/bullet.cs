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
    public float aimSpeed = 10f; // �G�C���̑��x
    private Vector2 aimInput; // ���͂��ꂽ�G�C���̕���

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

            if (Input.GetButtonDown("Fire1")) // Fire1 �̓f�t�H���g�� A �{�^���Ƀ}�b�s���O
            {

                //a = 1;

                GameObject cloneObj;//�N���[���̕ϐ�
           cloneObj= Instantiate(BulletObj, transform.position, Quaternion.identity);//����
            Rigidbody2D rb;
            rb= cloneObj.GetComponent<Rigidbody2D>();//���W���擾����
            rb.AddForce(moveVec2*fMoveSpeed, ForceMode2D.Impulse);//�͂�������
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
        if (aimInput.sqrMagnitude > 0.1f) // ���͂�����ꍇ�̂݃G�C�����X�V
        {
            Vector3 aimDirection = new Vector3(aimInput.x, 0, aimInput.y) * aimSpeed * Time.deltaTime;
            transform.LookAt(transform.position + aimDirection);
        }


        void RepeatMethod()
    {

    }
}
