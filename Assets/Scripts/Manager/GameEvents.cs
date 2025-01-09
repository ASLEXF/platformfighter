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

    //#region

    //public event Action OnRebindSucceed;
    //public void RebindSucceed() => OnRebindSucceed?.Invoke();

    //#endregion

    #region Modeled Window

    public event Action OnApplyGraphicSettings;

    public void ApplyGraphicSettings() => OnApplyGraphicSettings?.Invoke();

    #endregion

    #region Game Control

    public event Action OnLevelExit;

    public void LevelExit() => OnLevelExit?.Invoke();

    #endregion

    /*
     * In Game Level
     */

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
        Debug.Log($"Player {id} die!");
    }

    #endregion

    #region UI

    public event Action OnQuickSettingsReturned;

    public void QuickSettingsReturned() => OnQuickSettingsReturned?.Invoke();

    #endregion
}
