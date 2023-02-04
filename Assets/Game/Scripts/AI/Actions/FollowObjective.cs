using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using System.Collections;
using UnityEngine;
using System;
using BBUnity.Actions;

[Action("Game/FollowObjective")]
public class FollowObjective : BasePrimitiveAction
{
    [InParam("AIController")]
    public IAIController iaController;
    
    private Transform objective;
    private Vector3 Position;
    private float VelocityNormalized;
    public override void OnStart()
    {
        base.OnStart();
        Debug.Log("Started Follow Objective!");
        objective = GameObject.FindGameObjectWithTag("Objective").transform;
    }
    public override TaskStatus OnUpdate()
    {
        //Debug.Log("Update Follow Objective");
        Debug.Log($"IA Controller: {iaController}");
        GetCurrentInfo();
        Follow();
        return TaskStatus.COMPLETED;
    }
    private void Follow()
    {
        if (objective != null)
        {
            Debug.Log("Obejctive is not null!");
            Vector2 direction = Vector2.MoveTowards(Position, objective.position, VelocityNormalized) * -1.0f;
            Debug.Log($"Objective: {direction}");
            iaController.SetMovement(direction);
        }
        else
        {
            Debug.Log("Objective is null!");
        }
    }
    private void GetCurrentInfo()
    {
        Debug.Log("Getting current position");
        Position = iaController.GetCurrentPosition();
        Debug.Log($"Current position: {Position}");
        Debug.Log("Getting current veloity");
        VelocityNormalized = iaController.GetMovementSpeed() * Time.deltaTime;
        Debug.Log($"Current veloity: {VelocityNormalized}");
    }
}
