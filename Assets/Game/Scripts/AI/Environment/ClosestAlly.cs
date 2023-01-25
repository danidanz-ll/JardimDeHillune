using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using System.Collections.Generic;

namespace BBUnity.Actions
{
    /// <summary>
    /// It is an action to find the closest game object with a given label.
    /// </summary>
    [Action("GameObject/ClosestAlly")]
    [Help("Encontra o aliado mais próximo")]
    public class ClosestAlly : GOAction
    {
        [OutParam("foundGameObject")]
        [Help("O aliado mais próximo")]
        public GameObject foundGameObject;

        private float elapsedTime;
        private const string TagPlayer = "Player";
        private const string TagAlly = "Ally";
        private const string TagObjective = "Objective";
        private List<GameObject> AlliesGameObject = new List<GameObject>();
        public override void OnStart()
        {
            base.OnStart();
            SearchClosestAlly();
        }
        public override TaskStatus OnUpdate()
        {
            SearchClosestAlly();
            return TaskStatus.COMPLETED;
        }
        private void SearchClosestAlly()
        {
            float dist = float.MaxValue;
            AlliesGameObject.AddRange(GameObject.FindGameObjectsWithTag(TagPlayer));
            AlliesGameObject.AddRange(GameObject.FindGameObjectsWithTag(TagObjective));
            var TowersGameObject = GameObject.FindGameObjectsWithTag(TagAlly);
            if (TowersGameObject != null)
            {
                AlliesGameObject.AddRange(TowersGameObject);
            }

            foreach (GameObject go in AlliesGameObject)
            {
                LifeSystem lifeSystem = go.GetComponent<LifeSystem>();
                if (lifeSystem != null)
                {
                    if (!lifeSystem.IsDead)
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
