using System.Collections;
using UnityEngine;

public static class CharacterMovementAnimationKeys
{
    public const string IsRunning = "isRunning";
    public const string Speed = "Speed";
    public const string IsHurting = "isHurting";
    public const string IsDashing = "isDashing";
    public const string IsAttacking = "isAttacking";
    public const string IsDead = "isDead";
}
public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField]
    private float timeAttackDuration = 0;
    private SpriteRenderer spriteRenderer;
    protected Animator animator;
    private ICharacterController characterController;
    protected IMovement movement;
    private IDamageable damageable;
    private IWeapon weapon;
    private bool AttackingAnimationIsOn = false;
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        damageable = GetComponent<IDamageable>();
        weapon = GetComponentInChildren<IWeapon>();
        characterController = GetComponent<ICharacterController>();

        if (damageable != null)
        {
            damageable.DamageEvent += OnDamage;
        }
    }
    protected virtual void Update()
    {
        if (characterController.CharacterIsDead())
        {
            animator.SetTrigger(CharacterMovementAnimationKeys.IsDead);
            return;
        }
        if (weapon != null)
        {
            if (weapon.IsAttacking() && !weapon.IsAttackInCooldown() && !AttackingAnimationIsOn)
            {
                AttackingAnimationIsOn = true;
                animator.SetTrigger(CharacterMovementAnimationKeys.IsAttacking);
                StartCoroutine(AttackingAnimationTime());
            }
        }
        
        if (movement.isFreezing())
        {
            return;
        }
        
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
    private IEnumerator AttackingAnimationTime()
    {
        yield return new WaitForSeconds(timeAttackDuration);
        AttackingAnimationIsOn = false;
    }
}
