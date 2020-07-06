using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardQuest : MonoBehaviour
{
    public GameObject interButton;
    public GameObject questPanel;

    bool isButton = false;

    // Update is called once per frame
    void Update()
    {
        if(isButton)
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                questPanel.SetActive(true);
                questPanel.transform.parent.gameObject.SetActive(true);
                Debug.Log("보드퀘스트 활성화 : " + questPanel.activeSelf);
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
        if(other.gameObject.CompareTag("Player"))
        {
            interButton.SetActive(false);
            questPanel.SetActive(false);
            isButton = false;
        }
    }
}
