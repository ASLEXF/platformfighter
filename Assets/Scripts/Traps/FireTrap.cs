using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] bool isActive = false;
    [SerializeField] int damage = 2;

    [SerializeField] List<Collision> collisions = new List<Collision>();

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!isActive) return;

    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        other.transform.parent.GetComponentInChildren<PlayerHealth>().TakeDamage(damage);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!isActive || collisions.Contains(collision)) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            // deal damage
            collision.transform.parent.GetComponentInChildren<PlayerHealth>().TakeDamage(damage);
            // squeeze out
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 direction = collision.transform.position - transform.position;
            float distance = direction.magnitude;
            rb.AddExplosionForce(10, transform.position, 2, 0, ForceMode.Impulse);


            collisions.Add(collision);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        collisions.Remove(collision);
    }
}
