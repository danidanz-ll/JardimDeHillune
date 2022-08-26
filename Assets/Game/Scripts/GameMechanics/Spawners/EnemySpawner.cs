using System.Collections;
using UnityEngine;

public class EnemySpawner : MobSpawner
{
    [SerializeField] private MatchTimer matchTimer;
    [SerializeField] private float angleRangeToSpawn = 30.0f;
    [SerializeField][Min(0)] private float timeToResurect = 3.0f;
    [SerializeField][Min(0)] private float IntervalBetweenSpawn = 3.0f;

    private float oldTime = 0;

    public override void Start()
    {
        base.Start();
        matchTimer = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MatchTimer>();
        ActivateAllEntities(true);
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.transform.position = GetRandomPositionSpawn();
        }
        ActivateAllEntities(false);
        oldTime = matchTimer.timer;
    }
    private void Update() 
    {
        if (oldTime - matchTimer.timer >= IntervalBetweenSpawn)
        {
            oldTime = matchTimer.timer;
            ReleaseDeactivatedEntity();
        }
    }
    private void ReleaseDeactivatedEntity()
    {
        foreach (GameObject gameObject in gameObjects)
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                LivingEntities++;
                break;
            }
        }
    }
    public override void CountDeath()
    {
        base.CountDeath();
        StartCoroutine(ResurrectEntityDead());
    }
    public IEnumerator ResurrectEntityDead()
    {
        yield return new WaitForSeconds(timeToResurect);
        int i = 0;
        foreach (IMortal deathEvent in deathEvents)
        {
            if (deathEvent != null)
            {
                if (deathEvent.IsDead)
                {
                    RepositionEntity(gameObjects[i].transform);
                    deathEvent.Resurrect();
                    break;
                }
            }
            i++;
        }
        yield break;
    }
    private void RepositionEntity(Transform transformEntity)
    {
        transformEntity.position = GetRandomPositionSpawn();
    }
    public Vector3 GetRandomPositionSpawn()
    {
        Vector3 spawnPosition = Quaternion.AngleAxis(Random.Range(0, angleRangeToSpawn), Vector3.up) * Vector3.right * LenghtMap;

        if (Random.Range(-1, 1) < 0)
        {
            spawnPosition.x *= -1;
        }

        if (Random.Range(-1, 1) < 0)
        {
            spawnPosition.y *= -1;
        }

        return spawnPosition;
    }
    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Vector3 lineSpawn = new Vector3(LenghtMap, 0, 0);
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.AngleAxis(Random.Range(0, angleRangeToSpawn), lineSpawn) * lineSpawn);
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.AngleAxis(Random.Range(0, -angleRangeToSpawn), lineSpawn) * lineSpawn);
        Gizmos.DrawLine(transform.position, transform.position - Quaternion.AngleAxis(Random.Range(0, angleRangeToSpawn), lineSpawn) * lineSpawn);
        Gizmos.DrawLine(transform.position, transform.position - Quaternion.AngleAxis(Random.Range(0, -angleRangeToSpawn), lineSpawn) * lineSpawn);
    }
}
