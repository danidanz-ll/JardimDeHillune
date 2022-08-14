using System;
using System.Collections.Generic;
using UnityEngine;

public class MatchWin : MonoBehaviour
{
    private List<GameObject> SpawnersGameObjects;
    private List<EnemySpawner> Spawners;
    public bool IsMatchWin { get; private set; } = false;

    private void Start()
    {
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
                Debug.Log("[ERROR] Não foi possível obter componente spawner do objeto!");
            }
        }
    }
    private void Update()
    {
        if( Spawners != null )
        {
            bool matchWin = true;
            foreach (EnemySpawner spawner in Spawners)
            {
                if (spawner.LivingEntities > 0)
                {
                    matchWin = false;
                }
            }
            if (matchWin && Spawners.Count > 0)
            {
                IsMatchWin = true;
                Debug.Log("Fim da partida, o jogador venceu!");
            }
        }
    }
}
