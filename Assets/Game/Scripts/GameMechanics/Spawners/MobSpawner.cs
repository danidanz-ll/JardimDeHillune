using UnityEngine;

[RequireComponent(typeof(SpawnerEvents))]
public class MobSpawner : MonoBehaviour, ISpawner
{
    [Header("Spawn settings")]
    [SerializeField] public GameObject Entity;
    [SerializeField] [Min(0)] public int NumberOfEntitiesInGame = 0;
    [SerializeField] [Min(0)] public int NumberMaxEntities = 0;
    [SerializeField] [Min(0)] public float LenghtMap = 0;
    [SerializeField][Min(0)] public float TimeToDestroy = 5.0f;

    public int LivingEntities { get; private set; } = 0;
    public int EliminatedEntities { get; private set; } = 0;
    public SpawnerEvents spawnerEvents { get; private set; }
    public virtual void Start()
    {
        spawnerEvents = GetComponent<SpawnerEvents>();
    }
    public GameObject CreateEntity(Transform parentGameObject = null)
    {
        if (EliminatedEntities < NumberMaxEntities && LivingEntities < NumberOfEntitiesInGame)
        {
            GameObject mob = Instantiate(Entity, new Vector3(0, 0, 0), Quaternion.identity);
            IMortal mortalComponent = mob.GetComponent<IMortal>();
            mortalComponent.DeathEvent += CountDeath;
            mortalComponent.DeathGameObjectEvent += DestroyMob;

            if (parentGameObject != null)
            {
                mob.transform.SetParent(parentGameObject);
            }
            LivingEntities++;
            return mob;
        }
        return null;
    }
    public GameObject CreateEntity(Vector3 position, Transform parentGameObject=null)
    {
        if (EliminatedEntities < NumberMaxEntities && LivingEntities < NumberOfEntitiesInGame)
        {
            GameObject mob = Instantiate(Entity, position, Quaternion.identity);
            LifeSystem lifeSystem = mob.GetComponent<LifeSystem>();
            lifeSystem.DeathEvent += CountDeath;
            lifeSystem.DeathGameObjectEvent += DestroyMob;

            if (parentGameObject != null)
            {
                mob.transform.SetParent(parentGameObject);
            }
            LivingEntities++;
            return mob;
        }
        return null;
    }
    public virtual void CountDeath()
    {
        LivingEntities--;
        EliminatedEntities++;

        if (EliminatedEntities == NumberMaxEntities)
        {
            spawnerEvents.WarnAllUnitsDied();
        }
    }
    public void DestroyMob(object sender, GameObject mob)
    {
        LifeSystem lifeSystem = mob.GetComponent<LifeSystem>();
        lifeSystem.DeathEvent -= CountDeath;
        lifeSystem.DeathGameObjectEvent -= DestroyMob;
        Destroy(mob, TimeToDestroy);
    }
    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, LenghtMap);
    }
}
