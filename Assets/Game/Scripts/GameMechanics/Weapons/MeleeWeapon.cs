using System.Collections;
using UnityEngine;
using System;

public class MeleeWeapon : TriggerDamage, IWeapon
{
    [SerializeField] private float attackCooldown = 0f;

    public event Action AttackEvent;

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
        boxCollider.enabled = false;
        //gameObject.SetActive(false);
    }
    public void Attack()
    {
        if (!IsAttackInCooldown() && !IsAttacking())
        {
            Attacking = true;
            AttackEvent.Invoke();
        }
    }
    public void Attack(Vector2 direction)
    {
        if (!IsAttackInCooldown() && !IsAttacking())
        {
            AttackEvent.Invoke();
        }
    }
    public void PerformAttack()
    {
        boxCollider.enabled = true;
    }
    public void DisableAttack()
    {
        StartCoroutine(StartAttackCooldown());
        Attacking = false;
        boxCollider.enabled = false;
    }

    public bool IsAttacking()
    {
        return Attacking;
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

    public void SetDirectionWeapon(bool right)
    {
        if (right)
        {
            gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        else
        {
            gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
        }
    }
}
