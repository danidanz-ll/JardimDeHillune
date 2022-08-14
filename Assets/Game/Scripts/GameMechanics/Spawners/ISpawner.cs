using UnityEngine;

public interface ISpawner
{
    void Start();
    void CreateEntities();
    void ActivateAllEntities(bool active);
}
