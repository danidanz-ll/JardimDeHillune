using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent (typeof(PlayerInput))]
[RequireComponent(typeof(Dash))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(IDamageable))]
[RequireComponent(typeof(IMortal))]
[RequireComponent(typeof(LifeSystem))]
public class PlayerController : MonoBehaviour, ICharacterController
{
    [Header("Weapon")]
    [SerializeField] GameObject weaponObject;

    public bool IsDead = false;
    public IDamageable damageable { get; private set; };
    public IMortal deathOnDamage { get; private set; };
    private PlayerMovement playerMovement;
    private Dash dash;
    private PlayerInput playerInput;
    private LifeSystem lifeSystem;
    private IWeapon weapon;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        dash = GetComponent<Dash>();
        playerInput = GetComponent<PlayerInput>();
        damageable = GetComponent<IDamageable>();
        damageable = GetComponent<IMortal>();
        lifeSystem = GetComponent<LifeSystem>();

        if (weaponObject != null)
        {
            weapon = weaponObject.GetComponent<IWeapon>();
        }

        damageable.DamageEvent += OnDamage;
        deathOnDamage.DeathEvent += OnDeath;
    }

    private void Update()
    {
        if (weapon.IsAttacking())
        {
            playerMovement.FreezeMovement(weapon.GetWaitToFreezeTime(), weapon.GetAttackingTime());
            return;
        }
        Vector2 movementInupt = playerInput.GetMovementInput();

        #region Dash
        if (playerInput.IsDashingButtonDown() && dash.isAvailable())
        {
            dash.StartDashing(movementInupt);
        }

        if (dash.isRunning())
        {
            dash.ContinueDashing();
            return;
        }
        #endregion

        #region Run
        playerMovement.SetMovement(movementInupt);
        #endregion

        if (weapon != null && playerInput.IsAttackButtonDown())
        {
            weapon.Attack();
        }
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

    public bool CharacterIsDead()
    {
        return IsDead;
    }
    private void OnDeath()
    {
        playerMovement.StopMovement();
        enabled = false;
    }
    private void OnDamage(float damage)
    {
        playerMovement.StopMovement();
        if(!lifeSystem.TakeDamage(damage))
        {
            OnDeath();
        }
    }
}
