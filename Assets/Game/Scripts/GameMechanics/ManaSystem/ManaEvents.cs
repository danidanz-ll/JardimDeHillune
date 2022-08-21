using System;
using System.Collections.Generic;
using UnityEngine;

public class ManaEvents : MonoBehaviour
{
    public event Action GetManaPoints;
    public event EventHandler<float> UseManaEvent; 

    private List<GameObject> SpawnersGameObjects = new List<GameObject>();
    private List<EnemySpawner> Spawners = new List<EnemySpawner>();

    private void Start()
    {
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

        foreach (EnemySpawner spawner in Spawners)
        {
            foreach (IMortal deathEvent in spawner.deathEvents)
            {
                deathEvent.DeathEvent += GetMana;
            }
        }
    }
    private void OnDestroy()
    {
        foreach (EnemySpawner spawner in Spawners)
        {
            foreach (IMortal deathEvent in spawner.deathEvents)
            {
                deathEvent.DeathEvent -= GetMana;
            }
        }
    }
    public void GetMana()
    {
        GetManaPoints.Invoke();
    }
    public void UseMana(float points)
    {
        UseManaEvent.Invoke(this, points);
    }
}
