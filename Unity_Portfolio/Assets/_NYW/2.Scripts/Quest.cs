using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public GameObject questPanel;
    public GameObject questNotice;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnYesButtonClick()
    {
        questPanel.SetActive(false);
    }

    public void OnNoButtonClick()
    {
        questPanel.SetActive(false);
    }
}
