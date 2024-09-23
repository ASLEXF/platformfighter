using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    Animator animator;

    public float speed = 5f;

    private Rigidbody rb;
    [SerializeField] private GameObject playerObj;
    [SerializeField] private GameObject connetPoint;

    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private Rigidbody connectRb;
    [SerializeField] private SpringJoint spring_1;
    [SerializeField] private SpringJoint spring_2;

    public float RotationSmoothTime = 0.5f;
    private float _rotationVelocity;
    private float rotateY;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = playerObj.GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        playerObj.transform.position = transform.position;
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        if (move == Vector3.zero)
        {
            spring_1.connectedBody = null;
            spring_2.connectedBody = null;
            playerRb.velocity = Vector3.zero;

            transform.position = playerObj.transform.position;
            connetPoint.transform.position = transform.position;
        }
        else
        {
            spring_1.connectedBody = connectRb;
            spring_2.connectedBody = playerRb;
            rb.MovePosition(rb.position + move * speed * Time.deltaTime);

            rotateY = playerObj.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(rotateY, Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg, ref _rotationVelocity, RotationSmoothTime);
            Debug.Log($"{rotation} {Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg}");
            playerObj.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }
    }

    #region Input System

    PlayerInput playerInput;
    InputAction shield;

    Vector2 _rawInputMovement;

    void initialInputAction()
    {
        shield = playerInput.actions.FindActionMap("Keyboard").FindAction("Shield");
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        _rawInputMovement = value.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void OnShield(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            animator.SetTrigger("Attack");
        }
    }

    #endregion
}
