using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private TowerSkill towerSkill;
    [SerializeField][Min(0)] private float SummonCost = 0;

    public IMortal deathOnDamage { get; private set; }
    private PlayerMovement playerMovement;
    private Dash dash;
    private PlayerInput playerInput;
    private LifeSystem lifeSystem;
    private ManaSystem manaSystem;
    private IWeapon weapon;
    private Vector2 movementInput;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        dash = GetComponent<Dash>();
        playerInput = GetComponent<PlayerInput>();
        deathOnDamage = GetComponent<IMortal>();
        lifeSystem = GetComponent<LifeSystem>();
        manaSystem = GetComponent<ManaSystem>();
        towerSkill = GetComponent<TowerSkill>();

        if (weaponObject != null)
        {
            weapon = weaponObject.GetComponent<IWeapon>();
        }

        deathOnDamage.DeathEvent += OnDeath;
    }
    private void Update()
    {
        movementInput = playerInput.GetMovementInput();

        if (weapon != null && playerInput.IsAttackButtonDown() && !weapon.IsAttacking())
        {
            weapon.Attack();
            playerMovement.FreezeMovement(weapon.GetWaitToFreezeTime(), weapon.GetAttackingTime());
        }

        if (playerInput.IsInvokeButtonDown())
        {
            if (manaSystem.currentMana > 0)
            {
                manaSystem.UseMana(SummonCost);
                Vector2 aux = playerMovement.GetFacingDirection();
                Vector3 aux2 = new Vector3(aux.x, aux.y, 0);
                towerSkill.Invoke(transform.position + aux2);
            }
        }
    }
    private void FixedUpdate()
    {
        if (weapon.IsAttacking())
        {
            playerMovement.FreezeMovement(weapon.GetWaitToFreezeTime(), weapon.GetAttackingTime());
            return;
        }

        if (dash.isRunning())
        {
            dash.ContinueDashing();
            return;
        }

        #region Dash
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
        playerMovement.SetBodyType(RigidbodyType2D.Static);
        StartCoroutine(DisappearAfterDeath());
    }
    private IEnumerator DisappearAfterDeath()
    {
        enabled = false;
        yield return new WaitForSeconds(TimeToDisappearAfterDeath);
    }
}
