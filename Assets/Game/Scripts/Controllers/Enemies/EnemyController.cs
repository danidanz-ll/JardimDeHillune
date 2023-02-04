using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour, ICharacterController, IAIController
{
    [Header("Weapon")]
    [SerializeField] 
    GameObject weaponObject;
    
    [Header("Settings")]
    [SerializeField] 
    public bool IsMelee = true;

    [SerializeField]
    public string EnemyName;

    private EnemyMovement enemyMovement;
    private LifeSystem lifeSystem;
    private Damageable damageable;
    private IWeapon weapon;

    private GameObject Body;
    private GameObject Canvas;

    private EnemySpawner enemySpawner;

    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        lifeSystem = GetComponent<LifeSystem>();
        damageable = GetComponent<Damageable>();
        if (weaponObject != null)
        {
            weapon = weaponObject.GetComponent<IWeapon>();
        }

        lifeSystem.DeathEvent += OnDeath;
        lifeSystem.RessurectEvent += Resurrect;

        Body = gameObject.transform.GetChild(1).gameObject;
        Canvas = gameObject.transform.GetChild(2).gameObject;

        if (Settings.GetUserSettings())
        {
            lifeSystem.maxLife = SettingsEnemies.GetLife(EnemyName);
            lifeSystem.currentLife = SettingsEnemies.GetLife(EnemyName);
            weapon.SetDamage(SettingsEnemies.GetDamage(EnemyName));
        }
    }
    private void OnDestroy()
    {
        if (damageable != null)
        {
            lifeSystem.DeathEvent -= OnDeath;
            lifeSystem.RessurectEvent -= Resurrect;
            lifeSystem.DeathGameObjectEvent -= EnemySpawner.GiveManaToPlayer;
        }
    }
    public void StartEnemy(EnemySpawner spawner)
    {
        enemySpawner = spawner;
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
        enemySpawner.DestroyMob(gameObject);
    }
    private void Resurrect()
    {
        enemyMovement.SetBodyType(RigidbodyType2D.Dynamic);
        lifeSystem.SetFullLife();
    }
    public bool CharacterIsDead()
    {
        return lifeSystem.IsDead;
    }
    public Vector3 GetCurrentPosition()
    {
        return transform.position;
    }
}
