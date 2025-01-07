using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphicSettings : MonoBehaviour
{
    TMP_Dropdown resolutionDropDown;
    Toggle windowed, fullScreen;
    Button apply;
    GameObject confirmWindow;

    Resolution[] resolutions;

    [SerializeField] int countdown = 7;

    private void Awake()
    {
        resolutionDropDown = transform.GetChild(0).GetComponent<TMP_Dropdown>();
        windowed = transform.GetChild(1).GetChild(0).GetComponent<Toggle>();
        fullScreen = transform.GetChild(1).GetChild(1).GetComponent<Toggle>();
        apply = transform.GetChild(2).GetComponent<Button>();
        confirmWindow = transform.GetChild(3).gameObject;
    }

    private void Start()
    {
        initializeResolutionDropdown();

        apply.onClick.AddListener(() => GraphicManager.Instance.Apply(fullScreen.isOn, resolutions[resolutionDropDown.value]));
        apply.onClick.AddListener(() =>
            {
                StartCoroutine(ConfirmWindow());
            }
        );
    }

    private void initializeResolutionDropdown()
    {
        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();
        
        List<string> options = new List<string>();
        foreach (Resolution res in resolutions)
        {
            options.Add(res.ToString());
        }

        resolutionDropDown.options = options.ConvertAll(options => new TMP_Dropdown.OptionData(options));
    }

    public void OnSetWindowedIsOn() => fullScreen.isOn = !windowed.isOn;
    public void OnSetFullScreenIsOn() => windowed.isOn = !fullScreen.isOn;

    IEnumerator ConfirmWindow()
    {
        TMP_Text text = confirmWindow.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>();

        // countdown
        int current = countdown;

        while (current >= 0)
        {
            text.text = $"All changes will be reverted in {current} seconds.";
            yield return new WaitForSecondsRealtime(1);
            current--;
        }

        confirmWindow.SetActive(false);
        Revert();
    }

    public void Confirm() => GraphicManager.Instance.Confirm();
    public void Revert() => GraphicManager.Instance.Revert();
}
