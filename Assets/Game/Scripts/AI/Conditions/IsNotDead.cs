using BBUnity.Actions;
using BBUnity.Conditions;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;

[Action("Game/Perception/IsNotDead")]
public class IsNotDead : GOAction
{
    public override TaskStatus OnUpdate()
    {
        if (Check())
        {
            return TaskStatus.COMPLETED;
        }
        else
        {
            return TaskStatus.FAILED;
        }
    }
    public bool Check()
    {
        ICharacterController characterController = gameObject.GetComponent<ICharacterController>();
        return !characterController.CharacterIsDead();
    }
}