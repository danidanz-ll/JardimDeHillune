using UnityEngine;

public interface ISpawner
{
    void CreateEntities();
    void ActivateAllEntities();
    Vector3 GetRandomPositionSpawn();
    int LivingEntities { get; }
}
