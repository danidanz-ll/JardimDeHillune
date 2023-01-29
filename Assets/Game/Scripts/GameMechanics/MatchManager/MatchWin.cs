using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MatchWin : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] private TMP_Text matchWinText;

    [Header("Enemies")]
    public List<EnemySpawner> EnemiesSpawners;

    private List<SpawnerEvents> SpawnersEvents = new List<SpawnerEvents>();
    private GameEvents gameEvents;
    public bool IsMatchWin { get; private set; } = false;
    private int spawnersInMatch;

    private void Start()
    {
        gameEvents = GetComponent<GameEvents>();
        gameEvents.TimeOver += SetPlayerWin;
        spawnersInMatch = EnemiesSpawners.Count;

        foreach (EnemySpawner enemySpawner in EnemiesSpawners) 
        {
            enemySpawner.SpawnerIsEmpty += DeleteSpawner;
        }
    }
    private void Update()
    {
        
    }
    private void ShowPlayerWin()
    {
        if (matchWinText != null)
        {
            matchWinText.text = "Jardim protegido!";
        }
    }
    private void SetPlayerWin()
    {
        gameEvents.WarnMatchWin();
        IsMatchWin = true;
        ShowPlayerWin();
        Debug.Log("Jogador venceu!");
    }
    private void DeleteSpawner()
    {
        spawnersInMatch--;

        if (spawnersInMatch == 0)
        {
            SetPlayerWin();
        }
    }
}
