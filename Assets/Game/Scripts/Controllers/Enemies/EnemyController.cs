using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour, ICharacterController, IAIController
{
    [Header("Weapon")]
    [SerializeField] GameObject weaponObject;
    [Header("Settings")]
    [SerializeField] public bool IsMelee = true;
    [Header("Death settings")]
    [Min(0)]
    [SerializeField] private float TimeToDisappearAfterDeath = 0;

    private EnemyMovement enemyMovement;
    private LifeSystem lifeSystem;
    private Damageable damageable;
    private IWeapon weapon;

    private GameObject Body;
    private GameObject Canvas;

    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        lifeSystem = GetComponent<LifeSystem>();
        damageable = GetComponent<Damageable>();
        if (weaponObject != null)
        {
            weapon = weaponObject.GetComponent<IWeapon>();
        }

        damageable.DeathEvent += OnDeath;
        damageable.RessurectEvent += Resurrect;

        Body = gameObject.transform.GetChild(1).gameObject;
        Canvas = gameObject.transform.GetChild(2).gameObject;
    }
    private void OnDestroy()
    {
        if (damageable != null)
        {
            damageable.DeathEvent -= OnDeath;
            damageable.RessurectEvent -= Resurrect;
        }
    }
    public void SetMovement(Vector2 direction)
    {
        if (!weapon.IsAttacking() && !damageable.IsHurting && !enemyMovement.IsFreeze)
        {
            enemyMovement.SetMovement(direction);
        }
    }
    public float GetMovementSpeed()
    {
        return enemyMovement.moveSpeed;
    }
    public void Attack()
    {
        if (!weapon.IsAttacking() && !damageable.IsHurting && !enemyMovement.IsFreeze)
        {
            weapon.Attack();
        }
    }
    public void Attack(GameObject target)
    {
        if (!weapon.IsAttacking() && !damageable.IsHurting && !enemyMovement.IsFreeze)
        {
            var targetAttack = target.transform.position;
            var originAttack = gameObject.transform.position;
            var attackDirection = targetAttack - originAttack;

            weapon.Attack(attackDirection);
        }
    }
    public void PerformAttack()
    {
        weapon.PerformAttack();
    }
    public void DisableAttack()
    {
        weapon.DisableAttack();
    }
    private void OnDeath()
    {
        enemyMovement.SetBodyType(RigidbodyType2D.Static);
        Body.SetActive(false);
        Canvas.SetActive(false);
    }
    private void Resurrect()
    {
        enemyMovement.SetBodyType(RigidbodyType2D.Dynamic);
        lifeSystem.SetFullLife();
    }
    public bool CharacterIsDead()
    {
        return damageable.IsDead;
    }
    public Vector3 GetCurrentPosition()
    {
        return transform.position;
    }
}
