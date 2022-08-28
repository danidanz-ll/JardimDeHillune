using UnityEngine;

public class BossAnimationController : CharacterAnimationController
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
        dead = false;
        enabled = true;
        animator.SetTrigger(CharacterMovementAnimationKeys.IsResurrect);
    }
}
