using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    //산타메인퀘스트
    public GameObject questPanel;
    public GameObject questNotice;
    //서브퀘스트
    public GameObject boardQuestPanel;
    public GameObject boardQuestNotice;

    public void OnYesButtonClick()
    {
        questNotice.SetActive(true);
        questPanel.SetActive(false);
    }

    public void OnNoButtonClick()
    {
        questPanel.SetActive(false);
    }

    public void OnBoardYesButtonClick()
    {
        boardQuestPanel.SetActive(false);
        boardQuestNotice.SetActive(true);
    }

    public void OnBoardNoButtonClick()
    {
        boardQuestPanel.SetActive(false);
    }
}
