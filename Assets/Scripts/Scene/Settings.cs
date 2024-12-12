using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    [SerializeField] GameObject inputSettings;
    [SerializeField] GameObject audioSettings;

    private void Start()
    {
        RebindManager.Instance.Initialize();
    }

    public void GraphicsSettings()
    {
        inputSettings.SetActive(false);
        audioSettings.SetActive(false);
    }

    public void InputSettings()
    {
        inputSettings.SetActive(true);
        audioSettings.SetActive(false);
    }

    public void AudioSettings()
    {
        inputSettings.SetActive(false);
        audioSettings.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
