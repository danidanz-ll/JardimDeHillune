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
    [Header("Sound")]
    [SerializeField] public AudioSource AudioHurting;
    [SerializeField] public AudioSource AudioAttacking;
    [SerializeField] public AudioSource AudioRunning;
    [SerializeField] public AudioSource AudioDeath;

    private SpriteRenderer spriteRenderer;
    private IDamageable damageable;
    public IMortal deathOnDamage;
    private IWeapon weapon;
    protected IMovement movement;
    protected Animator animator;
    public bool dead = false;
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
        if (weapon != null)
        {
            weapon.AttackEvent += Attack;
        }
    }
    protected virtual void Update()
    {
        if (dead) return;
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
            if (weapon != null)
            {
                weapon.SetDirectionWeapon(true);
            }
        }
        else
        {
            spriteRenderer.flipX = true;
            if (weapon != null)
            {
                weapon.SetDirectionWeapon(false);
            }
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
        if (weapon != null)
        {
            weapon.AttackEvent -= Attack;
        }
    }
    private void Attack()
    {
        if (AudioAttacking != null)
        {
            AudioAttacking.Play();
        }
        animator.SetTrigger(CharacterMovementAnimationKeys.IsAttacking);
    }
    private void OnDamage()
    {
        if (AudioHurting != null)
        {
            AudioHurting.Play();
        }
        animator.SetTrigger(CharacterMovementAnimationKeys.IsHurting);
    }
    private void OnDeath()
    {
        if (AudioDeath != null)
        {
            AudioDeath.Play();
        }
        animator.SetTrigger(CharacterMovementAnimationKeys.IsDead);
        dead = true;
        enabled = false;
    }
}
