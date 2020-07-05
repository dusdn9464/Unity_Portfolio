using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButton : MonoBehaviour
{
    public static PlayerButton instance;

    public bool isAttack = false;
    public bool isJump = false;

    private void Awake()
    {
        PlayerButton.instance = this;
    }
    
    public void OnAttackButtonClick()
    {
        isAttack = true;
    }

    public void OnJumpButtonClick()
    {
        isJump = true;
    }
}
