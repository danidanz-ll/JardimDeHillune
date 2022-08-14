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
    private Vector2 movementInput;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        dash = GetComponent<Dash>();
        playerInput = GetComponent<PlayerInput>();
        deathOnDamage = GetComponent<IMortal>();
        lifeSystem = GetComponent<LifeSystem>();
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
            Vector3 spaceInvoke = new Vector3(5,5,0);
            Vector3 currentPosition = transform.position + spaceInvoke;
            towerSkill.Invoke(currentPosition);
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
