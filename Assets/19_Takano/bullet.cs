using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float fMoveSpeed = 7.0f;
    public GameObject BulletObj;
    Vector2 moveVec2 = new Vector2(1.0f, 0.0f);

    GameObject testObj;
    Rigidbody2D testRb;
    int a = 0;

    [SerializeField] private float time = 0;

    void Start()
    {
        testObj = Instantiate(BulletObj);
        testRb = testObj.GetComponent<Rigidbody2D>();
        InvokeRepeating("RepeatMethod", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            a = 1;
            GameObject cloneObj;
           cloneObj= Instantiate(BulletObj);
            Rigidbody2D rb;
            //cloneObj.transform.Translate(moveVec2.x,Time.deltaTime,0,0);
            rb= cloneObj.GetComponent<Rigidbody2D>();
            rb.AddForce(moveVec2, ForceMode2D.Force);
        }

        if (Input.GetKey(KeyCode.Return))
        {
            testRb.AddForce(moveVec2, ForceMode2D.Force);
        }


        if (time > 0.1f)
        {
            time = 0;
            
            testRb.AddForce(moveVec2 * 10, ForceMode2D.Force);
        }
    }
    void RepeatMethod()
    {

    }
}
