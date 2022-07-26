using UnityEngine;

public class EnemyController : MonoBehaviour, ICharacterController
{
    [Header("Config")]
    [SerializeField]
    GameObject weaponObject;

    public bool IsDead = false;
    private EnemyMovement enemyMovement;
    private LifeSystem lifeSystem;
    private IDamageable damageable;
    private IWeapon weapon;
    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        lifeSystem = GetComponent<LifeSystem>();
        damageable = GetComponent<IDamageable>();
        if (weaponObject != null)
        {
            weapon = weaponObject.GetComponent<IWeapon>();
        }

        damageable.DamageEvent += OnDamage;
    }

    private void Update()
    {
        if (lifeSystem.currentLife <= 0)
        {
            IsDead = true;
            OnDeath();
        }
    }
    private void OnDestroy()
    {
        if (damageable != null)
        {
            damageable.DamageEvent -= OnDamage;
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
    private void OnDamage()
    {
        enemyMovement.StopMovement();
        lifeSystem.TakeDamage(10);
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
