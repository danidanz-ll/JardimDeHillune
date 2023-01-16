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
    [InParam("IsObjective")] private bool IsObjective = false;

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
                return true;
            }
        }

        return false;
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
