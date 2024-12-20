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
    [SerializeField][Range(0.0001f, 1)] float mainVolume = 1;
    [SerializeField][Range(0.0001f, 1)] float VFXVolume = 1;
    [SerializeField][Range(0.0001f, 1)] float BGMVolume = 1;

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
        float value;
    }

    public void SetMainVolume(float volume)
    {
        mainVolume = (volume < 0.0001f ? 0.0001f : volume);
        audioMixer.SetFloat("MainVolume", Mathf.Log10(mainVolume) * 20);
    }
    public void SetVFXVolume(float volume)
    {
        VFXVolume = (volume < 0.0001f ? 0.0001f : volume);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(VFXVolume) * 20);
    }
    public void SetBGMVolume(float volume)
    {
        BGMVolume = (volume < 0.0001f ? 0.0001f : volume);
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(BGMVolume) * 20);
    }

    private void OnValidate()
    {
        audioMixer.SetFloat("MainVolume", Mathf.Log10(mainVolume) * 20);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(VFXVolume) * 20);
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(BGMVolume) * 20);
    }
}
