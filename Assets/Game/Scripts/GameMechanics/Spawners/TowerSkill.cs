using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TowerSpawner))]
public class TowerSkill : MonoBehaviour
{
    [SerializeField] public List<TowerSpawner> towerSpawners;
    public int currentTower = 0;
    public void Invoke(Vector3 position)
    {
        Transform towersParent = GameObject.FindGameObjectWithTag("TowersParent").transform;
        towerSpawners[currentTower].CreateEntity(position, towersParent);
    }
    public void SelectNextTower()
    {
        if (currentTower < towerSpawners.Count - 1)
        {
            currentTower++;
        }
        else
        {
            currentTower = 0;
        }
    }
}