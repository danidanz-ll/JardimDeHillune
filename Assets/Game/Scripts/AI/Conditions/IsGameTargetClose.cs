using BBUnity.Conditions;
using Pada1.BBCore;
using UnityEngine;

[Condition("Game/Perception/IsGameTargetClose")]
public class IsGameTargetClose : GOCondition
{
    [InParam("Target")] private GameObject target;
    [InParam("AIVision")] private AIVision aiVision;
    public override bool Check()
    {
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
