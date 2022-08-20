using UnityEngine;

public class EnemyAnimationController : CharacterAnimationController
{
    protected override void Awake()
    {
        base.Awake();
        movement = GetComponent<EnemyMovement>();
        deathOnDamage.RessurectEvent += Resurrect;
    }
    protected override void Update()
    {
        base.Update();
    }
    private void Resurrect()
    {
        animator.SetTrigger(CharacterMovementAnimationKeys.IsResurrect);
    }
}
