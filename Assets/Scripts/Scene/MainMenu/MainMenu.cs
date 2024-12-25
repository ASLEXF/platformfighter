using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.ClearAll();
    }

    public void OnStartGame()
    {
        SceneManager.LoadScene("Select");
    }

    public void OnSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
