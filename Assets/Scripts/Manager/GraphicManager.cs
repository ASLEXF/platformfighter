using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GraphicManager : MonoBehaviour
{
    private static GraphicManager instance;

    public static GraphicManager Instance
    { get { return instance; } }

    private void Awake()
    {
        if (instance is null)
            instance = this;
    }

    private void Start()
    {
        // load saved settings
    }

    public void Apply(bool isFullScreen, string resolution)
    {
        Screen.fullScreen = isFullScreen;

        switch (resolution)
        {
            case "1280¡Á720":
                Screen.SetResolution(1280, 720, Screen.fullScreen);
                break;
            case "1920¡Á1080":
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;
            case "2560¡Á1600":
                Screen.SetResolution(2560, 1600, Screen.fullScreen);
                break;
            case "3840¡Á2160":
                Screen.SetResolution(3840, 2160, Screen.fullScreen);
                break;
            default:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                Debug.LogWarning($"wrong resolution: {resolution}!");
                break;
        }
    }
}
