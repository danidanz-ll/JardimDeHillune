using UnityEngine;

public static class CharacterMovementAnimationKeys
{
    public const string IsRunning = "isRunning";
    public const string Speed = "Speed";
    public const string IsHurting = "IsHurting";
}
public class CharacterAnimationController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected IMovement movement;
    private IDamageable damageable;
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        damageable = GetComponent<IDamageable>();
        if (damageable != null)
        {
            //damageable.DeathEvent += OnDeath;
        }
    }
    protected virtual void Update()
    {
        animator.SetFloat(CharacterMovementAnimationKeys.Speed, movement.GetCurrentVelocityNormalized());
        animator.SetBool(CharacterMovementAnimationKeys.IsHurting, false);

        if (movement.isLookingToRight())
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
    private void OnDestroy()
    {
        if (damageable != null)
        {
            //damageable.DeathEvent -= OnDeath;
        }
    }
    private void OnDeath()
    {
        animator.SetTrigger(CharacterMovementAnimationKeys.IsHurting);
    }
}
