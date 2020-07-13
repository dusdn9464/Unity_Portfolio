using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    //싱글톤만들기
    public static EnemyFSM instance;



    //몬스터 상태 이넘문
    public enum EnemyState
    {
        Idle, Move, Attack, Return, Damaged, Die
    }

    public float findRange = 0f; //플레이어를 찾는 범위
    public float moveRange = 0f; //시작지점에서 최대 이동가능한 범위
    public float attackRange = 2f; //공격 가능 범위

    public EnemyState state;

    Vector3 startPoint; //몬스터 시작위치
    //Quaternion startRotation; //몬스터 시적회전값
    Transform player;   //플레이어를 찾기위해(안그럼 모든 몬스터에 다 드래그앤드랍 해줘야 한다 걍 코드로 찾아서 처리하기)
    CharacterController cc; //몬스터 이동을 위해 캐릭터컨트롤러 컴포넌트

    //애니메이션을 제어하기 위한 애니메이터 컴포넌트
    Animator anim;
    NavMeshAgent agent;


    ///몬스터 일반변수
    int hp = 100; //체력
    int att = 5; //공격력
    float speed = 5.0f; //이동속도


    //공격 딜레이
    float attTime = 2f; //2초에 한번 공격
    float timer = 0f; //타이머

    public void Awake()
    {
        EnemyFSM.instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //몬스터 상태 초기화
        state = EnemyState.Idle;
        //시작지점 저장
        startPoint = transform.position;
        //startRotation = transform.rotation;
        //플레이어 트렌스폼 컴포넌트
        player = GameObject.Find("Player").transform;
        //캐릭터 컨트롤러 컴포넌트
        cc = GetComponent<CharacterController>();
        //애니메이터 컴포넌트
        anim = GetComponentInChildren<Animator>();

        //네비메시에이전트 가져오기
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
    }

    void Update()
    {
        Debug.Log("EnemyFSM Current State : " + state);

        if (EnemyActive.instance.isTouch)
        {
            findRange = 15f;
            moveRange = 30f;

        }
        else
        {
            findRange = 0f;
            moveRange = 0f;

            //state = EnemyState.Return;
            //print("상태전환 : Move -> return");
        }
        //상태에 따른 행동처리
        switch (state)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                Damaged();
                break;
            case EnemyState.Die:
                Die();
                break;
        }
    }//end of void Update()

    //대기상태
    private void Idle()
    {
        if (Vector3.Distance(transform.position, player.position) < findRange)
        {
            state = EnemyState.Move;
            print("상태전환 : Idle -> Move");

            //애니메이션
            anim.SetTrigger("Move");
        }

    }

    //이동상태
    private void Move()
    {
        //시작할때 꺼주고 무브상태가 아닐때 꺼줘야 한다
        if (!agent.enabled) agent.enabled = true;

        //이동중 이동할 수 있는 최대범위에 들어왔을때
        if (Vector3.Distance(transform.position, startPoint) > moveRange)
        {
            state = EnemyState.Return;
            print("상태전환 : Move -> Return");
            anim.SetTrigger("Return");
        }
        //리턴상태가 아니면 플레이어를 추격해야 한다
        else if (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            //플레이어를 향해서 이동해라
            //네비메시에이전트가 회전처리부터 이동까지 전부다 처리해준다
            agent.SetDestination(player.position);

        }
        else //공격범위 안에 들어옴
        {
            if (EnemyHpBar.instance.currentHp > 0)
            {
                state = EnemyState.Attack;
                print("상태전환 : Move -> Attack");
                anim.SetTrigger("Attack");
            }
        }
    }

    //공격상태
    private void Attack()
    {
        //에이전트 오프
        agent.enabled = false;

        //공격범위안에 들어옴
        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            //공격할때 거리로만 처리되다보니 엉뚱한곳을 공격할 수 있다
            transform.LookAt(player.position);

            //일정 시간마다 플레이어를 공격하기
            timer += Time.deltaTime;
            if (timer > attTime)
            {
                print("공격");
                //플레이어의 필요한 스크립트 컴포넌트를 가져와서 데미지를 주면 된다
                //player.GetComponent<PlayerMove>().hitDamage(att);

                //타이머 초기화
                timer = 0f;

                anim.SetTrigger("Attack");
            }
        }
        else//현재상태를 무브로 전환하기 (재추격)
        {
            state = EnemyState.Move;
            print("상태전환 : Attack -> Move");
            //타이머 초기화
            timer = 0f;
            anim.SetTrigger("Move");
        }
    }

    //복귀상태
    private void Return()
    {
        //시작위치까지 도달하지 않을때는 이동
        //도착하면 대기상태로 변경
        if (Vector3.Distance(transform.position, startPoint) > 0.1)
        {
            //복귀
            agent.SetDestination(startPoint);
        }
        else
        {
            //위치값을 초기값으로 
            transform.position = startPoint;
            transform.rotation = Quaternion.identity;//startRotation;
            //Quaternion.identity => 쿼터니온 회전값을 0으로 초기화시켜준다

            state = EnemyState.Idle;
            print("상태전환 : Return -> Idle");
            anim.SetTrigger("Idle");
            //에이전트 오프
            agent.enabled = false;
        }
    }
    //플레이어쪽에서 충돌감지를 할 수 있으니 이함수는 퍼블릭으로 만들자
    public void hitDamage(int value)
    {
        //예외처리
        //피격상태이거나, 죽은 상태일때는 데미지 중첩으로 주지 않는다
        if (state == EnemyState.Damaged || state == EnemyState.Die) return;

        //체력깍기
        hp -= value;

        //몬스터의 체력이 1이상이면 피격상태
        if (hp > 0)
        {
            state = EnemyState.Damaged;
            print("상태전환 : AnyState -> Damaged");
            print("HP : " + hp);
            anim.SetTrigger("Damaged");
            Damaged();
        }
        //0이하이면 죽음상태
        else
        {
            state = EnemyState.Die;
            print("상태전환 : AnyState -> Die");
            anim.SetTrigger("Die");
            Die();
        }
    }

    //피격상태 (Any State)
    private void Damaged()
    { 
        //피격 상태를 처리하기 위한 코루틴을 실행한다
        StartCoroutine(DamageProc());
    }

    //피격상태 처리용 코루틴
    IEnumerator DamageProc()
    {
        //피격모션 시간만큼 기다리기
        yield return new WaitForSeconds(1.0f);
        //현재상태를 이동으로 전환
        state = EnemyState.Move;
        //print("상태전환 : Damaged -> Move");
        anim.SetTrigger("Move");
    }

    //죽음상태 (Any State)
    private void Die()
    {
        //코루틴을 사용하자
        //1. 체력이 0이하
        //2. 몬스터 오브젝트 삭제
        //- 상태변경
        //- 상태전환 출력 (죽었다)
        Debug.Log("State : Die");
        anim.SetBool("isDie", true);
        Destroy(gameObject);
        //진행중인 모든 코루틴은 정지한다
        //StopAllCoroutines();

        //죽음상태를 처리하기 위한 코루틴 실행
        //StartCoroutine(DieProc());
    }

    IEnumerator DieProc()
    {
        //캐릭터컨트롤러 비활성화
        //enabled = false;
        //에이전트 오프
        agent.enabled = false;

        //2초후에 자기자신을 제거한다
        yield return new WaitForSeconds(2.0f);
        //print("죽었다!!");
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        //공격가능범위
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        //플레이어 찾을 수 있는 범위
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, findRange);
        //이동가능한 최대 범위
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(startPoint, moveRange);
    }

}
