using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent (typeof(PlayerInput))]
[RequireComponent(typeof(Dash))]
[RequireComponent(typeof(IMortal))]
[RequireComponent(typeof(LifeSystem))]
public class PlayerController : MonoBehaviour, ICharacterController
{
    [Header("Weapon")]
    [SerializeField] GameObject weaponObject;
    [Header("Death settings")]
    [Min(0)]
    [SerializeField] private float TimeToDisappearAfterDeath = 0;
    [Header("Tower skills")]
    [SerializeField] private TowerSkill towerSkill;

    public IMortal deathOnDamage { get; private set; }
    private PlayerMovement playerMovement;
    private Dash dash;
    private PlayerInput playerInput;
    private LifeSystem lifeSystem;
    private IWeapon weapon;
    private Vector2 movementInupt;

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
        Vector2 movementInupt = playerInput.GetMovementInput();

        if (weapon != null && playerInput.IsAttackButtonDown() && !weapon.IsAttacking())
        {
            weapon.Attack();
            playerMovement.FreezeMovement(weapon.GetWaitToFreezeTime(), weapon.GetAttackingTime());
        }

        if (playerInput.IsInvokeButtonDown())
        {
            towerSkill.Invoke(transform.position);
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
            dash.StartDashing(movementInupt);
            return;
        }
        #endregion

        #region Run
        playerMovement.SetMovement(movementInupt);
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
