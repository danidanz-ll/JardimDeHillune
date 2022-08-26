using System.Collections;
using UnityEngine;
using System;

public class MeleeWeapon : TriggerDamage, IWeapon
{
    [SerializeField] private float attackTime = 0.2f;
    [SerializeField] private float startAttackDamageTime = 0.2f;
    [SerializeField] private float attackCooldown = 0f;

    public event Action AttackEvent;

    private bool Attacking = false;
    private bool attackCooldownOn = false;
    private BoxCollider2D boxCollider;
    public float GetAttackTime()
    {
        return attackTime;
    }
    private void Awake()
    {
        gameObject.SetActive(true);
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        boxCollider.enabled = false;
        //gameObject.SetActive(false);
    }
    public void Attack()
    {
        if (!IsAttackInCooldown() && !IsAttacking())
        {
            AttackEvent.Invoke();
            StartCoroutine(StartAttackDamage());
        }
    }
    public void Attack(Vector2 direction)
    {
        if (!IsAttackInCooldown() && !IsAttacking())
        {
            StartCoroutine(StartAttackDamage());
        }
    }
    private IEnumerator PerformAttack()
    {
        boxCollider.enabled = true;
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

    public bool IsAttackInCooldown()
    {
        return attackCooldownOn;
    }

    public IEnumerator StartAttackCooldown()
    {
        attackCooldownOn = true;
        yield return new WaitForSeconds(attackCooldown);
        attackCooldownOn = false;
    }

    public IEnumerator StartAttackDamage()
    {
        Attacking = true;
        yield return new WaitForSeconds(startAttackDamageTime);
        StartCoroutine(PerformAttack());
    }

    public void SetDirectionWeapon(bool right)
    {
        if (right)
        {
            transform.rotation.z = 0.0f;
        }
        else
        {
            transform.rotation.z = 180.0f;
        }
    }
}
