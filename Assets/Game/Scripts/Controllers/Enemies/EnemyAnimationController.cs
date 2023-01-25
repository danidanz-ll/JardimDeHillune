using UnityEngine;

public class EnemyAnimationController : CharacterAnimationController
{
    protected override void Awake()
    {
        base.Awake();
        movement = GetComponent<EnemyMovement>();
        lifeSystem.RessurectEvent += Resurrect;
    }
    protected override void Update()
    {
        base.Update();
    }
    private void Resurrect()
    {
        dead = false;
        enabled = true;
        animator.SetTrigger(CharacterMovementAnimationKeys.IsResurrect);
    }
}
