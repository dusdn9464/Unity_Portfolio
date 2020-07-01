using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyState : MonoBehaviour
{
    public float enemyMaxHp = 500f;
    float currentHp = 500f;

    public Slider hpBar;

    //피격이펙트
    public GameObject fxFactory;

    private void Update()
    {
        hpBar.value = Mathf.Lerp(hpBar.value, currentHp / enemyMaxHp, 5f * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyFSM.instance.state = EnemyFSM.EnemyState.Damaged;

        Debug.Log("총알 맞음");

        if (collision.gameObject.name.Contains("Bullet"))
        {
            if (currentHp > 0)
            {
                currentHp -= 100f;
                collision.gameObject.SetActive(false);
            }
            else
            {
                EnemyFSM.instance.state = EnemyFSM.EnemyState.Die;
            }
        }

        //이펙트 보여주기
        GameObject fx = Instantiate(fxFactory);
        fx.transform.position = transform.position;
        Destroy(fx, 1.0f);
    }
}
