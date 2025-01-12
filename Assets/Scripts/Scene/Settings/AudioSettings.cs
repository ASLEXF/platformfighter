using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    Scrollbar mainVolume;
    Scrollbar SFXVolume;
    Scrollbar BGMVolume;

    private void Awake()
    {
        mainVolume = transform.GetChild(0).GetChild(0).GetComponentInChildren<Scrollbar>();
        SFXVolume = transform.GetChild(0).GetChild(1).GetComponentInChildren<Scrollbar>();
        BGMVolume = transform.GetChild(0).GetChild(2).GetComponentInChildren<Scrollbar>();
    }

    private void Start()
    {
        // load saved values
        mainVolume.value = AudioManager.Instance.mainVolume;
        SFXVolume.value = AudioManager.Instance.SFXVolume;
        BGMVolume.value = AudioManager.Instance.BGMVolume;

        // listen value changes
        mainVolume.onValueChanged.AddListener((value) => AudioManager.Instance.SetMainVolume(value));
        SFXVolume.onValueChanged.AddListener((value) => AudioManager.Instance.SetVFXVolume(value));
        BGMVolume.onValueChanged.AddListener((value) => AudioManager.Instance.SetBGMVolume(value));
    }
}
