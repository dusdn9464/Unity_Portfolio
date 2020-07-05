using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    public Slider hpBar;

    public float bossMaxHp = 3000f;
    float currentHp = 3000f;


    //피격
    public GameObject fxFactory;

    private void Update()
    {
        hpBar.value = Mathf.Lerp(hpBar.value, currentHp / bossMaxHp, 5f * Time.deltaTime);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name.Contains("Bullet"))
        {
            if(currentHp > 0)
            {
                BossController.instance.state = BossController.BossState.Damaged;
                currentHp -= 100f;
                collision.gameObject.SetActive(false);
            }
            else
            {
                BossController.instance.state = BossController.BossState.Die;
            }

            //이펙트 보여주기
            GameObject fx = Instantiate(fxFactory);
            fx.transform.position = transform.position + new Vector3(0, 3, 0);
            Destroy(fx, 1.0f);
        }


    }

}
