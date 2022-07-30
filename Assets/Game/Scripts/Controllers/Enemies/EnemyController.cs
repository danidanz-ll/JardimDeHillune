using UnityEngine;

public class EnemyController : MonoBehaviour, ICharacterController
{
    [Header("Config")]
    [SerializeField] GameObject weaponObject;

    public bool IsDead = false;
    private EnemyMovement enemyMovement;
    private LifeSystem lifeSystem;
    private IDamageable damageable;
    public IMortal mortal { get; private set; }
    private IWeapon weapon;
    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        lifeSystem = GetComponent<LifeSystem>();
        damageable = GetComponent<IDamageable>();
        mortal = GetComponent<IMortal>();
        if (weaponObject != null)
        {
            weapon = weaponObject.GetComponent<IWeapon>();
        }

        damageable.DamageEvent += OnDamage;
        mortal.DeathEvent += OnDeath;
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
    public void SetMovement(Vector2 direction)
    {
        enemyMovement.SetMovement(direction.normalized);
    }
    public float GetMovementSpeed()
    {
        return enemyMovement.moveSpeed;
    }
    public void Attack()
    {
        weapon.Attack();
        enemyMovement.FreezeMovement(weapon.GetWaitToFreezeTime(), weapon.GetAttackingTime());
    }
    private void OnDamage(float damage)
    {
        enemyMovement.StopMovement();
        lifeSystem.TakeDamage(damage);
    }
    private void OnDeath()
    {
        enemyMovement.StopMovement();
        enabled = false;
    }
    public bool CharacterIsDead()
    {
        return IsDead;
    }
}
