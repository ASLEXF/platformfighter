using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    PlayerAttacked playerAttacked;
    PlayerRespawn playerRespawn;

    [SerializeField] int maxHealth = 3;
    [SerializeField] int currentHealth;

    public bool isInvincible = false;

    private void Awake()
    {
        playerAttacked = transform.parent.GetChild(0).GetComponent<PlayerAttacked>();
        playerRespawn = GetComponent<PlayerRespawn>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage = 1)
    {
        int resultHealth = currentHealth - damage;
        if (resultHealth > 0)
        {
            currentHealth = resultHealth;
        }
        else
        {
            currentHealth = 0;
            playerAttacked.Die();
            StartCoroutine(respawn(3));
        }
    }

    private IEnumerator respawn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        playerRespawn.Respawn();
    }
}
