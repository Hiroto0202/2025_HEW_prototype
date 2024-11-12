using System.Collections;
using System.Collections.Generic;
//yyyusing System.Diagnostics;
//using System.Diagnostics;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update
   // public InputAction inputAction;

    public float fMoveSpeed = 20.0f;//�e�̃X�s�[�h
    public GameObject BulletObj;//�e�̃I�u�W�F�N�g
    public float speed=5.0f;//�v���C���[�̑��x
    Vector2 playerMoveVec;//�v���C���[�̐i�ތ���
    Rigidbody2D player_rb;
    Rigidbody2D testRb;
    GameObject testObj;

    public float aimSpeed = 10f; // �G�C���̑��x
    private Vector2 aimInput; // ���͂��ꂽ�G�C���̕���


    public InputAction m_aim;

    private Gamepad gamepad;


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
        // InputAction�̗L����
        m_aim.Enable();
    }

    private void OnDisable()
    {
        // InputAction�̖�����
        m_aim.Disable();
    }


    // Update is called once per frame
    void Update()
    {

        // �ڑ����ꂽ�Q�[���p�b�h���擾
        gamepad = Gamepad.current;
        if (gamepad == null)
        {
            Debug.Log("�Q�[���p�b�h���ڑ�����Ă��܂���");
            return;
        }

        // �Q�[���p�b�h�̊e�{�^�����`�F�b�N���A������Ă���ꍇ�Ƀ��O�o��
        if (gamepad.buttonSouth.wasPressedThisFrame) Debug.Log("A�{�^����������܂���");
        if (gamepad.buttonEast.wasPressedThisFrame) Debug.Log("B�{�^����������܂���");
        if (gamepad.buttonWest.wasPressedThisFrame) Debug.Log("X�{�^����������܂���");
        if (gamepad.buttonNorth.wasPressedThisFrame) Debug.Log("Y�{�^����������܂���");

        if (gamepad.leftShoulder.wasPressedThisFrame) Debug.Log("�����{�^����������܂���");
        if (gamepad.rightShoulder.wasPressedThisFrame) Debug.Log("�E���{�^����������܂���");

        if (gamepad.leftTrigger.wasPressedThisFrame) Debug.Log("���g���K�[��������܂���");
        if (gamepad.rightTrigger.wasPressedThisFrame) Debug.Log("�E�g���K�[��������܂���");

        if (gamepad.startButton.wasPressedThisFrame) Debug.Log("�X�^�[�g�{�^����������܂���");
        if (gamepad.selectButton.wasPressedThisFrame) Debug.Log("�Z���N�g�{�^����������܂���");

        if (gamepad.dpad.up.wasPressedThisFrame) Debug.Log("D-Pad �オ������܂���");
        if (gamepad.dpad.down.wasPressedThisFrame) Debug.Log("D-Pad ����������܂���");
        if (gamepad.dpad.left.wasPressedThisFrame) Debug.Log("D-Pad ����������܂���");
        if (gamepad.dpad.right.wasPressedThisFrame) Debug.Log("D-Pad �E��������܂���");




        time += Time.deltaTime;
        //�X�e�B�b�N�̓ǂݍ���
        playerMoveVec = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    
        // move����E�X�e�B�b�N�̓��͂��擾
        Vector2 moveInput = m_aim.ReadValue<Vector2>();
        Debug.Log("Right Stick Input: " + moveInput);
       
       
            //time = 0;
            
            //testRb.AddForce(moveVec2 * 10, ForceMode2D.Force);
        

        Debug.Log(playerMoveVec);
        player_rb.velocity = playerMoveVec*speed;

        // �X�e�B�b�N�̓��͂�����ꍇ�ɔ���   Fire1��A�{�^��
        if (moveInput.sqrMagnitude > 0.1f && gamepad.leftTrigger.wasPressedThisFrame)
        {
            Shoot(moveInput);
        }
        //if (gamepad.rightTrigger.wasPressedThisFrame)
    }
    //�e���˂̊֐�
    private void Shoot(Vector2 aimDirection)
    {
        if (time > 2.0f)
        {
            //�e�̃C���X�^���X����
            GameObject cloneObj;//�N���[���̕ϐ�
        cloneObj = Instantiate(BulletObj, transform.position, Quaternion.identity);//����
        Rigidbody2D rb;
        Vector2 shootDirection = aimDirection.normalized;// �e�̌������v�Z���Đݒ�
        rb = cloneObj.GetComponent<Rigidbody2D>();//���W���擾����
        rb.AddForce(shootDirection * fMoveSpeed, ForceMode2D.Impulse);//�͂�������
            time = 0.0f;
        }

    }
    

    void RepeatMethod()
    {

    }
}
