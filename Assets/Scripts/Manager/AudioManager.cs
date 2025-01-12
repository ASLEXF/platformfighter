using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager Instance
    { get { return instance; } }

    [SerializeField] AudioMixer audioMixer = null!;
    [SerializeField][Range(0.0001f, 1)] public float mainVolume;
    [SerializeField][Range(0.0001f, 1)] public float SFXVolume;
    [SerializeField][Range(0.0001f, 1)] public float BGMVolume;

    private void Awake()
    {
        if (instance is null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        // load saved volume levels
        mainVolume = PlayerPrefs.GetFloat("MainVolume", 1);
        SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1);
        BGMVolume = PlayerPrefs.GetFloat("BGMVolume", 1);
    }

    public void SetMainVolume(float volume)
    {
        mainVolume = (volume < 0.0001f ? 0.0001f : volume);
        audioMixer.SetFloat("MainVolume", Mathf.Log10(mainVolume) * 20);
        PlayerPrefs.SetFloat("MainVolume", (float)mainVolume);
    }
    public void SetVFXVolume(float volume)
    {
        SFXVolume = (volume < 0.0001f ? 0.0001f : volume);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(SFXVolume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", (float)SFXVolume);
    }
    public void SetBGMVolume(float volume)
    {
        BGMVolume = (volume < 0.0001f ? 0.0001f : volume);
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(BGMVolume) * 20);
        PlayerPrefs.SetFloat("BGMVolume", (float)BGMVolume);
    }

    private void OnValidate()
    {
        SetMainVolume(mainVolume);
        SetVFXVolume(SFXVolume);
        SetBGMVolume(BGMVolume);
    }
}
