using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    PlayerController controller;
    PlayerHealth playerHealth;
    PlayerAttacked playerAttacked;

    [SerializeField] public Transform SpawnPoint;

    private void Awake()
    {
        controller = transform.parent.Find("ControlPoint").GetComponent<PlayerController>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void Initialize()
    {
        playerAttacked = transform.parent.GetChild(0).GetComponent<PlayerAttacked>();
    }

    public void Respawn()
    {
        transform.parent.GetChild(0).transform.position = SpawnPoint.position;
        transform.parent.GetChild(0).transform.rotation = SpawnPoint.rotation;
        controller.Respawn();
        playerHealth.Respawn();
        playerAttacked.Respawn();
    }
}
