using UnityEngine;

public static class CharacterMovementAnimationKeys
{
    public const string IsRunning = "isRunning";
    public const string Speed = "Speed";
    public const string IsHurting = "isHurting";
    public const string IsDashing = "isDashing";
    public const string IsAttacking = "isAttacking";
}
public class CharacterAnimationController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    protected Animator animator;
    private ICharacterController characterController;
    protected IMovement movement;
    private IDamageable damageable;
    private IWeapon weapon;
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        damageable = GetComponent<IDamageable>();
        weapon = GetComponentInChildren<IWeapon>();

        if (damageable != null)
        {
            damageable.DamageEvent += OnDamage;
        }
    }
    protected virtual void Update()
    {
        animator.SetFloat(CharacterMovementAnimationKeys.Speed, movement.GetCurrentVelocityNormalized());
        if (weapon != null)
        {
            animator.SetBool(CharacterMovementAnimationKeys.IsAttacking, weapon.IsAttacking());
        }

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
            damageable.DamageEvent -= OnDamage;
        }
    }
    private void OnDamage()
    {
        animator.SetTrigger(CharacterMovementAnimationKeys.IsHurting);
    }
}
