using BBUnity.Actions;
using BBUnity.Conditions;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;

[Action("Game/Perception/IsTag")]
public class IsTag : GOAction
{
    [InParam("Target")] private GameObject target;
    [InParam("Tag")] private string Tag;

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
        return target.tag == Tag;
    }
}
