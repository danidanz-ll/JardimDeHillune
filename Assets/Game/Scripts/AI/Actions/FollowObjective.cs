using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using System.Collections;
using UnityEngine;
using System;

[Action("Game/FollowObjective")]
public class FollowObjective : BasePrimitiveAction
{
    [InParam("AIController")]
    public IAIController iaController;
    
    private Transform objective;
    public override void OnStart()
    {
        base.OnStart();
        objective = GameObject.FindGameObjectWithTag("Objective").transform;
    }
    public override TaskStatus OnUpdate()
    {
        Follow();
        return TaskStatus.COMPLETED;
    }
    private void Follow()
    {
        
        if (objective != null)
        {
<<<<<<< Updated upstream
            Vector2 direction = Vector2.MoveTowards(iaController.GetPosition(), objective.position, iaController.GetMovementSpeed() * Time.deltaTime) * -1.0f;
            iaController.SetMovement(direction);
=======
            try
            {
                Vector2 direction = Vector2.MoveTowards(enemyController.transform.position, objective.position, enemyController.GetMovementSpeed() * Time.deltaTime) * -1.0f;
                enemyController.SetMovement(direction);
            }
            catch(Exception ex)
            {
                if (enemyController == null)
                {
                    Debug.Log("Enemy controler null");
                }
                if (objective == null)
                {
                    Debug.Log("Objective null");
                }
                Debug.Log("Ex:" + ex.ToString());
            }
            
>>>>>>> Stashed changes
        }
    }
}
