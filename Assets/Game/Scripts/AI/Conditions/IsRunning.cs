using BBUnity.Conditions;
using Pada1.BBCore;

[Condition("Game/Perception/IsRunning")]
public class IsRunning : GOCondition
{
    public override bool Check()
    {
        EnemyMovement enemyMovement = gameObject.GetComponent<EnemyMovement>();
        return enemyMovement.isRunning();
    }
}
