using BBUnity.Actions;
using BBUnity.Conditions;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;

[Condition("Game/Perception/IsTargetCloserThanObjective")]
public class IsTargetCloserThanObjective : GOAction
{
    [InParam("Target")] private GameObject target;
    public override TaskStatus OnUpdate()
    {
        if (Check())
        {
            return TaskStatus.COMPLETED;
        }
        else
        {
            return TaskStatus.FAILED;
        }
    }
    public bool Check()
    {
        GameObject objective = GameObject.FindGameObjectWithTag("Objective");
        if (Vector3.Distance(gameObject.transform.position, target.transform.position) > Vector3.Distance(gameObject.transform.position, objective.transform.position))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}