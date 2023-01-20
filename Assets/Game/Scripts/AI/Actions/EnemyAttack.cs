using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;

[Action("Game/EnemyAttack")]
public class EnemyAttack : BasePrimitiveAction
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
        if (enemyController.IsMelee)
        {
            enemyController.Attack();
        } else
        {
            enemyController.Attack(targetObject);
        }
        return TaskStatus.COMPLETED;
    }
}
