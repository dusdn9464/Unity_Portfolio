using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddQuestNum : MonoBehaviour
{
    public static AddQuestNum instance;

    //메인퀘스트(산타)
    int num = 0;
    public Text numText;
    //서브퀘스트(게시판)
    int monsterCount = 0;
    public Text countText;

    private void Awake()
    {
        AddQuestNum.instance = this;
    }

    public void Count()
    {
        num = 1;
        numText.text = "펌킨맨 " + num.ToString("") + "/1";
    }

    public void MonsterCount()
    {
        monsterCount++;
        countText.text = "루돌프 " + monsterCount.ToString("") + "/5";
    }
}
