using System;
using System.Collections.Generic;
using UnityEngine;

public class MatchWin : MonoBehaviour
{
    private List<GameObject> SpawnersGameObjects = new List<GameObject>();
    private List<EnemySpawner> Spawners = new List<EnemySpawner>();
    private GameEvents gameEvents;
    public bool IsMatchWin { get; private set; } = false;
    private int spawnersInMatch = 0;

    private void Start()
    {
        gameEvents = GetComponent<GameEvents>();

        try
        {
            SpawnersGameObjects.AddRange(GameObject.FindGameObjectsWithTag("Spawner"));   
        } catch (Exception ex)
        {
            Debug.Log("[ERROR] Não foi possível obter os spawners dos inimigos: " + ex.ToString());
        }

        foreach (GameObject spawnerGameObject in SpawnersGameObjects)
        {
            try
            {
                Spawners.Add(spawnerGameObject.GetComponent<EnemySpawner>());
            } catch (Exception ex)
            {
                Debug.Log("[ERROR] Não foi possível obter componente spawner do objeto: " + ex.ToString());
            }
        }
        spawnersInMatch = Spawners.Count;
    }
    private void Update()
    {
        
    }
    private void AccountSpawnerFinished()
    {
        spawnersInMatch--;
        if (spawnersInMatch == 0)
        {
            SetPlayerWin();
        }
    }
    private void SetPlayerWin()
    {
        gameEvents.WarnMatchWin();
        IsMatchWin = true;
    }
}
