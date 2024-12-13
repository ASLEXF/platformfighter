using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    PlayerController playerController;
    PlayerAttacked playerAttacked;
    PlayerRespawn playerRespawn;
    PlayerStatusEffect playerStatusEffect;

    [SerializeField] public int maxHealth;
    [SerializeField] public int currentHealth;

    public bool isInvincible = false;

    private void Awake()
    {
        playerController = transform.parent.Find("ControlPoint").GetComponent<PlayerController>();
        playerRespawn = GetComponent<PlayerRespawn>();
        playerStatusEffect = GetComponent<PlayerStatusEffect>();
    }

    public void Initialize()
    {
        playerAttacked = transform.parent.GetChild(0).GetComponent<PlayerAttacked>();

        if (transform.parent.GetChild(0).name.StartsWith("Knight"))
        {
            maxHealth = 3;
        }
        else if (transform.parent.GetChild(0).name.StartsWith("Barbarian"))
        {
            maxHealth = 4;
        }

        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage = 1)
    {
        if (playerStatusEffect.lifeStatus == LifeStatusEnum.Dead) return;

        int resultHealth = currentHealth - damage;
        if (resultHealth > 0)
        {
            currentHealth = resultHealth;
        }
        else
        {
            playerStatusEffect.lifeStatus = LifeStatusEnum.Dead;
            currentHealth = 0;
            if (playerController == null)
            {
                TestPlayerController controller = transform.parent.Find("Control Point").GetComponent<TestPlayerController>();
                controller.Die();
            }
            else
            {
                playerController.Die(damage);
            }
            playerAttacked.Die();
            StartCoroutine(respawn(3));
        }
    }

    public void Respawn()
    {
        playerStatusEffect.lifeStatus = LifeStatusEnum.Alive;
        currentHealth = maxHealth;
    }

    private IEnumerator respawn(float seconds)
    {
        GameEvents.Instance.PlayerDie(playerController.id);
        yield return new WaitForSeconds(seconds);
        playerRespawn.Respawn();
    }
}
