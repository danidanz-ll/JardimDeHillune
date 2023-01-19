using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;

[Action("GameObject/SetupTower")]
public class SetupTower : GOAction
{
    [OutParam("AIVision")]
    public AIVision aIVision;
    [OutParam("TowerController")]
    public TowerController towerController;
    public override void OnStart()
    { 
        base.OnStart();
        aIVision = gameObject.GetComponent<AIVision>();
        towerController = gameObject.GetComponent<TowerController>();
    }
    public override TaskStatus OnUpdate()
    {
        return TaskStatus.COMPLETED;
    }
}
