using System;
using System.Collections.Generic;
using UnityEngine;

public class MatchWin : MonoBehaviour
{
    private List<GameObject> SpawnersGameObjects;

    private List<ISpawner> Spawners;
    public bool IsMatchWin { get; private set; } = false;

    private void Start()
    {   
        var objects = GameObject.FindGameObjectWithTag("Spawner");
        Debug.Log(objects.ToString());   
        SpawnersGameObjects.Add(new GameObject("teste"));   
        /*foreach (GameObject spawnerGameObject in objects)
        {
            try
            {
                SpawnersGameObjects.Add(spawnerGameObject);
            }catch (Exception ex)
            {
                Debug.Log("[ERROR] O objeto Spawner esta nulo!");
            }
        }*/
        Debug.Log(SpawnersGameObjects.ToString()); 
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
        bool matchWin = true;
        foreach (ISpawner spawner in Spawners)
        {
            if (spawner.LivingEntities > 0)
            {
                matchWin = false;
            }
        }
        if (matchWin)
        {
            Debug.Log("Fim da partida, o jogador venceu!");
        }
    }
}
