using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    enum BossState
    {
        Idle,Attack1,Attack2,Attack3, Damaged,Die
    }

    BossState state;

    Animator anim;

    Transform player;

    //공격 딜레이
    float attTime = 1f;
    float timer = 0f;

    //공격 이펙트
    //attack1
    public GameObject notice;
    public GameObject lightningAttack;
    public GameObject backGround;
    public GameObject electric;
    int attackCount = 0;
    //attack2
    public GameObject fireCircle;
    public GameObject fireBallFactory;
    public GameObject fbFirePoint;
    int fireballCnt = 0;
    //오브젝트 풀링
    //int poolSize = 10;
    //int fireIndex = 0;
    //public List<GameObject> fireballPool;
    bool isStart = true;    
    //attack3


    // Start is called before the first frame update
    void Start()
    {
        state = BossState.Idle;
        player = GameObject.Find("Player").transform;
        anim = GetComponentInChildren<Animator>();
        //InitObjectPooling();
    }

    //private void InitObjectPooling()
    //{
    //    fireballPool = new List<GameObject>();
    //    for(int i=0; i<poolSize;i++)
    //    {
    //        GameObject bullet = Instantiate(fireBallFactory);
    //        bullet.SetActive(false);
    //        fireballPool.Add(bullet);
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case BossState.Idle:
                Idle();
                break;
            case BossState.Attack1:
                timer += Time.deltaTime;
                if (timer > attTime)
                {
                    Attack1();
                    timer = 0f;
                }
                break;
            case BossState.Attack2:
                timer += Time.deltaTime;
                if (timer > attTime)
                {
                    Attack2();
                    timer = 0f;
                }
                break;
            case BossState.Attack3:
                Attack3();
                break;
            case BossState.Damaged:
                damaged();
                break;
            case BossState.Die:
                Die();
                break;

        }
    }

    private void Idle()
    {
        transform.LookAt(player.position);
        int randNum = Random.Range(1,4);
        timer += Time.deltaTime;
        Debug.Log("Timer : " + timer);
        if(timer > attTime)
        {
            if (randNum == 1)
            {
                state = BossState.Attack1;
                anim.SetBool("isAttack1", true);
                timer = 0f;
            }
            else if (randNum == 2)
            {
                state = BossState.Attack2;
                anim.SetBool("isAttack2", true);
                timer = 0f;
            }
            else
            {
                state = BossState.Attack3;
                anim.SetBool("isAttack3", true);
                timer = 0f;
            }
        }
    }

    //번개공격->플레이어 위치로 번개떨구기
    private void Attack1()
    {
        transform.LookAt(player.position);
        backGround.SetActive(true);
        electric.SetActive(true);
        StartCoroutine(Thunder());
    }

    IEnumerator Thunder()
    {
        
        attackCount++;
        GameObject danger = Instantiate(notice, player.position, Quaternion.Euler(transform.eulerAngles + new Vector3(-90, 0, 0)));
        yield return new WaitForSeconds(1.5f);
        Instantiate(lightningAttack, danger.transform.position, Quaternion.identity);
        Destroy(danger);
        Debug.Log("Attack Count : " + attackCount);
        if (attackCount > 3)
        {
            state = BossState.Idle;
            anim.SetBool("isAttack1", false);
            backGround.SetActive(false);
            electric.SetActive(false);
            //Debug.Log("Boss State : " + state);
        }
    }

    //플레이어위치로 파이어볼
    private void Attack2()
    {
        transform.LookAt(player.position);
        fireCircle.SetActive(true);

        StartCoroutine(FireBallFire());


    }

    IEnumerator FireBallFire()
    {
        isStart = false;
        fireballCnt++;
        GameObject bullet = Instantiate(fireBallFactory);
        bullet.transform.position = fbFirePoint.transform.position;
        yield return new WaitForSeconds(3f);
        isStart = true;

        if(fireballCnt > 3)
        {
            state = BossState.Idle;
            anim.SetBool("isAttack2", false);
            fireCircle.SetActive(false);
        }
    }

    //플레이어한테 점프하기
    private void Attack3()
    {
        state = BossState.Idle;
    }


    private void damaged()
    {
        
    }

    private void Die()
    {
        
    }
}
