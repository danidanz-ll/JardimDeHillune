using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameOver))]
[RequireComponent(typeof(MatchWin))]
[RequireComponent(typeof(MatchTimer))]
public class GameManager : MonoBehaviour
{
    private GameOver gameOver;
    private MatchWin matchWin;
    private MatchTimer matchTimer;
    private void Start()
    {
        gameOver = GetComponent<GameOver>();
        matchWin = GetComponent<MatchWin>();
        matchTimer = GetComponent<MatchTimer>();
    }
    private void Update()
    {
        bool isRoundOver = false;
        if (gameOver.IsGameOver || matchWin.IsMatchWin || matchTimer.IsTimerFinshed)
        {
            isRoundOver = true;
        }

        if (isRoundOver)
        {
            Debug.Log("Fim da partida!");
            enabled = false;
        }
    }
}
