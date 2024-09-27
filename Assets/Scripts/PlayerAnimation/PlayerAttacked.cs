using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacked : MonoBehaviour
{
    Animator animator;

    public void GetAttacked()
    {
        animator.SetTrigger("Attacked");
    }

    public void Die()
    {
        animator.SetBool("IsDead", true);
    }
}
