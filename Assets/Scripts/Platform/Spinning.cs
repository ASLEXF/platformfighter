using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    public Transform point;
    public float rotationSpeed = 50f;
    void FixedUpdate()
    {
        transform.RotateAround(point.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
