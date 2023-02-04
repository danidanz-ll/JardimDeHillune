using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnerEvents))]
public class MobSpawner : MonoBehaviour, ISpawner
{
    [Header("Spawn settings")]
    [SerializeField] 
    public GameObject Entity;
    
    [SerializeField] 
    [Min(0)] 
    public int NumberMaxEntities = 0;
    
    [SerializeField] 
    [Min(0)] 
    public float LenghtMap = 0;
    
    [SerializeField]
    [Min(0)] 
    public float TimeToDestroy = 5.0f;

    [HideInInspector]
    public SpawnerEvents spawnerEvents { get; private set; }

    [HideInInspector]
    public event Action SpawnerIsEmpty;

    private List<GameObject> mobs;
    private int MobsDead;
    private int MobsCreated;
    public virtual void Start()
    {
        spawnerEvents = GetComponent<SpawnerEvents>();
        mobs = new List<GameObject>();
        MobsDead = 0;
        MobsCreated = 0;
    }
    public GameObject CreateEntity(Transform parentGameObject = null)
    {
        if (MobsCreated < NumberMaxEntities)
        {
            GameObject mob = Instantiate(Entity, new Vector3(0, 0, 0), Quaternion.identity);

            if (parentGameObject != null)
            {
                mob.transform.SetParent(parentGameObject);
            }

            return mob;
        }
        return null;
    }
    public GameObject CreateEntity(Vector3 position, Transform parentGameObject=null)
    {
        if (MobsCreated < NumberMaxEntities)
        {
            GameObject mob = Instantiate(Entity, position, Quaternion.identity);
            mobs.Add(mob);

            if (parentGameObject != null)
            {
                mob.transform.SetParent(parentGameObject);
            }

            MobsCreated++;
            return mob;
        }
        return null;
    }
    public void DestroyMob(GameObject mob)
    {
        if (mobs.Contains(mob))
        {
            mobs.Remove(mob);
            Destroy(mob, TimeToDestroy);
            MobsDead++;
        }

        if (MobsDead == NumberMaxEntities)
        {
            try
            {
                SpawnerIsEmpty.Invoke();
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }
        }
    }
    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, LenghtMap);
    }
}
