using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MatchWin : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] private TMP_Text matchWinText;

    private List<GameObject> SpawnersGameObjects = new List<GameObject>();
    private List<EnemySpawner> Spawners = new List<EnemySpawner>();
    private List<SpawnerEvents> SpawnersEvents = new List<SpawnerEvents>();
    private GameEvents gameEvents;
    public bool IsMatchWin { get; private set; } = false;
    private int spawnersInMatch = 0;

    private void Start()
    {
        gameEvents = GetComponent<GameEvents>();

        gameEvents.TimeOver += SetPlayerWin;
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
    }
}
