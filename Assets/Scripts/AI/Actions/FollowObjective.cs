using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using System.Collections;
using UnityEngine;

[Action("Game/FollowObjective")]
public class FollowObjective : BasePrimitiveAction
{
    [InParam("AIController")]
    public EnemyController enemyController;
    [InParam("Objective")]
    public GameObject objectiveGameObject;
    
    private Transform objective;
    public override void OnStart()
    {
        base.OnStart();
        objective = objectiveGameObject.transform;
    }
    public override TaskStatus OnUpdate()
    {
        Follow();
        return TaskStatus.COMPLETED;
    }
    private void Follow()
    {
        Vector2 direction = Vector2.MoveTowards(enemyController.transform.position, objective.position, enemyController.enemyMovement.moveSpeed * Time.deltaTime) * -1.0f;
        enemyController.enemyMovement.SetMovement(direction.normalized);
    }
}
