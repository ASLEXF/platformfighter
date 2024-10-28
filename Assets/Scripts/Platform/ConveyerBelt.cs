using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class ConveyerBelt : MonoBehaviour
{
    float rotationSpeed;
    Transform center;

    private void Update()
    {
        rotationSpeed = GetComponent<Spinning>().rotationSpeed;
        center = transform.GetChild(0);
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Platform")) return;

        Vector3 direction = other.transform.position - center.position;
        direction.y = 0;
        float step = rotationSpeed * Time.fixedDeltaTime;
        Quaternion rotation = Quaternion.Euler(0, step, 0);
        direction = rotation * direction.normalized;

        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            //if (other.gameObject.CompareTag("Player"))
            //{
            //    //PlayerController playerController = other.transform.parent.GetComponentInChildren<PlayerController>();
            //    //if (playerController != null)
            //    //{
            //    //    Debug.Log($"{direction}");
            //    //    playerController.movePosition += direction;
            //    //}
            //}
            //else
            //    rb.velocity = direction;
            rb.gameObject.transform.RotateAround(center.position, Vector3.up, rotationSpeed * Time.deltaTime);
            //Vector3 relative = rb.position - center.position;
            //relative.z = 0;

            //Quaternion rotation;
            //if (rotationSpeed > 0) {
            //    rotation = Quaternion.Euler(0, Mathf.PI / 2, 0);
            //}
            //else
            //{
            //    rotation = Quaternion.Euler(0, Mathf.PI / 2 * -1, 0);
            //}

            //Vector3 targetVelocity = rotation * relative.normalized;
            //Vector3 velocityChange = targetVelocity - rb.velocity;
            //rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }
    }
}
