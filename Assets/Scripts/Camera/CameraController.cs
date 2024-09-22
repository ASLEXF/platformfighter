using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 5f;

    void Start()
    {
        transform.rotation = Quaternion.Euler(45, 0, 0);
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime; // A & D or Left & Right
        float moveZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;   // W & S or Up & Down

        Vector3 moveDirection = new Vector3(moveX, moveZ, moveZ);

        Vector3 newPosition = transform.position + moveDirection;

        transform.position = newPosition;
    }
}
