using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TowerSpawner))]
public class TowerSkill : MonoBehaviour
{
    [SerializeField] public List<TowerSpawner> towerSpawners;
    public int currentTower = 0;
    private TowerSpawner towerSpawner;
    private void Start()
    {
        towerSpawner = GetComponent<TowerSpawner>();
    }
    public void Invoke(Vector3 position)
    {
        //towerSpawner.ActivateEntity(true, position);
        towerSpawners[currentTower].ActivateEntity(true, position);
    }
    public void SelectNextTower()
    {
        if (currentTower < towerSpawners.Count)
        {
            currentTower++;
        }
        else
        {
            currentTower = 0;
        }
    }
}