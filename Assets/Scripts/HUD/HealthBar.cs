using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    PlayerHealth playerHealth;
    List<SpriteRenderer> healthBarsSprite = new List<SpriteRenderer>();

    private void Awake()
    {
        playerHealth = transform.parent.parent.Find("Status").GetComponent<PlayerHealth>();
    }

    public void Initialize()
    {
        for (int i = 0; i < playerHealth.maxHealth; i++)
            healthBarsSprite.Add(transform.GetChild(i).GetComponent<SpriteRenderer>());

        if (playerHealth.maxHealth == 3)
            transform.GetChild(3).gameObject.SetActive(false);
        else if (playerHealth.maxHealth == 4)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).transform.localPosition = new Vector3(-1.2f, 0, -3.18f) + new Vector3(0.8f, 0, 0) * i;
            }
        }
    }

    private void Update()
    {
        // TODO: event
        for(int i = 0; i < playerHealth.currentHealth; i++)
        {
            healthBarsSprite[i].color = new Color(255, 0, 0);
        }
        for (int i = playerHealth.currentHealth; i < playerHealth.maxHealth; i++)
        {
            healthBarsSprite[i].color = new Color(225, 225, 225);
        }
    }
}
