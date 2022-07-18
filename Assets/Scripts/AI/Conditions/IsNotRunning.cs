using BBUnity.Conditions;
using Pada1.BBCore;

[Condition("Game/Perception/IsNotRunning")]
public class IsNotRunning : GOCondition
{
    public override bool Check()
    {
        EnemyMovement enemyMovement = gameObject.GetComponent<EnemyMovement>();
        return !enemyMovement.isRunning();
    }
}
