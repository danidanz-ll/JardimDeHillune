using UnityEngine;

public class EnemyController : MonoBehaviour, ICharacterController
{
    [Header("Stats")]
    [SerializeField] private float damage;
    [SerializeField] private float playerVisionRay;

    private EnemyMovement enemyMovement;
    private LifeSystem lifeSystem;
    private IDamageable damageable;
    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        lifeSystem = GetComponent<LifeSystem>();
        damageable = GetComponent<IDamageable>();

        damageable.DamageEvent += OnDamage;
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
    private void OnDamage()
    {
        enemyMovement.StopMovement();
        lifeSystem.TakeDamage(10);
    }
}
