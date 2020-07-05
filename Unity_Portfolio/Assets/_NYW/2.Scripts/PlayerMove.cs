using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5.0f;
    public float damage = 10.0f;

    public static PlayerMove instance;

    public void Awake()
    {
        PlayerMove.instance = this;
    }

    CharacterController cc;
    Animator anim;

    public VariableJoystick joystick;       //조이스틱

    public GameObject bulletFactory;
    public GameObject firePoint;
    public GameObject attackEffect;


    //오브젝트 풀링
    int poolSize = 20;
    int fireIndex = 0;
    //리스트
    public List<GameObject> bulletPool;

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
        //오브젝트 풀링 초기화
        InitObjectPooling();
    }

    private void InitObjectPooling()
    {
        //2. 리스트
        bulletPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletFactory);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
        //Damage();
        //Die();
        
    }

    private void Die()
    {
        if (PlayerHpBar.instance.isDie)
        {
            anim.SetBool("isDie", true);
        }
    }

    private void Damage()
    {
        if (PlayerHpBar.instance.isDamaged == true)
        {
            anim.SetBool("isDamaged", true);
        }
        else if(PlayerHpBar.instance.isDamaged == false)
        {
            anim.SetBool("isDamaged", false);
        }
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        //조이스틱으로 움직이기
        //if(h == 0 && v == 0)
        //{
        //    h = joystick.Horizontal;
        //    v = joystick.Vertical;
        //}

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
        if ((Input.GetButtonDown("Jump") || PlayerButton.instance.isJump) && jumpCount < 2)
        {
            anim.SetBool("Jump", true);
            velocityY = jumpPower;
            jumpCount++;
        }
    }

    private void Attack()
    {
        if(Input.GetMouseButtonDown(0) || PlayerButton.instance.isAttack == true)
        {
            int randomAtk = UnityEngine.Random.Range(1,4);
            if (randomAtk == 1)
            {
                anim.SetTrigger("Attack1");
                //2. 리스트 오브젝트풀링으로 총알발사         
                bulletPool[fireIndex].SetActive(true);
                bulletPool[fireIndex].transform.position = firePoint.transform.position;
                bulletPool[fireIndex].transform.forward = firePoint.transform.forward;
                fireIndex++;
                if (fireIndex >= poolSize) fireIndex = 0;
            }
            else if(randomAtk == 2)
            {
                anim.SetTrigger("Attack2");
                //2. 리스트 오브젝트풀링으로 총알발사         
                bulletPool[fireIndex].SetActive(true);
                bulletPool[fireIndex].transform.position = firePoint.transform.position;
                bulletPool[fireIndex].transform.forward = firePoint.transform.forward;
                fireIndex++;
                if (fireIndex >= poolSize) fireIndex = 0;
            }
            else if(randomAtk == 3)
            {
                anim.SetTrigger("Attack3");
                //2. 리스트 오브젝트풀링으로 총알발사         
                bulletPool[fireIndex].SetActive(true);
                bulletPool[fireIndex].transform.position = firePoint.transform.position;
                bulletPool[fireIndex].transform.forward = firePoint.transform.forward;
                fireIndex++;
                if (fireIndex >= poolSize) fireIndex = 0;
            }
        }
    }


}
