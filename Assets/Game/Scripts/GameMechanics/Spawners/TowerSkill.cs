using UnityEngine;

[RequireComponent(typeof(TowerSpawner))]
public class TowerSkill : MonoBehaviour
{
    private TowerSpawner towerSpawner;
    private void Start()
    {
        towerSpawner = GetComponent<TowerSpawner>();
    }
    public void Invoke(Vector3 position)
    {
        towerSpawner.ActivateEntity(true, position);
    }
}