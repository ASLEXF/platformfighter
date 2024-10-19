using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    Animator animator;
    HandLSlot handLSlot;
    HandRSlot handRSlot;
    PlayerAttack playerAttack;
    PlayerAttacked playerAttacked;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        handLSlot = transform.parent.GetComponent<QuickRefer>().handLSlot;
        handRSlot = transform.parent.GetComponent<QuickRefer>().handRSlot;
        playerAttack = GetComponent<PlayerAttack>();
        playerAttacked = GetComponent<PlayerAttacked>();
    }

    private void startAttacking() => animator.SetBool("IsAttacking", true);

    private void stopAttacking() => animator.SetBool("IsAttacking", false);

    private void startThrowing() => animator.SetBool("IsThrowing", true);

    private void stopThrowing() => animator.SetBool("IsThrowing", false);


    private void startWeaponOneStrike()
    {
        IWeapon weapon = handRSlot.GetCurrentIWeapon();

        if (weapon != null)
        {
            weapon.StartOneStrike();
        }
    }

    private void endWeaponOneStrike()
    {
        IWeapon weapon = handRSlot.GetCurrentIWeapon();

        if (weapon != null)
        {
            weapon.EndOneStrike();
        }
    }

    private void throwWeapon()
    {
        GameObject weaponObj = handRSlot.GetCurrentWeaponObj();
        //weaponObj.SetActive(false);

        string path = $"Prefabs/{weaponObj.name}";
        GameObject prefanObj = Resources.Load<GameObject>(path);
        Vector3 position = gameObject.transform.position + gameObject.transform.forward * 1 + new Vector3(0, 1.5f, 0);
        Quaternion rotation = Quaternion.Euler(5, gameObject.transform.rotation.eulerAngles.y + 90, 90);

        GameObject projectile = Instantiate(prefanObj, position, rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = gameObject.transform.forward * 20.0f;

        DropItem dropItem = projectile.GetComponent<DropItem>();
        dropItem.name = weaponObj.name;
        dropItem.itemType = ItemType.knightWeapon;  // TODO
        dropItem.isThrown = true;

        dropItem.collisionObjs.Add(gameObject);
    }

    private void resumeBlock()
    {
        if (playerAttack.isRightKeyDown)
        {
            animator.SetTrigger("Block");
            animator.SetBool("IsBlocking", true);
            playerAttacked.isBlocking = true;
        }
    }
}
