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

        damageable.DeathEvent += OnDeath;
        damageable.RessurectEvent += Resurrect;
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
        enemyMovement.SetMovement(direction);
    }
    public float GetMovementSpeed()
    {
        return enemyMovement.moveSpeed;
    }
    public void Attack()
    {
        weapon.Attack();
        enemyMovement.FreezeMovement(0, weapon.GetAttackingTime());
    }
    public void Attack(GameObject target)
    {
        enemyMovement.FreezeMovement(0, weapon.GetAttackingTime());
        var targetAttack = target.transform.position.normalized;
        var gameObjectAttack = gameObject.transform.position.normalized;

        if(targetAttack == null)
        {
            return;
        }
        weapon.Attack(target.transform.position.normalized - gameObject.transform.position.normalized);
    }
    private void OnDeath()
    {
        enemyMovement.SetBodyType(RigidbodyType2D.Static);
        //StartCoroutine(DisappearAfterDeath());
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
    private IEnumerator DisappearAfterDeath()
    {
        //enabled = false;
        yield return new WaitForSeconds(TimeToDisappearAfterDeath);
    }
    public Vector3 GetCurrentPosition()
    {
        Debug.Log($"Returning current position.");
        Debug.Log($"Current position: {transform.position}");
        return transform.position;
    }
}
