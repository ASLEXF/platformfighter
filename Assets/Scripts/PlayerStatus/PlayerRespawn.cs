using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    PlayerController controller;

    [SerializeField] GameObject SpawnPoint;

    private void Awake()
    {
        controller = transform.parent.GetComponent<PlayerController>();
    }

    public void Respawn()
    {
        controller.transform.position = SpawnPoint.transform.position;
        controller.transform.rotation = SpawnPoint.transform.rotation;
        controller.transform.localScale = SpawnPoint.transform.localScale;
    }
}
