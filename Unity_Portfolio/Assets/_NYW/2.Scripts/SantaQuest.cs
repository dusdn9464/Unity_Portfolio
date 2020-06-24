using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaQuest : MonoBehaviour
{
    public GameObject santaQuest;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            santaQuest.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            santaQuest.SetActive(false);
        }
    }
}
