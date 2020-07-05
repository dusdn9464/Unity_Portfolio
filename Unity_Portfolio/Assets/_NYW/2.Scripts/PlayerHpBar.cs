using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    public float playerMaxHp = 1000f;
    float currentHp = 1000f;

    public Slider hpBar;

    public bool isDie = false;
    public bool isDamaged = false;

    public GameObject gameOverImg;

    public static PlayerHpBar instance;

    private void Awake()
    {
        PlayerHpBar.instance = this;
    }

    // Update is caslled once per frame
    void Update()
    {
        hpBar.value = Mathf.Lerp(hpBar.value, currentHp / playerMaxHp, 5f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Attack")
            ||other.gameObject.CompareTag("Thunderbolt")
            ||other.gameObject.CompareTag("FireBall"))
        {
            if (currentHp > 0)
            {
                //Debug.Log("Name : " + other.gameObject.name);
                isDamaged = true;
                currentHp -= 100f;
                other.gameObject.SetActive(false);
            }
            else
            {
                isDie = true;
                gameOverImg.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isDamaged = false;
    }
}
