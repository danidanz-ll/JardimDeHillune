using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MatchWin : MonoBehaviour
{
    [SerializeField] private TMP_Text matchWinText;

    private List<GameObject> SpawnersGameObjects;
    private List<EnemySpawner> Spawners;
    public bool IsMatchWin { get; private set; } = false;

    private void Start()
    {
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
                if (matchWinText != null)
                {
                    matchWinText.text = "Jardim protegido!";
                }
            }
        }
    }
}
