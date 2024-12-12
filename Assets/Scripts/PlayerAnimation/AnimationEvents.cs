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
        playerAttack = GetComponent<PlayerAttack>();
        playerAttacked = GetComponent<PlayerAttacked>();
    }

    public void Initialize()
    {
        handLSlot = transform.parent.GetComponent<QuickRefer>().handLSlot;
        handRSlot = transform.parent.GetComponent<QuickRefer>().handRSlot;
    }

    Coroutine attackingCoroutine;

    private void startAttacking()
    {
        animator.SetBool("IsAttacking", true);
        attackingCoroutine = StartCoroutine(attacking());  // handle dead when attacking
    }

    IEnumerator attacking()
    {
        yield return new WaitForSeconds(1.2f);
        animator.SetBool("IsAttacking", false);
    }

    private void stopAttacking()
    {
        StopCoroutine(attackingCoroutine);
        animator.SetBool("IsAttacking", false);
    }

    private void startThrowing() => animator.SetBool("IsThrowing", true);

    private void stopThrowing()
    {
        animator.SetBool("IsThrowing", false);
        handRSlot.TryGetLWeapon();
    }


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
        weaponObj.SetActive(false);

        string path = $"Prefabs/Items/{weaponObj.name}";
        GameObject prefabObj = Resources.Load<GameObject>(path);
        Vector3 position = gameObject.transform.position + gameObject.transform.forward * 1 + new Vector3(0, 1.5f, 0);
        Quaternion rotation = Quaternion.Euler(185, gameObject.transform.rotation.eulerAngles.y + 90, 90);

        GameObject projectile = Instantiate(prefabObj, position, rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = gameObject.transform.forward * 20.0f;

        DropItem dropItem = projectile.GetComponent<DropItem>();
        dropItem.name = weaponObj.name;
        dropItem.itemType = ItemType.barbarianWeapon;
        dropItem.isThrown = true;

        dropItem.collisionObjs.Add(gameObject);
        StartCoroutine(dropItem.SetThrown());
    }

    private void resumeBlock()
    {
        if (animator.GetBool("IsBlocking")) return;

        if (playerAttack.isRightKeyDown)
        {
            animator.SetTrigger("Block");
            animator.SetBool("IsBlocking", true);
            playerAttacked.isBlocking = true;
        }
    }
}
