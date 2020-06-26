using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActive : MonoBehaviour
{
    public bool isTouch = false;

    public static EnemyActive instance;

    public void Awake()
    {
        EnemyActive.instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        isTouch = true;
    }

    public void OnTriggerExit(Collider other)
    {
        isTouch = false;
    }
}
