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
        float x, y, angle;
        do
        {
            x = Random.Range(0, LenghtMap);
            y = Random.Range(0, LenghtMap);
            angle = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
        } while (angle >= angleRangeToSpawn && Mathf.Pow(Mathf.Pow(x, 2) + Mathf.Pow(y, 2), 1.0f/2.0f) >= LenghtMap / 2.0f);

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
    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Vector3 lineSpawn = new Vector3(0, LenghtMap, 0);
        Gizmos.DrawLine(transform.position, transform.position + (Quaternion.Euler(0, 0, angleRangeToSpawn)) * lineSpawn);
        Gizmos.DrawLine(transform.position, transform.position + (Quaternion.Euler(0, 0, -angleRangeToSpawn))* lineSpawn);
        Gizmos.DrawLine(transform.position, transform.position - (Quaternion.Euler(0, 0, angleRangeToSpawn)) * lineSpawn);
        Gizmos.DrawLine(transform.position, transform.position - (Quaternion.Euler(0, 0, -angleRangeToSpawn)) * lineSpawn);
    }
}
