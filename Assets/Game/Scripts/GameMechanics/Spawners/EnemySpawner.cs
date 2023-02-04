using System.Collections;
using UnityEngine;

public class EnemySpawner : MobSpawner
{

    [SerializeField]
    [Tooltip("Inst�ncia do match timer.")]
    private MatchTimer matchTimer;

    [SerializeField]
    private float angleRangeToSpawn = 30.0f;

    [SerializeField]
    [Min(0)]
    [Tooltip("Define o intervalo entre o spawn de cada inimigo.")]
    private float IntervalBetweenSpawn = 3.0f;

    [SerializeField]
    private string EnemyName;

    private float oldTime = 0;
    private ManaEvents PlayerManaEvents;


    public override void Start()
    {
        base.Start();
        matchTimer = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MatchTimer>();
        oldTime = matchTimer.timer;
        PlayerManaEvents = ObeliscController.Instance.GetComponent<ManaEvents>();


        if (Settings.GetUserSettings())
        {
            NumberMaxEntities = SettingsSpawners.GetMaxMob(EnemyName);
            IntervalBetweenSpawn = SettingsSpawners.GetSpawnInterval(EnemyName);
        }
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
    public static void GiveManaToPlayer(object sender, GameObject mob)
    {
        PlayerController.Instance.GetComponent<ManaEvents>().GetMana();
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
