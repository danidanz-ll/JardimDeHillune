using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;

[Action("Game/AttackTarget")]
public class AttackTarget : BasePrimitiveAction
{
    [InParam("AIController")] public EnemyController enemyController;
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
        enemyController.Attack();
        return TaskStatus.COMPLETED;
    }
}
