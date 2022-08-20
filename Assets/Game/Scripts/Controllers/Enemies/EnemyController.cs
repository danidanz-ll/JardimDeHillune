using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour, ICharacterController
{
    [Header("Weapon")]
    [SerializeField] GameObject weaponObject;
    [Header("Death settings")]
    [Min(0)]
    [SerializeField] private float TimeToDisappearAfterDeath = 0;

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

        mortal.DeathEvent += OnDeath;
        mortal.RessurectEvent += Resurrect;
    }
    private void OnDestroy()
    {
        if (mortal != null)
        {
            mortal.DeathEvent -= OnDeath;
            mortal.RessurectEvent -= Resurrect;
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
    private void OnDeath()
    {
        enemyMovement.SetBodyType(RigidbodyType2D.Static);
        //StartCoroutine(DisappearAfterDeath());
    }
    private void Resurrect()
    {
        enemyMovement.SetBodyType(RigidbodyType2D.Dinamic);
    }
    public bool CharacterIsDead()
    {
        return mortal.IsDead;
    }
    private IEnumerator DisappearAfterDeath()
    {
        //enabled = false;
        yield return new WaitForSeconds(TimeToDisappearAfterDeath);
    }
}
