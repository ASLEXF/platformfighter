using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    PlayerAttack playerAttack;
    PlayerAttacked playerAttacked;
    PlayerInteract playerInteract;
    PlayerStatusEffect playerStatusEffect;

    public int id;
    public Vector3 playerVelocity;

    [SerializeField] float walkSpeed = 0.9f;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float deceleration = 0.6f;
    [SerializeField] float acceleration = 4f;
    [SerializeField] float currentSpeed;
    Vector3 move = Vector3.zero;

    [SerializeField] GameObject playerObj;
    [SerializeField] Rigidbody playerRb;

    [SerializeField] bool Grounded = false;
    [SerializeField] LayerMask GroundLayers;
    [SerializeField] float GroundedOffset = -0.14f;
    [SerializeField] float GroundedRadius = 0.28f;

    [SerializeField] float rotationSmoothTime = 0.5f;
    private float _rotationVelocity;
    private float _rotateY;

    private void Awake()
    {
        playerObj = transform.parent.GetChild(0).gameObject;
        playerRb = playerObj.GetComponent<Rigidbody>();
        animator = playerObj.GetComponent<Animator>();
        playerAttack = playerObj.GetComponent<PlayerAttack>();
        playerAttacked = playerObj.GetComponent<PlayerAttacked>();
        playerInput = GetComponent<PlayerInput>();
        playerInteract = playerObj.transform.Find("Interact").GetComponent<PlayerInteract>();
        playerStatusEffect = transform.parent.Find("Status").GetComponent<PlayerStatusEffect>();
    }

    private void Start()
    {
        playerVelocity = new Vector3();

        initialInputAction();
    }

    private void Update()
    {
        GroundedCheck();

        if (Input.GetKeyDown(KeyCode.Space))
            playerStatusEffect.Frozen = true;
    }

    private void FixedUpdate()
    {
        if (playerStatusEffect.Frozen || playerStatusEffect.Stunned)
        {
            playerRb.velocity = Vector3.zero;
            return;
        }
        move = (transform.right * _rawInputMovement.x + transform.forward * _rawInputMovement.y).normalized;

        if (move == Vector3.zero)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            if (!isRunning)
            {
                if (currentSpeed > walkSpeed)
                    currentSpeed = Mathf.MoveTowards(currentSpeed, walkSpeed, acceleration * Time.fixedDeltaTime);
                else
                    currentSpeed = Mathf.MoveTowards(currentSpeed, walkSpeed, acceleration * Time.fixedDeltaTime);
            }
            else
            {
                if (currentSpeed < walkSpeed)
                    currentSpeed = walkSpeed;
                currentSpeed = Mathf.MoveTowards(currentSpeed, runSpeed, acceleration * Time.fixedDeltaTime);
            }

            // turn the player
            _rotateY = playerObj.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(_rotateY, Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg, ref _rotationVelocity, rotationSmoothTime);
            playerObj.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        if (Grounded)
            playerRb.velocity = playerVelocity + move * currentSpeed;
            //playerRb.AddForce(force);
            //playerRb.MovePosition(movePosition);

        animator.SetFloat("Speed", currentSpeed);
    }

    private void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y - GroundedOffset,
            playerObj.transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
            QueryTriggerInteraction.Ignore);
    }

    #region Input System

    PlayerInput playerInput;
    //InputAction move, run, attack, block, interact;

    Vector2 _rawInputMovement;

    bool isRunning = false;

    void initialInputAction()
    {
        foreach (var action in playerInput.actions.actionMaps[0].actions)  // Keyboard
        {
            action.started += handleAction;
            action.performed += handleAction;
            action.canceled += handleAction;
        }
    }

    void handleAction(InputAction.CallbackContext context)
    {
        if (playerStatusEffect.Frozen || playerStatusEffect.Stunned) return;

        switch (context.action.name)
        {
            case "Move":
                _rawInputMovement = context.ReadValue<Vector2>();
                animator.SetBool("IsWalking", context.performed);
                break;
            case "Run":
                isRunning = context.performed;
                break;
            case "Attack":
                playerAttack.Attack(context);
                break;
            case "Block":
                playerAttack.Block(context);
                break;
            case "Throw":
                playerAttack.ThrowWeapon(context);
                break;
            case "Interact":
                playerInteract.Interact(context);
                break;
        }
    }

    //public void OnMove(InputAction.CallbackContext context)
    //{
    //    if (playerStatusEffect.Frozen || playerStatusEffect.Stunned) return;

    //    _rawInputMovement = context.ReadValue<Vector2>();
    //    animator.SetBool("IsWalking", context.performed);
    //}

    //public void OnRun(InputAction.CallbackContext context)
    //{
    //    isRunning = context.performed;
    //}

    //public void OnAttack(InputAction.CallbackContext context)
    //{
    //    if (playerStatusEffect.Frozen || playerStatusEffect.Stunned) return;

    //    playerAttack.Attack(context);
    //}

    //public void OnThrow(InputAction.CallbackContext context)
    //{
    //    if (playerStatusEffect.Frozen || playerStatusEffect.Stunned) return;

    //    playerAttack.ThrowWeapon(context);

    //}

    //public void OnShield(InputAction.CallbackContext context)
    //{
    //    if (playerStatusEffect.Frozen || playerStatusEffect.Stunned) return;

    //    playerAttack.Block(context);
    //}

    //public void OnInteract(InputAction.CallbackContext context)
    //{
    //    if (playerStatusEffect.Frozen || playerStatusEffect.Stunned) return;

    //    playerInteract.Interact(context);
    //}

    #endregion

    #region Die & Respawn

    public void Respawn()
    {
        playerInput.enabled = true;
        playerRb.velocity = Vector3.zero;
        playerRb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    public void Die()
    {
        playerInput.enabled = false;
        playerRb.constraints = RigidbodyConstraints.None;
    }

    #endregion
}
