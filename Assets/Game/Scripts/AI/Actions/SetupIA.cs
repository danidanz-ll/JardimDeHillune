using BBUnity.Actions;
using Pada1.BBCore;
using UnityEngine;
using Pada1.BBCore.Tasks;

[Action("GameObject/SetupIA")]
public class SetupIA : GOAction
{
    [OutParam("AIVision")]
    public AIVision aIVision;
    [OutParam("EnemyController")]
    public EnemyController enemyController;
    public override void OnStart()
    { 
        base.OnStart();
        aIVision = gameObject.GetComponent<AIVision>();
        enemyController = gameObject.GetComponent<EnemyController>();
    }
    public override TaskStatus OnUpdate()
    {
        return TaskStatus.COMPLETED;
    }
}
