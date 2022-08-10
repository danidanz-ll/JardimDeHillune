using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnerEvents))]
public class Spawner : MonoBehaviour, ISpawner
{
    [Header("Spawn settings")]
    [SerializeField] private GameObject Entity; 
    [SerializeField] [Min(0)] private int NumberOfentities = 0;
    [SerializeField] [Min(0)] private float LenghtMap = 0;

    public int LivingEntities { get; private set; }
    private List<GameObject> gameObjects = new List<GameObject>();
    private List<IMortal> deathEvents = new List<IMortal>();
    private SpawnerEvents spawnerEvents;
    void Start()
    {
        spawnerEvents = GetComponent<SpawnerEvents>();
        LivingEntities = NumberOfentities;
        CreateEntities();

        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.transform.parent = transform;
        }
        ActivateAllEntities(true);
        GetMortalComponents();
        SubscribeDeathEvent();
        ActivateAllEntities(false);
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
    public void ActivateAllEntities(bool active)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(active);
        }
    }
    private void CountDeath()
    {
        LivingEntities--;

        if (LivingEntities == 0)
        {
            Debug.Log("All units are dead!");
            spawnerEvents.WarnAllUnitsDied();
        }
    }
}
