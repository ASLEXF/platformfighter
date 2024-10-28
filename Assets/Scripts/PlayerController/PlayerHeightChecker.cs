using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeightChecker : MonoBehaviour
{
    [SerializeField] float height = -10.0f;
    PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (transform.position.y < height)
        {
            playerHealth.TakeDamage(24);
        }
    }
}
