using BBUnity.Conditions;
using Pada1.BBCore;
using UnityEngine;

[Condition("Game/Perception/IsTargetVisible")]
public class IsTargetVisible : GOCondition
{
    [InParam("Target")] private GameObject target;
    [InParam("AIVision")] private AIVision aiVision;
    [InParam("TargetMemoryDuration")] private float targetMemoryDuration;
    [InParam("ForgetTargetTime")] private float forgetTargetTime;
    public override bool Check()
    {
        if (IsAvailable())
        {
            if (IsObjective()) return true;

            if (aiVision.IsVisible(target))
            {
                aiVision.forgetTargetTime = Time.time + targetMemoryDuration;
                return true;
            }
        }
        //Debug.Log(aiVision.forgetTargetTime.ToString());
        return Time.time < aiVision.forgetTargetTime;
    }
    private bool IsAvailable()
    {
        if (target == null)
        {
            return false;
        }
        return true;
    }
    private bool IsObjective()
    {
        if (target.tag == "Objective")
        {
            return true;
        }
        return false;
    }
}
