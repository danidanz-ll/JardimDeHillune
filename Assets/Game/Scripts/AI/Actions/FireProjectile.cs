using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;
using System;


[Action("Game/FireProjectile")]
public class FireProjectile : BasePrimitiveAction
{
    [InParam("TowerController")] public TowerController towerController;
    [InParam("TargetObject")] public GameObject targetObject;
    public override void OnStart()
    {
        base.OnStart();
    }
    public override TaskStatus OnUpdate()
    {
        if (targetObject == null)
        {
            return TaskStatus.ABORTED;
        }
        
        try
        {
            towerController.Attack(targetObject);
        } catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            return TaskStatus.ABORTED;
        }
        return TaskStatus.COMPLETED;
    }
}
