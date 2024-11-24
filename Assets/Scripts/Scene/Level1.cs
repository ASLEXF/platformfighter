using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    [SerializeField] int scoreL = 0;
    [SerializeField] int scoreR = 0;
    TMP_Text scoreLText, scoreRText;

    private void Awake()
    {
        scoreLText = transform.GetChild(0).Find("Player1").Find("ScoreL").GetComponent<TMP_Text>();
        scoreRText = transform.GetChild(0).Find("Player2").Find("ScoreR").GetComponent<TMP_Text>();
    }

    private void Start()
    {
        GameEvents.Instance.OnPlayer1Die += Instance_OnPlayer1Die;
        GameEvents.Instance.OnPlayer2Die += Instance_OnPlayer2Die;
    }

    private void Instance_OnPlayer1Die()
    {
        scoreR++;
        scoreRText.text = scoreR.ToString();
    }

    private void Instance_OnPlayer2Die()
    {
        scoreL++;
        scoreLText.text = scoreL.ToString();
    }
}
