using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    Scrollbar mainVolume;
    Scrollbar VFXVolume;
    Scrollbar BGMVolume;

    private void Awake()
    {
        mainVolume = transform.GetChild(0).GetChild(0).GetComponentInChildren<Scrollbar>();
        VFXVolume = transform.GetChild(0).GetChild(1).GetComponentInChildren<Scrollbar>();
        BGMVolume = transform.GetChild(0).GetChild(2).GetComponentInChildren<Scrollbar>();
    }

    private void Start()
    {
        mainVolume.onValueChanged.AddListener((value) => AudioManager.Instance.SetMainVolume(value));
        VFXVolume.onValueChanged.AddListener((value) => AudioManager.Instance.SetVFXVolume(value));
        BGMVolume.onValueChanged.AddListener((value) => AudioManager.Instance.SetBGMVolume(value));
    }
}
