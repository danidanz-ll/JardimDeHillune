using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent (typeof(PlayerInput))]
[RequireComponent(typeof(Dash))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(IDamageable))]
public class PlayerController : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField]
    IDamageable damageable;
    [SerializeField]
    GameObject weaponObject;

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

        //damageable.DeathEvent += OnDeath;
        //damageable.DamageEvent += OnDamage;
    }

    private void Update()
    {
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
            //damageable.DeathEvent -= OnDeath;
            //damageable.DamageEvent -= OnDamage;
        }
    }
    private void OnDeath()
    {
        playerMovement.StopMovement();
        enabled = false;
    }
    private void OnDamage(float damage)
    {
        playerMovement.StopMovement();
        lifeSystem.TakeDamage(damage);
    }
}
