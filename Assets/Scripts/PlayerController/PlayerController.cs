using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    PlayerAttack playerAttack;
    PlayerAttacked playerAttacked;
    PlayerInteract playerInteract;

    public Vector3 movePosition;
    public Vector3 playerVelocity;

    [SerializeField] float walkSpeed = 0.9f;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float deceleration = 0.6f;
    [SerializeField] float acceleration = 4f;
    [SerializeField] float currentSpeed;

    [SerializeField] GameObject playerObj;
    //[SerializeField] GameObject connetPoint;

    [SerializeField] Rigidbody playerRb;
    //[SerializeField] Rigidbody connectRb;
    //[SerializeField] SpringJoint spring_1;
    //[SerializeField] SpringJoint spring_2;

    [SerializeField] bool Grounded = false;
    [SerializeField] LayerMask GroundLayers;
    [SerializeField] float GroundedOffset = -0.14f;
    [SerializeField] float GroundedRadius = 0.28f;

    private float rotationSmoothTime = 0.5f;
    private float _rotationVelocity;
    private float _rotateY;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerObj = transform.parent.GetChild(0).gameObject;
        playerRb = playerObj.GetComponent<Rigidbody>();
        animator = playerObj.GetComponent<Animator>();
        playerAttack = playerObj.GetComponent<PlayerAttack>();
        playerAttacked = playerObj.GetComponent<PlayerAttacked>();
        playerInput = GetComponent<PlayerInput>();
        playerInteract = playerObj.transform.Find("Interact").GetComponent<PlayerInteract>();
    }

    private void Start()
    {
        movePosition = playerObj.transform.position;
        playerVelocity = new Vector3();
    }

    private void Update()
    {
        GroundedCheck();
    }

    private void FixedUpdate()
    {
        Vector3 move = (transform.right * _rawInputMovement.x + transform.forward * _rawInputMovement.y).normalized;

        if (move == Vector3.zero)
        {
            //spring_1.connectedBody = null;
            //spring_2.connectedBody = null;

            //playerRb.velocity = Vector3.zero;

            //transform.position = playerObj.transform.position;
            //connetPoint.transform.position = transform.position;

            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            //spring_1.connectedBody = connectRb;
            //spring_2.connectedBody = null;

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

            movePosition += move * currentSpeed * Time.deltaTime;

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
        if (Grounded)
        {
            movePosition = playerObj.transform.position;
        }
    }

    #region Input System

    PlayerInput playerInput;
    //InputAction shield;

    Vector2 _rawInputMovement;

    bool isRunning = false;

    void initialInputAction()
    {
        //shield = playerInput.actions.FindActionMap("Keyboard").FindAction("Shield");
        //shield.canceled += OnShieldCanceled;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _rawInputMovement = context.ReadValue<Vector2>();
        animator.SetBool("IsWalking", context.performed);
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        isRunning = context.performed;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerAttack.Attack();
        }
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerAttack.ThrowWeapon();
        }
    }

    public void OnShield(InputAction.CallbackContext context)
    {
        playerAttack.Block(context);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerInteract.Interact();
        }
    }

    //private void OnShieldCanceled(InputAction.CallbackContext context)
    //{
    //    animator.SetBool("IsBlocking", false);
    //}

    #endregion

    #region Die & Respawn

    public void Respawn()
    {
        playerInput.enabled = true;
        rb.constraints = RigidbodyConstraints.None;
    }

    public void Die()
    {
        playerInput.enabled = false;
        rb.constraints = RigidbodyConstraints.FreezePositionY;
    }

    #endregion
}
