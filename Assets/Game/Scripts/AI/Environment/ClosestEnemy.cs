using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using System.Collections.Generic;

namespace BBUnity.Actions
{
    /// <summary>
    /// It is an action to find the closest game object with a given label.
    /// </summary>
    [Action("GameObject/ClosestEnemy")]
    [Help("Encontra o inimigo mais próximo")]
    public class ClosestEnemy : GOAction
    {
        [OutParam("foundGameObject")]
        [Help("O inimigo mais próximo")]
        public GameObject foundGameObject;

        private float elapsedTime;
        private const string TagBoss = "Boss";
        private const string TagEnemy = "Enemy";
        private List<GameObject> EnemyGameObjects = new List<GameObject>();
        public override void OnStart()
        {
            base.OnStart();
            this.EnemyGameObjects.AddRange(GameObject.FindGameObjectsWithTag(TagBoss));
            SearchClosestEnemy();
        }
        public override TaskStatus OnUpdate()
        {
            SearchClosestEnemy();
            return TaskStatus.COMPLETED;
        }
        private void SearchClosestEnemy()
        {
            float dist = float.MaxValue;
            var EnemyGameObjects = GameObject.FindGameObjectsWithTag(TagEnemy);
            if (EnemyGameObjects != null)
            {
                this.EnemyGameObjects.AddRange(EnemyGameObjects);
            }

            foreach (GameObject go in this.EnemyGameObjects)
            {
                IDamageable damageable = go.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    if (!damageable.IsDead)
                    {
                        float newdist = Vector3.Distance(go.transform.position, gameObject.transform.position);
                        if (newdist < dist)
                        {
                            dist = newdist;
                            foundGameObject = go;
                        }
                    }
                }
            }
        }
    }
}
