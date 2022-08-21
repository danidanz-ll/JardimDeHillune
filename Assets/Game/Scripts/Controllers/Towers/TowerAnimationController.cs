using System.Collections;
using UnityEngine;

public static class TowerAnimationKeys
{
    public const string IsHurting = "isHurting";
    public const string IsAttacking = "isAttacking";
    public const string IsDead = "isDead";
    public const string IsResurrect = "isResurrect";
}
public class TowerAnimationController : MonoBehaviour
{
    [SerializeField] private float timeAttackDuration = 0;
    
    private SpriteRenderer spriteRenderer;
    private IDamageable damageable;
    public IMortal deathOnDamage;
    private IWeapon weapon;
    private bool AttackingAnimationIsOn = false;
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
            deathOnDamage.RessurectEvent += Resurrect;
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
                animator.SetTrigger(TowerAnimationKeys.IsAttacking);
                StartCoroutine(AttackingAnimationTime());
            }
        }
    }
    private void FixedUpdate() 
    {
        if (dead) return;
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
            deathOnDamage.RessurectEvent -= Resurrect;
        }
    }
    private void OnDamage()
    {
        animator.SetTrigger(TowerAnimationKeys.IsHurting);
    }
    private void OnDeath()
    {
        animator.SetTrigger(TowerAnimationKeys.IsDead);
        dead = true;
        enabled = false;
    }
    private void Resurrect()
    {
        dead = false;
        enabled = true;
        animator.SetTrigger(TowerAnimationKeys.IsResurrect);
    }
    private IEnumerator AttackingAnimationTime()
    {
        yield return new WaitForSeconds(timeAttackDuration);
        AttackingAnimationIsOn = false;
    }
}
