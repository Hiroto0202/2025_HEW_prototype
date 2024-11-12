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
        // InputActionの有効化
        m_aim.Enable();
    }

    private void OnDisable()
    {
        // InputActionの無効化
        m_aim.Disable();
    }

    private void Update()
    {
        // moveから右スティックの入力を取得
        Vector2 moveInput = m_aim.ReadValue<Vector2>();
        Debug.Log("Right Stick Input: " + moveInput);

        // moveInputを使ってキャラクターの向きや移動などを制御できます。
    }
}
