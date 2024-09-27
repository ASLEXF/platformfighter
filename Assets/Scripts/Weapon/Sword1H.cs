using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword1H : MonoBehaviour
{
    [SerializeField] int damage = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.GetComponent<PlayerHealth>().GetAttacked(damage);
        }
    }
}
