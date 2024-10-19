using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayerController : MonoBehaviour
{
    Animator animator;
    PlayerAttack playerAttack;
    PlayerAttacked playerAttacked;

    [SerializeField] private GameObject playerObj;
    [SerializeField] bool attack = false;
    [SerializeField] bool block = false;

    float time;

    bool isBlock = true;

    private void Awake()
    {
        animator = playerObj.GetComponent<Animator>();
        playerAttack = playerObj.GetComponent<PlayerAttack>();
        playerAttacked = playerObj.GetComponent<PlayerAttacked>();
    }

    private void Start()
    {
        playerObj.transform.position = transform.position;
        //initialInputAction();
        time = Time.time;

        if (block)
        {
            playerAttacked.isBlocking = true;
            animator.SetTrigger("Block");
            animator.SetBool("IsBlocking", true);
        }
    }

    private void Update()
    {
        if (attack && Time.time - time > 3)
        {
            animator.SetTrigger("Attack");
            isBlock = false;
            time = Time.time;
        }

        if (block && isBlock == false)
        {
            animator.SetTrigger("Block");
            animator.SetBool("IsBlocking", true);
            isBlock = true;
        }
    }
}
