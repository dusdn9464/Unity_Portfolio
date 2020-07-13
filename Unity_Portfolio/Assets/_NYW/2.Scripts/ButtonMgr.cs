using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMgr : MonoBehaviour
{

    public void OnStartButtonClick()
    {
        SceneMgr.Instance.LoadScene("GameScene");
    }
}
