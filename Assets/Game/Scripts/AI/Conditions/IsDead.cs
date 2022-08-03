using BBUnity.Conditions;
using Pada1.BBCore;

[Condition("Game/Perception/IsDead")]
public class IsNotRunning : GOCondition
{
    public override bool Check()
    {
        ICharacterController characterController = gameObject.GetComponent<ICharacterController>();
        return characterController.CharacterIsDead();
    }
}