using UnityEngine;

public class EnemySpawner : Spawner
{
    protected override void Update()
    {
        base.Update();
        ActivateAllEntities(true);
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.transform.position = GetRandomPositionSpawn();
        }
    }
    public Vector3 GetRandomPositionSpawn()
    {
        return new Vector3(LenghtMap * Random.Range(-LenghtMap, LenghtMap), LenghtMap * Random.Range(-LenghtMap, LenghtMap), 0);
    }
}
