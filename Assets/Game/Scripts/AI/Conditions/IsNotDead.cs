using BBUnity.Actions;
using BBUnity.Conditions;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;

[Condition("Game/Perception/IsNotDead")]
public class IsNotDead : GOAction
{
    public override TaskStatus OnUpdate()
    {
        Debug.Log("Call IsNotDead");
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
        Debug.Log("Is character dead: " + characterController.CharacterIsDead());
        return !characterController.CharacterIsDead();
    }
}