using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayerController : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    PlayerAttack playerAttack;

    [SerializeField] float walkSpeed = 0.9f;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float deceleration = 0.6f;
    [SerializeField] float acceleration = 4f;
    [SerializeField] float currentSpeed;

    [SerializeField] private GameObject playerObj;

    float time = Time.time;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = playerObj.GetComponent<Animator>();
        playerAttack = playerObj.GetComponent<PlayerAttack>();
    }

    private void Start()
    {
        playerObj.transform.position = transform.position;
        //initialInputAction();
    }

    private void Update()
    {
        if (Time.time - time > 3)
        {
            animator.SetTrigger("Attack");
            time = Time.time;
        }
    }
}
