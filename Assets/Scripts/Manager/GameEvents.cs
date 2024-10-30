using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    private static GameEvents instance;

    public static GameEvents Instance
    {  get { return instance; } }

    private void Awake()
    {
        instance = this;
    }

    #region Item

    public event Action OnItemDropped;

    public void ItemDropped() => OnItemDropped?.Invoke();

    #endregion

    #region Player

    public event Action OnPlayer1Die;
    public event Action OnPlayer2Die;

    public void PlayerDie(int id)
    {
        switch (id)
        {
            case 1: OnPlayer1Die?.Invoke(); break;
            case 2: OnPlayer2Die?.Invoke(); break;
        }
    }

    #endregion
}
