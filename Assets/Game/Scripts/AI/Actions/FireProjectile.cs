using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;

[Action("Game/FireProjectile")]
public class FireProjectile : BasePrimitiveAction
{
    [InParam("AIController")] public TowerController towerController;
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
            towerController.Attack(targetObject);
        } catch (Exception ex)
        {
            return TaskStatus.ABORTED;
        }
        return TaskStatus.COMPLETED;
    }
}
