using UnityEngine;

public class TowerSpawner : Spawner
{
    public void ActivateEntity(bool active, Vector3 position)
    {
        if (LivingEntities < NumberOfentities && active)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (!gameObject.activeSelf)
                {
                    gameObject.SetActive(active);
                    gameObject.transform.position = position;
                    LivingEntities++;
                    break;
                }
            }
        } else if (LivingEntities > 0 && !active)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.activeSelf)
                {
                    gameObject.SetActive(active);
                    LivingEntities--;
                    break;
                }
            }
        }
    }
}
