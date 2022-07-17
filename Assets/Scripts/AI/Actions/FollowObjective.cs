using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;

[Action("Game/FollowObjective")]
public class FollowObjective : BasePrimitiveAction
{
    [InParam("AIController")]
    public EnemyController enemyController;
    public override void OnStart()
    {
        base.OnStart();
        Follow();
    }
    public override TaskStatus OnUpdate()
    {
        return TaskStatus.RUNNING;
    }
    private void Follow()
    {
        Debug.Log("Following objective!");
        enemyController.transform.position = Vector2.MoveTowards(enemyController.transform.position, enemyController.objectivePosition.position, enemyController.moveSpeed * Time.deltaTime);
    }
}
