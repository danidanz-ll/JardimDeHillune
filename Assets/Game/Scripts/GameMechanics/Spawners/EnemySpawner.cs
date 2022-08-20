using UnityEngine;

public class EnemySpawner : MobSpawner
{
    [SerializeField] private float angleRangeToSpawn = 30.0f;
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
        float x, y, angle;
        do
        {
            x = Random.Range(0, LenghtMap);
            y = Random.Range(0, LenghtMap);
            angle = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
        } while (angle >= angleRangeToSpawn);

        if (Random.Range(-1, 1) < 0)
        {
            x *= -1;
        }

        if (Random.Range(-1, 1) < 0)
        {
            y *= -1;
        }

        return new Vector3(x, y, 0);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, LenghtMap);

        Gizmos.DrawLine(transform.position, transform.position + (Quaternion.Euler(0, 0, angleRangeToSpawn / 2));
        Gizmos.DrawLine(transform.position, transform.position + (Quaternion.Euler(0, 0, -angleRangeToSpawn / 2));
        Gizmos.DrawLine(transform.position, transform.position - (Quaternion.Euler(0, 0, angleRangeToSpawn / 2));
        Gizmos.DrawLine(transform.position, transform.position - (Quaternion.Euler(0, 0, -angleRangeToSpawn / 2));
    }
}
