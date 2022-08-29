using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using System.Collections;
using UnityEngine;

[Action("Game/FollowObjective")]
public class FollowObjective : BasePrimitiveAction
{
    [InParam("AIController")]
    public IAIController iaController;
    
    private Transform objective;
    public override void OnStart()
    {
        base.OnStart();
        objective = GameObject.FindGameObjectWithTag("Objective").transform;
    }
    public override TaskStatus OnUpdate()
    {
        Follow();
        return TaskStatus.COMPLETED;
    }
    private void Follow()
    {
        if (objective != null)
        {
            Vector2 direction = Vector2.MoveTowards(iaController.GetPosition(), objective.position, iaController.GetMovementSpeed() * Time.deltaTime) * -1.0f;
            iaController.SetMovement(direction);
        }
    }
}
