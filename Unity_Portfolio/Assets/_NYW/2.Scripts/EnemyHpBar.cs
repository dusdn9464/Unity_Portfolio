using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    public Slider hpBar;
    public GameObject enemy;


    Camera mainCam = null;

    public float enemyMaxHp = 500f;
    public float currentHp = 500f;

    //피격이펙트
    public GameObject fxFactory;

    public static EnemyHpBar instance;

    private void Awake()
    {
        EnemyHpBar.instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;

        //Slider t_hpbar = Instantiate(hpBar, enemy.transform.position, Quaternion.identity, transform);
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = Mathf.Lerp(hpBar.value, currentHp / enemyMaxHp, 5f * Time.deltaTime);
        hpBar.transform.position = transform.position + new Vector3(0, 0.8f, 0);
        //hpBar.transform.position = mainCam.WorldToScreenPoint(enemy.transform.position + new Vector3(0, 1.15f, 0));
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyFSM.instance.state = EnemyFSM.EnemyState.Damaged;

        //Debug.Log("총알 맞음");

        if (collision.gameObject.name.Contains("Bullet"))
        {
            if (currentHp > 0)
            {
                currentHp -= 100f;
                collision.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("EnemyDie");
                AddQuestNum.instance.MonsterCount();
                EnemyFSM.instance.state = EnemyFSM.EnemyState.Die;
                Destroy(gameObject);
                Debug.Log("Current State : " + EnemyFSM.instance.state);
            }
        }

        //이펙트 보여주기
        GameObject fx = Instantiate(fxFactory);
        fx.transform.position = transform.position;
        Destroy(fx, 1.0f);
    }
}
