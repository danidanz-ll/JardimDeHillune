using UnityEngine;

public class PlayerAnimationController : CharacterAnimationController
{
    protected override void Awake()
    {
        base.Awake();
        movement = GetComponent<PlayerMovement>();
    }
    protected override void Update()
    {
        base.Update();
    }
}