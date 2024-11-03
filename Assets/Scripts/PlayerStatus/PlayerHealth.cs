using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    PlayerController playerController;
    PlayerAttacked playerAttacked;
    PlayerRespawn playerRespawn;
    PlayerStatusEffect playerStatusEffect;

    [SerializeField] int maxHealth = 3;
    [SerializeField] public int currentHealth;

    public bool isInvincible = false;

    private void Awake()
    {
        playerController = transform.parent.Find("ControlPoint").GetComponent<PlayerController>();
        playerAttacked = transform.parent.GetChild(0).GetComponent<PlayerAttacked>();
        playerRespawn = GetComponent<PlayerRespawn>();
        playerStatusEffect = GetComponent<PlayerStatusEffect>();
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
            playerStatusEffect.lifeStatus = LifeStatusEnum.Dead;
            currentHealth = 0;
            if (playerController == null)
            {
                TestPlayerController controller = transform.parent.Find("Control Point").GetComponent<TestPlayerController>();
                controller.Die();
            }
            else
            {
                playerController.Die();
            }
            playerAttacked.Die();
            StartCoroutine(respawn(3));
        }
    }

    public void Respawn()
    {
        playerStatusEffect.lifeStatus = LifeStatusEnum.Alive;
        currentHealth = maxHealth;
        playerController.Respawn();
        playerAttacked.Respawn();
    }

    private IEnumerator respawn(float seconds)
    {
        GameEvents.Instance.PlayerDie(playerController.id);
        yield return new WaitForSeconds(seconds);
        GameManager.Instance.PlayerRespawn(playerController.id);
    }
}
