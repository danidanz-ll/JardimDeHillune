using UnityEngine;

public class EnemySpawner : MobSpawner
{
    public override void Start()
    {
        base.Start();
        ActivateAllEntities(true);
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.transform.position = GetRandomPositionSpawn();
        }
    }
    private void Update()
    {
    }
    public Vector3 GetRandomPositionSpawn()
    {
        return new Vector3(LenghtMap * Random.Range(-LenghtMap, LenghtMap), LenghtMap * Random.Range(-LenghtMap, LenghtMap), 0);
    }
}
