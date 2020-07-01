using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    public float playerMaxHp = 1000f;
    float currentHp = 1000f;

    public Slider hpBar;

    // Update is caslled once per frame
    void Update()
    {
        hpBar.value = Mathf.Lerp(hpBar.value, currentHp / playerMaxHp, 5f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Attack"))
        {
            Debug.Log("Name : " + other.gameObject.name);
            currentHp -= 100f;
        }
    }
}
