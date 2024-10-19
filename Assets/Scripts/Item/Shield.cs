using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Shield : MonoBehaviour, IPlayer
{
    BoxCollider boxCollider;
    CapsuleCollider playerCollider;
    PlayerAttacked playerAttacked;

    [SerializeField] int health = 3;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        playerCollider = GetComponentInParent<CapsuleCollider>();
        playerAttacked = GetComponentInParent<PlayerAttacked>();
    }

    private void Start()
    {
        Physics.IgnoreCollision(boxCollider, playerCollider);
        gameObject.tag = "Shield";
    }

    public void Damage(int damage = 1)
    {
        if (health > 0)
        {
            health -= damage;
        }
        else
        {
            broke();
        }
    }

    private void broke()
    {
        health = 3;
        gameObject.SetActive(false);
        playerAttacked.ShieldBroken();
    }

    public PlayerAttacked GetPlayerAttacked()
    {
        return playerAttacked;
    }
}
