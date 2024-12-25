using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickSettings : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            Return();
        }
    }

    public void Return()
    {
        SceneManager.UnloadSceneAsync(5);
        GameManager.Instance.UIEnum = UIEnum.Normal;

        GameEvents.Instance.QuickSettingsReturned();
    }
}
