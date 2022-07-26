using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent (typeof(PlayerInput))]
[RequireComponent(typeof(Dash))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(IDamageable))]
public class PlayerController : MonoBehaviour, ICharacterController
{
    [Header("Damage")]
    [SerializeField]
    IDamageable damageable;
    [SerializeField]
    GameObject weaponObject;

    public bool IsDead = false;
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
        lifeSystem = GetComponent<LifeSystem>();

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
    private void OnDamage()
    {
        playerMovement.StopMovement();
        lifeSystem.TakeDamage(10);
    }
}
