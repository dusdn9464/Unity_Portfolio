using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    public float speed = 5.0f;

    CharacterController cc;
    Animator anim;

    public VariableJoystick joystick;       //조이스틱

    //중력적용
    public float gravity = -20f;
    float velocityY;    //낙하속도(벨로시티는 방향과 힘을 들고 있다)
    float jumpPower = 5f;
    int jumpCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        //anim = GetComponent<Animation>();
        //anim.clip = playerAnim.idle;
        //anim.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        dir.Normalize();

        if(h == 0 && v == 0)
        {
            anim.SetBool("Move", false);
        }
        else
        {
            anim.SetBool("Move", true);
        }

        //카메라가 보는 방향으로 이동해야 한다
        dir = Camera.main.transform.TransformDirection(dir);

        velocityY += gravity * Time.deltaTime;
        dir.y = velocityY;
        cc.Move(dir * speed * Time.deltaTime);
        

        if (cc.collisionFlags == CollisionFlags.Below)
        {
            velocityY = 0;
            jumpCount = 0;
            //anim.SetTrigger("Idle");
            anim.SetBool("Jump", false);
        }
        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            anim.SetBool("Jump", true);
            velocityY = jumpPower;
            jumpCount++;
        }
    }

    private void Attack()
    {
        if(Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
        }
    }
}
