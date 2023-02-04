using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;

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