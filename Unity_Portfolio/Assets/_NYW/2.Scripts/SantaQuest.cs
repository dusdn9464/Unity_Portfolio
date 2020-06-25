using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaQuest : MonoBehaviour
{
    public GameObject bButton;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            bButton.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bButton.SetActive(false);
        }
    }
}
