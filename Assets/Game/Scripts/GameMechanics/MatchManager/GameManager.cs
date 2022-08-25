using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(GameOver))]
[RequireComponent(typeof(MatchWin))]
[RequireComponent(typeof(MatchTimer))]
[RequireComponent(typeof(GameEvents))]
public class GameManager : MonoBehaviour
{
    [Header("Level stats")]
    [SerializeField] private int Level;
    [SerializeField] private int NextLevel;
    [SerializeField] private float TimeToNextLevel;
    
    [Header("Texts")]
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private TMP_Text matchWinText;

    private GameOver gameOver;
    private MatchWin matchWin;
    private MatchTimer matchTimer;
    private GameEvents gameEvents;

    private bool IsRoundOver = false;
    private void Start()
    {
        gameOver = GetComponent<GameOver>();
        matchWin = GetComponent<MatchWin>();
        matchTimer = GetComponent<MatchTimer>();
        gameEvents = GetComponent<GameEvents>();

        gameEvents.TimeOver += ShowPlayerWin;
        gameEvents.MatchWin += ShowPlayerWin;
        gameEvents.GameOver += ShowGameOver;

        if (!HasKey("save_level"))
        {
            PlayerPrefs.SetInt("save_level", 1);
        }
    }
    private void ShowPlayerWin()
    {
        if (matchWinText != null && !gameOver.IsGameOver)
        {
            IsRoundOver = true;
            matchWinText.text = "Jardim protegido!";
            SaveProgress(NextLevel);
            StartCoroutine(NextLevel());
        }
    }
    private void ShowGameOver()
    {
        if (gameOverText != null && !(matchTimer.IsTimerFinshed || matchWin.IsMatchWin))
        {
            IsRoundOver = true;
            gameOverText.text = "Voc� perdeu!";
            SaveProgress(Level);
            StartCoroutine(NextLevel());
        }
    }
    private void SaveProgress(int level)
    {
        PlayerPrefs.SetInt("save_level", level);
    }
    public IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(TimeToNextLevel);
        SceneManager.LoadScene("Level_" + NextLevel);
    }
}
