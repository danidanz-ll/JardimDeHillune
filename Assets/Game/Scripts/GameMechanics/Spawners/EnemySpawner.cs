using System.Collections;
using UnityEngine;

public class EnemySpawner : MobSpawner
{
    [SerializeField]
    [Tooltip("Instância do match timer.")]
    private MatchTimer matchTimer;

    [SerializeField]
    private float angleRangeToSpawn = 30.0f;

    [SerializeField]
    [Min(0)]
    [Tooltip("Define o intervalo entre o spawn de cada inimigo.")]
    private float IntervalBetweenSpawn = 3.0f;

    private float oldTime = 0;
    private ManaEvents PlayerManaEvents;


    public override void Start()
    {
        base.Start();
        matchTimer = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MatchTimer>();
        oldTime = matchTimer.timer;
        PlayerManaEvents = PlayerController.Instance.GetComponent<ManaEvents>();
    }
    private void Update() 
    {
        if (oldTime - matchTimer.timer >= IntervalBetweenSpawn)
        {
            oldTime = matchTimer.timer;
            try
            {
                GameObject mob = CreateEntity(GetRandomPositionSpawn(), transform);
                LifeSystem lifeSystem = mob.GetComponent<LifeSystem>();
                EnemyController enemyController = mob.GetComponent<EnemyController>();
                enemyController.StartEnemy(this);
                lifeSystem.DeathGameObjectEvent += GiveManaToPlayer;
            } catch
            {
                return;
            }
        }
    }
    public Vector3 GetRandomPositionSpawn()
    {   
        Vector3 spawnPosition = Quaternion.Euler(Random.Range(0, angleRangeToSpawn / 2.0f), 0.0f, 0.0f) * Vector3.right * LenghtMap;

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
    private void GiveManaToPlayer(object sender, GameObject mob)
    {
        LifeSystem lifeSystem = mob.GetComponent<LifeSystem>();
        lifeSystem.DeathGameObjectEvent -= GiveManaToPlayer;
        PlayerManaEvents.GetMana();
    }
    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Vector3 lineSpawn = new Vector3(LenghtMap, 0, 0);
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(angleRangeToSpawn / 2.0f, 0.0f, 0.0f) * lineSpawn);
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(-angleRangeToSpawn / 2.0f, 0.0f, 0.0f) * lineSpawn);
        Gizmos.DrawLine(transform.position, transform.position - Quaternion.Euler(angleRangeToSpawn / 2.0f, 0.0f, 0.0f) * lineSpawn);
        Gizmos.DrawLine(transform.position, transform.position - Quaternion.Euler(-angleRangeToSpawn / 2.0f, 0.0f, 0.0f) * lineSpawn);
    }
}
