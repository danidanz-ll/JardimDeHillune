using BBUnity.Actions;
using BBUnity.Conditions;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;

[Action("Game/Perception/IsTargetClose")]
public class IsTargetClose : GOAction
{
    [InParam("Target")] private GameObject target;
    [InParam("AIVision")] private AIVision aiVision;
    [InParam("TargetMemoryDuration")] private float targetMemoryDuration;
    [InParam("IsObjective")] private bool IsObjective = false;
    private float forgetTargetTime = 0f;

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
    private bool Check()
    {
        if (IsObjective)
        {
            target = GameObject.FindGameObjectWithTag("Objective");
        }

        if (IsAvailable())
        {
            if (aiVision.IsDamageble(target))
            {
                forgetTargetTime = Time.time + targetMemoryDuration;
                return true;
            }
        }

        return Time.time < forgetTargetTime;
    }
    private bool IsAvailable()
    {
        if (target == null)
        {
            return false;
        }

        return true;
    }
}
