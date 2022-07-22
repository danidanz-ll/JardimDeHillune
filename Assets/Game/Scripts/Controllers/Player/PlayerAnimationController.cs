using UnityEngine;

public class PlayerAnimationController : CharacterAnimationController
{
    private Dash dash;

    protected override void Awake()
    {
        base.Awake();
        movement = GetComponent<PlayerMovement>();
        dash = GetComponent<Dash>();
        
    }
    protected override void Update()
    {
        base.Update();
        animator.SetBool(CharacterMovementAnimationKeys.IsDashing, dash.isRunning());
    }
}