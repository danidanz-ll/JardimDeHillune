using UnityEngine;

public static class CharacterMovementAnimationKeys
{
    public const string IsRunning = "isRunning";
    public const string Speed = "Speed";
}
public class CharacterAnimationController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected IMovement movement;
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected virtual void Update()
    {
        animator.SetFloat(CharacterMovementAnimationKeys.Speed, movement.GetCurrentVelocityNormalized());

        if (movement.isLookingToRight())
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
}
