using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
         
            //a = 1;
            
            GameObject cloneObj;//ÉNÉçÅ[ÉìÇÃïœêî
           cloneObj= Instantiate(BulletObj, transform.position, Quaternion.identity);//ï°êª
            Rigidbody2D rb;
            rb= cloneObj.GetComponent<Rigidbody2D>();//ç¿ïWÇéÊìæÇ∑ÇÈ
            rb.AddForce(moveVec2*fMoveSpeed, ForceMode2D.Impulse);//óÕÇâ¡Ç¶ÇÈ
        }


        if (time > 0.1f)
        {
            time = 0;
            
            testRb.AddForce(moveVec2 * 10, ForceMode2D.Force);
        }

        Debug.Log(playerMoveVec);
        player_rb.velocity = playerMoveVec*speed;
    }
    void RepeatMethod()
    {

    }
}
