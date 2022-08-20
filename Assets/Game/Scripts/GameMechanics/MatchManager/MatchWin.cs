using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MatchWin : MonoBehaviour
{
    [SerializeField] private TMP_Text matchWinText;

    private List<GameObject> SpawnersGameObjects;
    private List<EnemySpawner> Spawners;
    private GameEvents gameEvents;
    public bool IsMatchWin { get; private set; } = false;
    private int spawnersInMatch = 0;

    private void Start()
    {
        gameEvents = GetComponent<GameEvents>();
        SpawnersGameObjects = new List<GameObject>();
        Spawners = new List<EnemySpawner>();
        try
        {
            var spawnersTeste = GameObject.FindGameObjectsWithTag("Spawner");
            Debug.Log(spawnersTeste.ToString());
            SpawnersGameObjects.AddRange(spawnersTeste);   
        } catch (Exception ex)
        {
            Debug.Log("[ERROR] Não foi possível obter os spawners dos inimigos!" + ex.ToString());
            return;
        }

        foreach (GameObject spawnerGameObject in SpawnersGameObjects)
        {
            try
            {
                Spawners.Add(spawnerGameObject.GetComponent<EnemySpawner>());
            } catch (Exception ex)
            {
                Debug.Log("[ERROR] Não foi possível obter componente spawner do objeto! (" + ex.ToString() + ")");
            }
        }
        spawnersInMatch = Spawners.Count;
        gameEvents.TimeOver += SetPlayerWin;
    }
    private void Update()
    {
        if (spawnersInMatch == 0)
        {
            SetPlayerWin();
        }
    }
    private void AccountSpawnerFinished()
    {
        spawnersInMatch--;
    }
    private void SetPlayerWin()
    {
        IsMatchWin = true;
        if (matchWinText != null)
        {
            matchWinText.text = "Jardim protegido!";
        }
    }
}
