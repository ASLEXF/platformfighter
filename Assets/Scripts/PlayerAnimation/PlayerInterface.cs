using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterface : MonoBehaviour, IPlayer
{
    public PlayerAttacked GetPlayerAttacked()
    {
        return gameObject.GetComponent<PlayerAttacked>();
    }
}
