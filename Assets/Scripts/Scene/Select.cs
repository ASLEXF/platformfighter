using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectScene : MonoBehaviour
{
    [SerializeField] PlayerButton Player1Knight, Player1Barbarian, Player2Knight, Player2Barbarian;
    [SerializeField] LevelButton Level1, Level2;
    [SerializeField] Button Start;

    [Space(10)]
    [SerializeField] string player1 = null;
    [SerializeField] string player2 = null;
    [SerializeField] string levelName = null;

    private static SelectScene instance;

    public static SelectScene Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance is null)
            instance = this;
    }

    private void CheckButtons()
    {
        if (player1 is null || player2 is null || levelName is null)
        {
            return;
        }

        Start.interactable = true;
    }

    public void OnPlayer1KnightSelected()
    {
        player1 = "Prefabs/Knight";

        Player1Knight.SetSelected();
        Player1Barbarian.SetUnselected();
        CheckButtons();
    }
    public void OnPlayer1BarbarianSelected()
    {
        player1 = "Prefabs/Barbarian";

        Player1Knight.SetUnselected();
        Player1Barbarian.SetSelected();
        CheckButtons();
    }
    
    public void OnPlayer2KnightSelected()
    {
        player2 = "Prefabs/Knight";

        Player2Knight.SetSelected();

        Player2Barbarian.SetUnselected();
        CheckButtons();
    }
    public void OnPlayer2BarbarianSelected()
    {
        player2 = "Prefabs/Barbarian";

        Player2Knight.SetUnselected();
        Player2Barbarian.SetSelected();
        CheckButtons();
    }

    public void OnLevel1()
    {
        levelName = "Level1";

        Level1.SetSelected();
        Level2.SetUnselected();
        CheckButtons();
    }

    public void OnLevel2()
    {
        levelName = "Level2";

        Level1.SetUnselected();
        Level2.SetSelected();
        CheckButtons();
    }

    public void OnStartGame()
    {
        GameManager.Instance.characters.Add(player1);
        GameManager.Instance.characters.Add(player2);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName);
        asyncLoad.completed += AsyncLoad_completed;
    }

    private void AsyncLoad_completed(AsyncOperation obj)
    {
        GameManager.Instance.Initialize();
    }
}
