using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;

[Action("Game/AttackTarget")]
public class AttackTarget : BasePrimitiveAction
{
    [InParam("Controller")] public EnemyController enemyController;
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
        enemyController.Attack();
        return TaskStatus.COMPLETED;
    }
}
