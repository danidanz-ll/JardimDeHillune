using System.Collections;
using UnityEngine;

public class MeleeWeapon : TriggerDamage, IWeapon
{
    [SerializeField] private float attackTime = 0.2f;
    [SerializeField] private float startAttackDamageTime = 0.2f;
    [SerializeField] private float timeToFreeze = 0.2f;
    [SerializeField] private float attackCooldown = 0f;

    private bool Attacking = false;
    private bool attackCooldownOn = false;
    private BoxCollider2D boxCollider;
    private void Awake()
    {
        gameObject.SetActive(true);
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void Attack()
    {
        if (!IsAttackInCooldown() && !IsAttacking())
        {
            StartCoroutine(StartAttackDamage());
        }
    }

    private IEnumerator PerformAttack()
    {
        Attacking = true;
        yield return new WaitForSeconds(attackTime);
        StartCoroutine(StartAttackCooldown());
        Attacking = false;
        boxCollider.enabled = false;
    }

    public bool IsAttacking()
    {
        return Attacking;
    }

    public float GetAttackingTime()
    {
        return attackTime;
    }

    public float GetWaitToFreezeTime()
    {
        return timeToFreeze;
    }

    public bool IsAttackInCooldown()
    {
        return attackCooldownOn;
    }

    public IEnumerator StartAttackCooldown()
    {
        attackCooldownOn = true;
        yield return new WaitForSeconds(attackCooldown);
        attackCooldownOn = false;
        boxCollider.enabled = true;
        gameObject.SetActive(false);
    }
    public IEnumerator StartAttackDamage()
    {
        yield return new WaitForSeconds(startAttackDamageTime);
        gameObject.SetActive(true);
        StartCoroutine(PerformAttack());
    }
}
