using BBUnity.Actions;
using Pada1.BBCore;
using UnityEngine;

[Action("GameObject/SetupIA")]
public class SetupIA : GOAction
{
    [OutParam("AIVision")]
    public AIVision AIVision;
    [OutParam("AIController")]
    public IAIController AIController;
    public override void OnStart()
    { 
        base.OnStart();
        AIVision = gameObject.GetComponent<AIVision>();
        AIController = gameObject.GetComponent<IAIController>();
    }
}
