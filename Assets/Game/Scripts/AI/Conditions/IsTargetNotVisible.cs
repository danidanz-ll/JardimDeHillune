using BBUnity.Conditions;
using Pada1.BBCore;
using UnityEngine;

[Condition("Game/Perception/IsTargetNotVisible")]
public class IsTargetNotVisible : GOCondition
{
    [InParam("Target")]
    private GameObject target;
    [InParam("AIVision")]
    private AIVision aiVision;
    [InParam("TargetMemoryDuration")]
    private float targetMemoryDuration;

    private float forgetTargetTime;
    public override bool Check()
    {
        if (aiVision.IsVisible(target))
        {
            forgetTargetTime = Time.time + targetMemoryDuration;
            return false;
        }
        return Time.time >= forgetTargetTime;
    }
}
