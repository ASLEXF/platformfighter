using System.Collections;
using TMPro;
using UnityEngine;

public class SettingsWindows : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] int countdown = 7;
    GameObject GraphicSettingsWindow;

    private void Awake()
    {
        GraphicSettingsWindow = canvas.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
    }

    private void Start()
    {
        GameEvents.Instance.OnApplyGraphicSettings += () => StartCoroutine(showGraphicConfirmWindow());
    }

    private IEnumerator showGraphicConfirmWindow()
    {
        canvas.SetActive(true);
        GraphicSettingsWindow.SetActive(true);
        TMP_Text text = GraphicSettingsWindow.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>();

        int current = countdown;

        while (current >= 0)
        {
            text.text = $"All changes will be reverted in {current} seconds.";
            yield return new WaitForSecondsRealtime(1);
            current--;
        }

        GraphicSettingsWindow.SetActive(false);
        Revert();
    }

    private void hideGraphicConfirmWindow()
    {
        GraphicSettingsWindow.SetActive(false);
        canvas.SetActive(false);
    }

    public void Confirm()
    {
        hideGraphicConfirmWindow();
        GraphicManager.Instance.Confirm();
        StopAllCoroutines();
    }
    public void Revert()
    {
        hideGraphicConfirmWindow();
        GraphicManager.Instance.Revert();
        StopAllCoroutines();
    }
}
