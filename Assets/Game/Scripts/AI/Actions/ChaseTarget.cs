using System;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;

[Action("Game/ChaseTarget")]
public class ChaseTarget : BasePrimitiveAction
{
    [InParam("AIController")] public IAIController iaController;
    [InParam("TargetObject")] public GameObject targetObject;
    public override void OnStart()
    {
        base.OnStart();
    }
    public override TaskStatus OnUpdate()
    {
        if (targetObject == null)
        {
            Debug.Log("Target Nulo");
            return TaskStatus.ABORTED;
        }
        else if (!targetObject.activeSelf)
        {
            Debug.Log("Target Desativado");
            return TaskStatus.ABORTED;
        }
        else if (GetControllerFromCharacter(targetObject).CharacterIsDead())
        {
            Debug.Log("Target Esta morto");
            return TaskStatus.ABORTED;
        }
        else
        {
            Vector2 toTarget = targetObject.transform.position - iaController.GetPosition();
            iaController.SetMovement(toTarget);
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
            Debug.Log("[ERROR] ChaseTarget: Não foi possível recuperar controlador do alvo. (" + ex.ToString() + ")");
            return new NullCharacterController();
        }
    }
}
