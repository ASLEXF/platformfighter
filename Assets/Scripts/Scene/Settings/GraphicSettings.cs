using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphicSettings : MonoBehaviour
{
    TMP_Dropdown resoluition;
    Toggle windowed, fullScreen;
    Button apply;

    private void Awake()
    {
        resoluition = transform.GetChild(0).GetComponent<TMP_Dropdown>();
        windowed = transform.GetChild(1).GetChild(0).GetComponent<Toggle>();
        fullScreen = transform.GetChild(1).GetChild(1).GetComponent<Toggle>();
        apply = transform.GetChild(2).GetComponent<Button>();
    }

    private void Start()
    {
        apply.onClick.AddListener(() => GraphicManager.Instance.Apply(fullScreen.isOn, resoluition.options[resoluition.value].text));
    }

    public void OnSetWindowedIsOn() => fullScreen.isOn = !windowed.isOn;
    public void OnSetFullScreenIsOn() => windowed.isOn = !fullScreen.isOn;
}
