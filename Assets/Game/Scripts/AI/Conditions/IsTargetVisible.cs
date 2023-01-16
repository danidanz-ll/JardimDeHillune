using BBUnity.Actions;
using BBUnity.Conditions;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;

[Action("Game/Perception/IsTargetVisible")]
public class IsTargetVisible : GOAction
{
    [InParam("Target")] private GameObject target;
    [InParam("AIVision")] private AIVision aiVision;
    [InParam("TargetMemoryDuration")] private float targetMemoryDuration;
    [InParam("ForgetTargetTime")] private float forgetTargetTime;

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
        if (IsAvailable())
        {
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
}
