using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    private static Settings instance;

    public static Settings Instance
    { get { return instance; } }

    [SerializeField] GameObject camera, light, eventSystem;
    //[SerializeField] public string LastScene;

    private void Awake()
    {
        if (instance is null)
            instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            Return();
        }
    }

    public void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
