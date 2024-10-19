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
}
