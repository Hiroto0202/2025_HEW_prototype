using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class test : MonoBehaviour
{
    public InputAction m_aim;

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

    private void Update()
    {
        // move����E�X�e�B�b�N�̓��͂��擾
        Vector2 moveInput = m_aim.ReadValue<Vector2>();
        Debug.Log("Right Stick Input: " + moveInput);

        // moveInput���g���ăL�����N�^�[�̌�����ړ��Ȃǂ𐧌�ł��܂��B
    }
}
