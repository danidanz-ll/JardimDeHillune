using UnityEngine;

public interface ISpawner
{
    void CreateEntities();
    void ActivateAllEntities(bool active);
    int LivingEntities { get; }
}
