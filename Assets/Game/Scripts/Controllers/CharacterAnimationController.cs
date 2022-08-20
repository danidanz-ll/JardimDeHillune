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
    public const string IsResurrect = "isResurrect";
}
public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] private float timeAttackDuration = 0;
    
    private SpriteRenderer spriteRenderer;
    private IDamageable damageable;
    public IMortal deathOnDamage;
    private IWeapon weapon;
    private bool AttackingAnimationIsOn = false;
    protected IMovement movement;
    protected Animator animator;
    private bool dead = false;
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        damageable = GetComponent<IDamageable>();
        deathOnDamage = GetComponent<IMortal>();
        
        weapon = GetComponentInChildren<IWeapon>();

        if (damageable != null)
        {
            damageable.DamageEvent += OnDamage;
        }
        if (deathOnDamage != null)
        {
            deathOnDamage.DeathEvent += OnDeath;
        }
    }
    protected virtual void Update()
    {
        if (dead) return;

        if (weapon != null)
        {
            if (weapon.IsAttacking() && !weapon.IsAttackInCooldown() && !AttackingAnimationIsOn)
            {
                AttackingAnimationIsOn = true;
                animator.SetTrigger(CharacterMovementAnimationKeys.IsAttacking);
                StartCoroutine(AttackingAnimationTime());
            }
        }
    }
    private void FixedUpdate() 
    {
        if (dead) return;

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
        if (deathOnDamage != null)
        {
            deathOnDamage.DeathEvent -= OnDeath;
        }
    }
    private void OnDamage()
    {
        animator.SetTrigger(CharacterMovementAnimationKeys.IsHurting);
    }
    private void OnDeath()
    {
        animator.SetTrigger(CharacterMovementAnimationKeys.IsDead);
        dead = true;
        enabled = false;
    }
    private IEnumerator AttackingAnimationTime()
    {
        yield return new WaitForSeconds(timeAttackDuration);
        AttackingAnimationIsOn = false;
    }
}
