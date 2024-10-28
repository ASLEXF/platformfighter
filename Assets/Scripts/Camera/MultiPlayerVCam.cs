using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlayerVCam : MonoBehaviour
{
    CinemachineVirtualCamera vcam;
    [SerializeField] Transform originalTransform;
    [SerializeField] List<Transform> targets;

    private void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        originalTransform = transform;
        //targets = GameManager
    }

    void Start()
    {
        transform.rotation = Quaternion.Euler(45, 0, 0);
    }

    void Update()
    {
        //float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime; // A & D or Left & Right
        //float moveZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;   // W & S or Up & Down

        //Vector3 moveDirection = new Vector3(moveX, moveZ, moveZ);

        //Vector3 newPosition = transform.position + moveDirection;

        //transform.position = newPosition;
    }

    private void LateUpdate()
    {
        //if (targets.Length == 0) return;

        //Vector3 averagePosition = Vector3.zero;
        //foreach (Transform target in targets)
        //{
        //    averagePosition += target.position;
        //}
        //averagePosition /= targets.Length;

        //vcam.transform.LookAt(averagePosition);
    }
}
