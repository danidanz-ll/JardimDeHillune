using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;
using System;


[Action("Game/FireProjectile")]
public class FireProjectile : BasePrimitiveAction
{
    [InParam("AIController")] public IAIController iaController;
    [InParam("TargetObject")] public GameObject targetObject;
    [InParam("IsObjective")] private bool IsObjective = false;
    public override void OnStart()
    {
        base.OnStart();
    }
    public override TaskStatus OnUpdate()
    {
        if (IsObjective)
        {
            targetObject = GameObject.FindGameObjectWithTag("Objective");
        }
        if (targetObject == null)
        {
            return TaskStatus.ABORTED;
        }
        
        try
        {
            iaController.Attack(targetObject);
        } catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            return TaskStatus.ABORTED;
        }
        return TaskStatus.COMPLETED;
    }
}
