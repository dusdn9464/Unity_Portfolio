using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5.0f;

    CharacterController cc;

    //중력적용
    public float gravity = -20f;
    float velocityY;    //낙하속도(벨로시티는 방향과 힘을 들고 있다)
    float jumpPower = 5f;
    int jumpCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(jumpCount);
        Move();

    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        dir.Normalize();


        //카메라가 보는 방향으로 이동해야 한다
        dir = Camera.main.transform.TransformDirection(dir);

        velocityY += gravity * Time.deltaTime;
        dir.y = velocityY;
        cc.Move(dir * speed * Time.deltaTime);
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            velocityY = 0;
            jumpCount = 0;
        }
        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            velocityY = jumpPower;
            jumpCount++;
        }

        

    }
}
