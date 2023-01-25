using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent (typeof(PlayerInput))]
[RequireComponent(typeof(Dash))]
[RequireComponent(typeof(IMortal))]
[RequireComponent(typeof(LifeSystem))]
[RequireComponent(typeof(ManaSystem))]
public class PlayerController : MonoBehaviour, ICharacterController
{
    [Header("Weapon")]
    [SerializeField] GameObject weaponObject;
    [Header("Death settings")]
    [Min(0)]
    [SerializeField] private float TimeToDisappearAfterDeath = 0;
    [Header("Tower skills")]
    [SerializeField][Min(0)] private float SummonCost = 0;

    public UnityEngine.Events.UnityEvent TakeDamagePlayer;

    public Damageable damageable { get; private set; }
    private PlayerMovement playerMovement;
    private Dash dash;
    private TowerSkill towerSkill;
    private PlayerInput playerInput;
    private ManaSystem manaSystem;
    private LifeSystem lifeSystem;
    private IWeapon weapon;
    private Vector2 movementInput;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        dash = GetComponent<Dash>();
        playerInput = GetComponent<PlayerInput>();
        damageable = GetComponent<Damageable>();
        manaSystem = GetComponent<ManaSystem>();
        towerSkill = GetComponent<TowerSkill>();
        lifeSystem = GetComponent<LifeSystem>();

        if (weaponObject != null)
        {
            weapon = weaponObject.GetComponent<IWeapon>();
        }

        lifeSystem.DeathEvent += OnDeath;
        damageable.DamageEvent += OnDamage;
    }
    private void Update()
    {
        if (lifeSystem.IsDead!) return;

        movementInput = playerInput.GetMovementInput();

        if (playerInput.IsSelectorTowerButtonDown())
        {
            towerSkill.SelectNextTower();
        }

        if (playerInput.IsInvokeButtonDown())
        {
            if (manaSystem.currentMana - SummonCost >= 0)
            {
                manaSystem.UseMana(SummonCost);
                Vector2 aux = playerMovement.GetFacingDirection();
                Vector3 aux2 = new Vector3(aux.x, aux.y, 0);
                towerSkill.Invoke(transform.position + aux2);
            }
        }

        if (playerInput.IsAttackButtonDown())
        {
            weapon.Attack();
        }
    }
    private void FixedUpdate()
    {
        if (lifeSystem.IsDead!) return;

        if (weapon.IsAttacking() || playerMovement.IsFreeze)
        {
            return;
        }
        #region Dash
        if (dash.isRunning())
        {
            dash.ContinueDashing();
            return;
        }

        if (playerInput.IsDashingButtonDown() && dash.isAvailable())
        {
            dash.StartDashing(movementInput);
            return;
        }
        #endregion

        #region Run
        playerMovement.SetMovement(movementInput);
        #endregion
    }
    private void OnDestroy()
    {
        if (lifeSystem != null)
        {
            lifeSystem.DeathEvent -= OnDeath;
        }
        damageable.DamageEvent -= OnDamage;
    }
    public void PerformAttack()
    {
        weapon.PerformAttack();
    }
    public void DisableAttack()
    {
        weapon.DisableAttack();
    }
    public bool CharacterIsDead()
    {
        return lifeSystem.IsDead;
    }
    private void OnDeath()
    {
        playerMovement.SetBodyType(RigidbodyType2D.Static);
    }
    private void OnDamage()
    {
        TakeDamagePlayer.Invoke();
    }
}
