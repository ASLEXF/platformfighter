using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    Animator animator;
    GameObject playerObj;
    PlayerAttacked playerAttacked;
    HandLSlot handLSlot;
    HandRSlot handRSlot;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerObj = transform.parent.GetChild(0).gameObject;
        playerAttacked = GetComponent<PlayerAttacked>();
        handLSlot = transform.parent.GetComponent<QuickRefer>().handLSlot;
        handRSlot = transform.parent.GetComponent<QuickRefer>().handRSlot;
    }

    public void Attack()
    {
        if (handRSlot.GetCurrentWeaponObj() == null) return;

        animator.SetTrigger("Attack");

        animator.SetBool("IsBlocking", false);
        animator.ResetTrigger("BlockAttacked");
        playerAttacked.isBlocking = false;
    }

    public void ThrowWeapon()
    {
        if (handRSlot.GetCurrentWeaponObj() == null) return;

        animator.SetTrigger("Throw");
    }

    public bool isRightKeyDown;  // to resumeBlock() after hit or attack

    public void Block(InputAction.CallbackContext context)
    {
        if (handLSlot.GetCurrentItemObj() == null) return;

        if (context.started)
        {
            animator.SetTrigger("Block");
            isRightKeyDown = true;
        }
        else if (context.canceled)
        {
            isRightKeyDown = false;
        }
        animator.SetBool("IsBlocking", context.performed);
        playerAttacked.isBlocking = context.performed;
    }
}
