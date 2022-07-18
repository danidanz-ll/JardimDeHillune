using UnityEngine;

public class EnemyAnimationController : CharacterAnimationController
{
    protected override void Awake()
    {
        base.Awake();
        movement = GetComponent<EnemyMovement>();
    }
    protected override void Update()
    {
        base.Update();
    }
}
