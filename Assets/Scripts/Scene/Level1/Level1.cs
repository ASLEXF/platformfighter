using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class Level1 : MonoBehaviour
{
    [SerializeField] int scoreL = 20;
    [SerializeField] int scoreR = 20;
    TMP_Text scoreLText, scoreRText;
    GameObject panel, pausedWindow, mainMenuNotification, quitGameNotification;

    private void Awake()
    {
        scoreLText = transform.GetChild(0).Find("Player1").Find("ScoreL").GetComponent<TMP_Text>();
        scoreRText = transform.GetChild(0).Find("Player2").Find("ScoreR").GetComponent<TMP_Text>();
        panel = transform.GetChild(1).gameObject;
        pausedWindow = transform.GetChild(1).GetChild(0).gameObject;
        mainMenuNotification = transform.GetChild(1).GetChild(1).gameObject;
        quitGameNotification = transform.GetChild(1).GetChild(2).gameObject;
    }

    private void Start()
    {
        scoreLText.text = scoreL.ToString();
        scoreRText.text = scoreR.ToString();
        panel.SetActive(false);
        pausedWindow.SetActive(false);
        mainMenuNotification.SetActive(false);
        quitGameNotification.SetActive(false);

        GameEvents.Instance.OnPlayer1Die += Instance_OnPlayer1Die;
        GameEvents.Instance.OnPlayer2Die += Instance_OnPlayer2Die;
        GameEvents.Instance.OnQuickSettingsReturned += showPausedWindow;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            if (GameManager.Instance.UIEnum == UIEnum.TempScene)
            {
                showPausedWindow();
            }
            else if (quitGameNotification.activeSelf)
                quitGameNotification.SetActive(false);
            else if (mainMenuNotification.activeSelf)
                mainMenuNotification.SetActive(false);
            else if (panel.activeSelf)
            {
                hidePausedWindow();
                Resume();
            }
            else
            {
                Pause();
                showPausedWindow();
            }
        }
    }

    private void Instance_OnPlayer1Die()
    {
        scoreL--;
        scoreLText.text = scoreL.ToString();

        if (scoreL == 0) gameSet(2);
    }

    private void Instance_OnPlayer2Die()
    {
        scoreR--;
        scoreRText.text = scoreR.ToString();

        if (scoreL == 0) gameSet(1);
    }

    private void gameSet(int playerId)
    {
        MultiPlayerVCam.Instance.FocusOn(playerId);
    }

    private IEnumerator waitForGameSet()
    {
        // draw
        yield return new WaitForSeconds(3.0f);
    }

    private void showPausedWindow()
    {
        panel.SetActive(true);
        pausedWindow.SetActive(true);
        mainMenuNotification.SetActive(false);
        quitGameNotification.SetActive(false);
    }

    private void hidePausedWindow()
    {
        panel.SetActive(false);
        pausedWindow.SetActive(false);
        mainMenuNotification.SetActive(false);
        quitGameNotification.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }

    public void ToSettings()
    {
        SceneManager.LoadSceneAsync("QuickSettings", LoadSceneMode.Additive);
        GameManager.Instance.UIEnum = UIEnum.TempScene;
        pausedWindow.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
