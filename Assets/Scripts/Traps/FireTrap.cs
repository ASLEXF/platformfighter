using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] bool isActive = false;
    [SerializeField] int damage = 2;

    [SerializeField] List<Collider> colliders = new List<Collider>();
    [SerializeField] float force = 20.0f;


    private void FixedUpdate()
    {
        if (!isActive) return;
        // squeeze out
        for (int i = 0; i < colliders.Count; ++i)
        {
            Rigidbody rb = colliders[i].gameObject.GetComponent<Rigidbody>();
            Vector3 direction = colliders[i].transform.position - transform.position;
            float distance = direction.magnitude;
            rb.AddExplosionForce(force, transform.position, 2, 0.5f, ForceMode.Impulse);
            colliders.RemoveAt(i);
            i--;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isActive || colliders.Contains(other)) return;

        if (other.gameObject.CompareTag("Player"))
        {
            // deal damage
            other.transform.parent.GetComponentInChildren<PlayerHealth>().TakeDamage(damage);
            //// squeeze out
            //Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            //Vector3 direction = other.transform.position - transform.position;
            //float distance = direction.magnitude;
            //rb.AddExplosionForce(force, transform.position, 2, 0.2f, ForceMode.Impulse);

            colliders.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        colliders.Remove(other);
    }
}
