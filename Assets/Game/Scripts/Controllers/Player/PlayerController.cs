using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent (typeof(PlayerInput))]
[RequireComponent(typeof(Dash))]
[RequireComponent(typeof(IMortal))]
[RequireComponent(typeof(LifeSystem))]
public class PlayerController : MonoBehaviour, ICharacterController
{
    [Header("Weapon")]
    [SerializeField] GameObject weaponObject;

    public IMortal deathOnDamage { get; private set; }
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
        deathOnDamage = GetComponent<IMortal>();
        lifeSystem = GetComponent<LifeSystem>();

        if (weaponObject != null)
        {
            weapon = weaponObject.GetComponent<IWeapon>();
        }

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
        if (deathOnDamage != null)
        {
            deathOnDamage.DeathEvent -= OnDeath;
        }
    }
    public bool CharacterIsDead()
    {
        return deathOnDamage.IsDead;
    }
    private void OnDeath()
    {
        enabled = false;
    }
}
