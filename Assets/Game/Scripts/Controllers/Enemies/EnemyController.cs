using UnityEngine;
using System;

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

        mortal.DeathEvent += OnDeath;
    }
    private void OnDestroy()
    {
        if (mortal != null)
        {
            mortal.DeathEvent -= OnDeath;
        }
    }
    public void SetMovement(Vector2 direction)
    {
        try
        {
            enemyMovement.SetMovement(direction.normalized);
        }
        catch(Exception e)
        {
            Debug.Log("Erroaqui");
        }
        
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
        enabled = false;
    }
    public bool CharacterIsDead()
    {
        return IsDead;
    }
}
