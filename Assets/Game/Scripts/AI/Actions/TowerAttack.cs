using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;
using System;


[Action("Game/TowerAttack")]
public class TowerAttack : BasePrimitiveAction
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

        if (towerController.IsMelee)
        {
            towerController.Attack();
        } else
        {
            towerController.Attack(targetObject);
        }
        return TaskStatus.COMPLETED;
    }
}
