using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;

[Action("Game/ChaseTarget")]
public class ChaseTarget : BasePrimitiveAction
{
    [InParam("AIController")]
    public EnemyController enemyController;
    [InParam("TargetObject")]
    public GameObject targetObject;
    public override void OnStart()
    {
        base.OnStart();
    }
    public override TaskStatus OnUpdate()
    {
        Vector2 toTarget = targetObject.transform.position - enemyController.transform.position;
        enemyController.SetMovement(toTarget);
        return TaskStatus.COMPLETED;
    }
}
