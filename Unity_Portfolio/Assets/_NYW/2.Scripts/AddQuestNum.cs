using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddQuestNum : MonoBehaviour
{
    public static AddQuestNum instance;

    int num = 0;
    public Text numText;

    private void Awake()
    {
        AddQuestNum.instance = this;
    }
    // Update is called once per frame
    //void Update()
    //{
    //    
    //
    //   if (BossController.instance.state == BossController.BossState.Die)
    //   {
    //       
    //   }
    //}
    public void Count()
    {
        num = 1;
        numText.text = "펌킨맨 " + num.ToString("") + "/1";
    }
}
