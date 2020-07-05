using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActive : MonoBehaviour
{
    public GameObject boss;
    public GameObject pupleFire;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            boss.SetActive(true);
            pupleFire.SetActive(true);
        }
    }
}
