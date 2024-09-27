using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void startAttacking()
    {
        animator.SetBool("IsAttacking", true);
    }

    private void stopAttacking()
    {
        animator.SetBool("IsAttacking", false);
    }
}
