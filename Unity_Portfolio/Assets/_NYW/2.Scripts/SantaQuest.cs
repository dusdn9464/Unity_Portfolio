using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaQuest : MonoBehaviour
{
    public GameObject interButton;
    public GameObject questPanel;

    bool isButton = false;

    private void Update()
    {
        if(isButton)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                questPanel.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            interButton.SetActive(true);
            isButton = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interButton.SetActive(false);
            questPanel.SetActive(false);
            isButton = false;
        }
    }
}
