# nullable enable

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerAttack : MonoBehaviour
{
    Animator animator = null!;
    GameObject playerObj = null!;
    PlayerAttacked playerAttacked = null!;
    HandLSlot handLSlot = null!;
    HandRSlot handRSlot = null!;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerObj = transform.parent.GetChild(0).gameObject;
        playerAttacked = GetComponent<PlayerAttacked>();
        handLSlot = transform.parent.GetComponent<QuickRefer>().handLSlot;
        handRSlot = transform.parent.GetComponent<QuickRefer>().handRSlot;
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (handRSlot.GetCurrentWeaponObj() == null) return;

        if (context.started)
        {
            animator.SetTrigger("Attack");

            animator.SetBool("IsBlocking", false);
            animator.ResetTrigger("BlockAttacked");
            playerAttacked.isBlocking = false;
        }
    }

    public void ThrowWeapon(InputAction.CallbackContext context)
    {
        if (handRSlot.GetCurrentWeaponObj() == null) return;

        if (context.started)
        {
            animator.SetTrigger("Throw");
        }
    }

    public bool isRightKeyDown;  // to resumeBlock() after hit or attack

    public void Block(InputAction.CallbackContext context)
    {
        if (handLSlot.GetCurrentItemObj()?.CompareTag("Item") ?? true) return;

        if (context.started)
        {
            animator.ResetTrigger("Attack");
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
