using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameOver))]
[RequireComponent(typeof(MatchWin))]
[RequireComponent(typeof(MatchTimer))]
[RequireComponent(typeof(GameEvents))]
public class GameManager : MonoBehaviour
{
    private GameOver gameOver;
    private MatchWin matchWin;
    private MatchTimer matchTimer;
    private GameEvents gameEvents;
    private void Start()
    {
        gameOver = GetComponent<GameOver>();
        matchWin = GetComponent<MatchWin>();
        matchTimer = GetComponent<MatchTimer>();
        gameEvents = GetComponent<GameEvents>();
    }
    private void Update()
    {
        bool isRoundOver = false;
        if (gameOver.IsGameOver || matchTimer.IsTimerFinshed || matchWin.IsMatchWin)
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
