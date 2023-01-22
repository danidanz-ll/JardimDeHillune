using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;

[Action("Game/Perception/IsPlantGrew")]
public class IsPlantGrew : GOAction
{
    private TowerController towerController;
    public override void OnStart()
    {
        base.OnStart();
        towerController = gameObject.GetComponent<TowerController>();
    }
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
        return towerController.IsPLantGrew;
    }
}