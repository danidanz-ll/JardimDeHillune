using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Enimies")] 
    [SerializableField] private List<GameObject> SpawnersGameObjects;

    private List<ISpawner> Spawners;

    private void Start()
    {
        foreach (GameObject spawnerGameObject in SpawnersGameObjects)
        {
            try
            {
                Spawners.Add(spawnerGameObject.GetComponent<ISpawner>());
            } catch (Exception ex)
            {
                Debug.Log("[ERROR] Não foi possível obter componente spawner do objeto!");
            }
        }
    }
    private void Update()
    {
        foreach (ISpawner spawner in Spawners)
        {
            if (spawner.LivingEntities <= 0)
            {
                Debug.Log("Player win!");
            }
        }
    }
}
