using System.Collections;
using UnityEngine;
using System;

public class ProjectileWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject projectileGameObject;
    [SerializeField][Min(0)] private float damageProjectiles = 0;
    [SerializeField][Min(0)] private float MoveSpeed = 10;
    [SerializeField] private float attackTime = 0.2f;
    [SerializeField] private float startAttackDamageTime = 0.2f;
    [SerializeField] private float timeToFreeze = 0.2f;
    [SerializeField] private float attackCooldown = 0f;
    public event Action AttackEvent;

    private bool Attacking = false;
    private bool attackCooldownOn = false;
    private void Awake()
    {
        gameObject.SetActive(true);
    }
    private void Start()
    {

    }
    public void PerformAttack()
    {
        Attacking = true;
    }
    public void DisableAttack()
    {
        StartCoroutine(StartAttackCooldown());
        Attacking = false;
    }
    public void Attack()
    {
        if (!IsAttackInCooldown() && !IsAttacking())
        {
            StartCoroutine(StartAttackDamage(new Vector2(0, 0)));
        }
    }
    public void Attack(Vector2 direction)
    {
        if (!IsAttackInCooldown() && !IsAttacking())
        {
            StartCoroutine(StartAttackDamage(direction));
        }
    }
    public IEnumerator StartAttackDamage(Vector2 direction)
    {
        Attacking = true;
        AttackEvent.Invoke();
        yield return new WaitForSeconds(startAttackDamageTime);
        StartCoroutine(PerformAttack(direction));
    }
    private IEnumerator PerformAttack(Vector2 direction)
    {
        FireProjectile(direction);
        yield return new WaitForSeconds(attackTime);
        StartCoroutine(StartAttackCooldown());
        Attacking = false;
    }
    private void FireProjectile(Vector3 direction)
    {
        GameObject projectileCreated = Instantiate(projectileGameObject, new Vector3(0, 0, 0), Quaternion.identity);
        projectileCreated.transform.position = transform.position;
        projectileCreated.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectilesParent").transform);
        Projectile projectile = projectileCreated.GetComponent<Projectile>();
        projectile.Damage = damageProjectiles;
        projectile.direction = direction.normalized;
        projectile.MoveSpeed = MoveSpeed;
        projectile.Origin = transform.parent.gameObject.tag;
        projectile.Fire();
    }
    public bool IsAttacking()
    {
        return Attacking;
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

    public void SetDamage(float damage)
    {
        damageProjectiles = damage;
    }
}
