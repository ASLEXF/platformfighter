using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    PlayerAttack playerAttack;
    PlayerAttacked playerAttacked;
    PlayerInteract playerInteract;

    [SerializeField] float walkSpeed = 0.9f;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float deceleration = 0.6f;
    [SerializeField] float acceleration = 4f;
    [SerializeField] float currentSpeed;

    [SerializeField] private GameObject playerObj;
    //[SerializeField] private GameObject connetPoint;

    [SerializeField] private Rigidbody playerRb;
    //[SerializeField] private Rigidbody connectRb;
    //[SerializeField] private SpringJoint spring_1;
    //[SerializeField] private SpringJoint spring_2;

    public float RotationSmoothTime = 0.5f;
    private float _rotationVelocity;
    private float rotateY;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = playerObj.GetComponent<Animator>();
        playerAttack = playerObj.GetComponent<PlayerAttack>();
        playerAttacked = playerObj.GetComponent<PlayerAttacked>();
        playerInput = GetComponent<PlayerInput>();
        playerInteract = playerObj.transform.Find("Interact").GetComponent<PlayerInteract>();
    }

    private void Start()
    {
        playerObj.transform.position = transform.position;
    }

    private void Update()
    {
        Vector3 move = transform.right * _rawInputMovement.x + transform.forward * _rawInputMovement.y;

        if (move == Vector3.zero)
        {
            //spring_1.connectedBody = null;
            //spring_2.connectedBody = null;

            playerRb.velocity = Vector3.zero;

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

            playerRb.MovePosition(playerRb.position + move * currentSpeed * Time.deltaTime);

            rotateY = playerObj.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(rotateY, Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg, ref _rotationVelocity, RotationSmoothTime);
            playerObj.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        animator.SetFloat("Speed", currentSpeed);
    }

    #region Input System

    PlayerInput playerInput;
    InputAction shield;

    Vector2 _rawInputMovement;

    bool isRunning = false;

    void initialInputAction()
    {
        shield = playerInput.actions.FindActionMap("Keyboard").FindAction("Shield");
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
}
