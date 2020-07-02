using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    public Slider hpBar;

    public float bossMaxHp = 3000f;
    float currentHp = 3000f;

    //피격이펙트
    public GameObject fxFactory;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
