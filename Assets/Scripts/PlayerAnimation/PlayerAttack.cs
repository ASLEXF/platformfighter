using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator animator;
    GameObject playerObj;

    [SerializeField] HandLSlot handlSlot;
    [SerializeField] HandRSlot handRSlot;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerObj = transform.parent.GetChild(0).gameObject;
    }

    public void ThrowWeapon()
    {
        GameObject weaponObj = handRSlot.GetCurrentWeaponObj();
        if (weaponObj != null)
        {
            animator.SetTrigger("Throw");
        }
    }
}
