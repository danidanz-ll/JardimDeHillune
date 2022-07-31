using System.Collections.Generic;
using UnityEngine;

public class SkeletonSpawner : MonoBehaviour, ISpawner
{
    [Header("Spawn settings")]
    [SerializeField] private GameObject Entity; 
    [SerializeField] [Min(0)] private int NumberOfentities = 0;
    [SerializeField] [Min(0)] private float LenghtMap = 0;

    public int LivingEntities { get; private set; }
    private List<GameObject> gameObjects = new List<GameObject>();
    private List<IMortal> deathEvents = new List<IMortal>();
    void Start()
    {
        LivingEntities = NumberOfentities;
        CreateEntities();

        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.transform.parent = transform;
            gameObject.transform.position = GetRandomPositionSpawn();
        }
        ActivateAllEntities();
        GetMortalComponents();
        SubscribeDeathEvent();
    }
    private void Update()
    {
        if (LivingEntities == 0)
        {
            Debug.Log("All units are dead!");
        }
    }

    private void Destroy()
    {
        UnsubscribeDeathEvent();
    }
    public void CreateEntities()
    {
        for (int i = 0; i < NumberOfentities; i++)
        {
            gameObjects.Add(Instantiate(Entity, new Vector3(0, 0, 0), Quaternion.identity));
        }
    }
    private void GetMortalComponents()
    {
        foreach (GameObject gameObject in gameObjects)
        {
            deathEvents.Add(gameObject.GetComponent<IMortal>());
        }
    }
    private void SubscribeDeathEvent()
    {
        foreach (IMortal deathEvent in deathEvents)
        {
            deathEvent.DeathEvent += CountDeath;
        }
    }
    private void UnsubscribeDeathEvent()
    {
        foreach (IMortal deathEvent in deathEvents)
        {
            if (deathEvent != null)
            {
                deathEvent.DeathEvent -= CountDeath;
            }
        }
    }
    public void ActivateAllEntities()
    {
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(true);
        }
    }
    public Vector3 GetRandomPositionSpawn()
    {
        
        return new Vector3(LenghtMap * Random.Range(-LenghtMap, LenghtMap), LenghtMap * Random.Range(-LenghtMap, LenghtMap), 0);
    }
    private void CountDeath()
    {
        LivingEntities--;
    }

}
