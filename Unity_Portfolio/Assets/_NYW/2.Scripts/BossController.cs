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
    public GameObject notice;
    public GameObject lightningAttack;


    // Start is called before the first frame update
    void Start()
    {
        state = BossState.Idle;
        player = GameObject.Find("Player").transform;
        anim = GetComponentInChildren<Animator>();
    }

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
                Attack2();
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
                Attack2();
                timer = 0f;
            }
            else
            {
                Attack3();
                timer = 0f;
            }
        }
    }

    //번개공격->플레이어 위치로 번개떨구기
    private void Attack1()
    {
        transform.LookAt(player.position);
        StartCoroutine(Thunder());
    }

    IEnumerator Thunder()
    {
        int attackCount = 0;
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
            Debug.Log("Boss State : " + state);
        }
    }

    //플레이어위치로 점프하기
    private void Attack2()
    {

    }

    //플레이어한테 파이어볼
    private void Attack3()
    {

    }


    private void damaged()
    {
        
    }

    private void Die()
    {
        
    }
}
