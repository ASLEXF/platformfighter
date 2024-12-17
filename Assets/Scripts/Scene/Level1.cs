using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    [SerializeField] int scoreL = 20;
    [SerializeField] int scoreR = 20;
    TMP_Text scoreLText, scoreRText;

    private void Awake()
    {
        scoreLText = transform.GetChild(0).Find("Player1").Find("ScoreL").GetComponent<TMP_Text>();
        scoreRText = transform.GetChild(0).Find("Player2").Find("ScoreR").GetComponent<TMP_Text>();
    }

    private void Start()
    {
        scoreLText.text = scoreL.ToString();
        scoreRText.text = scoreR.ToString();

        GameEvents.Instance.OnPlayer1Die += Instance_OnPlayer1Die;
        GameEvents.Instance.OnPlayer2Die += Instance_OnPlayer2Die;
    }

    private void Instance_OnPlayer1Die()
    {
        scoreL--;
        scoreLText.text = scoreL.ToString();

        if (scoreL == 0) gameSet(2);
    }

    private void Instance_OnPlayer2Die()
    {
        scoreR--;
        scoreRText.text = scoreR.ToString();

        if (scoreL == 0) gameSet(1);
    }

    private void gameSet(int playerId)
    {
        MultiPlayerVCam.Instance.FocusOn(playerId);
    }

    private IEnumerator waitForGameSet()
    {
        // draw
        yield return new WaitForSeconds(3.0f);
    }
}
