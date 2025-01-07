using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ResolutionExample: MonoBehaviour
{
    public Resolution resolution = new Resolution();
}

public class GraphicManager : ResolutionExample
{
    private static GraphicManager instance;

    public static GraphicManager Instance
    { get { return instance; } }

    [Header("Current")]
    //[SerializeField] Resolution resolution;
    [SerializeField] bool isFullScreen = false;

    [Header("New")]
    Resolution newResolution;
    [SerializeField] bool newIsFullScreen;

    private void Awake()
    {
        if (instance is null)
            instance = this;
    }

    private void Start()
    {
        // load saved settings

        // no saving
        resolution = FindClosestResolution(Screen.currentResolution, Screen.resolutions);
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, resolution.refreshRateRatio);
    }

    private void Update()
    {
        // auto adjust resolution
    }

    private Resolution FindClosestResolution(Resolution current, Resolution[] supported)
    {
        Resolution bestMatch = supported[0];
        int smallestDifference = int.MaxValue;

        foreach (Resolution res in supported)
        {
            int resolutionDifference = Mathf.Abs(res.width - current.width) + Mathf.Abs(res.height - current.height);

            int refreshRateDifference = Mathf.Abs(res.refreshRate - current.refreshRate);

            int totalDifference = resolutionDifference + refreshRateDifference;  // roughly

            if (totalDifference < smallestDifference)
            {
                smallestDifference = totalDifference;
                bestMatch = res;
            }
        }

        return bestMatch;
    }

    public void Apply(bool isFullScreen, Resolution res)
    {
        newResolution = res;
        newIsFullScreen = isFullScreen;

        Screen.fullScreenMode = isFullScreen ? FullScreenMode.ExclusiveFullScreen : FullScreenMode.Windowed;

        Screen.SetResolution(res.width, res.height, Screen.fullScreenMode, res.refreshRateRatio);
    }

    public void Confirm()
    {
        resolution = newResolution;
        isFullScreen = newIsFullScreen;
    }

    public void Revert()
    {
        Screen.fullScreenMode = isFullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, resolution.refreshRateRatio);
    }

    public string GetResolution() => resolution.ToString();
}
