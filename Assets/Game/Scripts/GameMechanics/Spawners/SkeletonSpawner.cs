using System.Collections.Generic;
using UnityEngine;

public class SkeletonSpawner : MonoBehaviour, ISpawner
{
    [Header("Spawn settings")]
    [SerializeField]
    private GameObject Entity;
    [SerializeField]
    [Min(0)]
    private int NumberOfentities = 0;
    [SerializeField]
    [Min(0)]
    private float LenghtMap = 0;

    private List<GameObject> gameObjects = new List<GameObject>();
    void Start()
    {
        for (int i = 0; i < NumberOfentities; i++)
        {
            gameObjects.Add(Instantiate(Entity, new Vector3(0, 0, 0), Quaternion.identity));
        }

        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.transform.parent = transform;
            gameObject.transform.position = GetRandomPositionSpawn();
        }

        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(true);
        }
    }
    public Vector3 GetRandomPositionSpawn()
    {
        
        return new Vector3(LenghtMap * Random.Range(-LenghtMap, LenghtMap), LenghtMap * Random.Range(-LenghtMap, LenghtMap), 0);
    }


}
