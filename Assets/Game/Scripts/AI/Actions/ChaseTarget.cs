using System;
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
        if (targetObject == null)
        {
            return TaskStatus.ABORTED;
        }
        else if (!targetObject.activeSelf)
        {
            return TaskStatus.ABORTED;
        }
        else if (GetControllerFromCharacter(targetObject).CharacterIsDead())
        {
            return TaskStatus.ABORTED;
        }
        else
        {
            Vector2 toTarget = targetObject.transform.position - enemyController.transform.position;
            enemyController.SetMovement(toTarget);
        }
        return TaskStatus.COMPLETED;
    }
    private ICharacterController GetControllerFromCharacter(GameObject character)
    {
        try
        {
            return character.GetComponent<ICharacterController>();
        } catch (Exception ex)
        {
            Debug.Log("[ERROR] ChaseTarget: Não foi possível recuperar controlador do alvo.");
            return new NullCharacterController();
        }
    }
}
