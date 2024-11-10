using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _vec = Enemy.m_position;
        _vec.z = 0.01f;
        this.transform.position = _vec;
    }

    
}
