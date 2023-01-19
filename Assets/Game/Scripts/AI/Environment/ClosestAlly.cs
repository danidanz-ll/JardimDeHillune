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
    [Help("Finds the closest game object with a given tag")]
    public class ClosestAlly : GOAction
    {
        [OutParam("foundGameObject")]
        [Help("The closest game object with the given tag")]
        public GameObject foundGameObject;

        private float elapsedTime;
        private const string TagPlayer = "Player";
        private const string TagAlly = "Ally";
        private const string TagObjective = "Objective";
        private List<GameObject> AlliesGameObject = new List<GameObject>();
        public override void OnStart()
        {
            base.OnStart();
            float dist = float.MaxValue;
            AlliesGameObject.AddRange(GameObject.FindGameObjectsWithTag(TagPlayer));
            AlliesGameObject.AddRange(GameObject.FindGameObjectsWithTag(TagObjective));
            var TowersGameObject = GameObject.FindGameObjectsWithTag(TagAlly);
            if(TowersGameObject != null)
            {
                AlliesGameObject.AddRange(TowersGameObject);
            }

            foreach (GameObject go in AlliesGameObject)
            {
                float newdist = Vector3.Distance(go.transform.position, gameObject.transform.position);
                if (newdist < dist)
                {
                    dist = newdist;
                    foundGameObject = go;
                }
            }
        }
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}
