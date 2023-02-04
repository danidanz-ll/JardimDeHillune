using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Dash))]
[RequireComponent(typeof(IMortal))]
[RequireComponent(typeof(LifeSystem))]
[RequireComponent(typeof(ManaSystem))]
public class PlayerController : MonoBehaviour, ICharacterController
{
    [Header("Weapon")]
    [Tooltip("Instância do objeto da arma.")]
    [SerializeField]
    GameObject weaponObject;

    [Header("Tower skills")]
    [SerializeField]
    [Min(0)]
    [Tooltip("Custo para sumonar uma planta.")]
    private float SummonCost = 0;

    [SerializeField]
    public UnityEngine.Events.UnityEvent TakeDamagePlayer;

    [SerializeField]
    private InputActionReference MovementInput, AttackInput, DashInput, InvokeInput, SwitchTowerInput;

    [HideInInspector]
    public Damageable damageable { get; private set; }

    [HideInInspector]
    public static GameObject Instance { get; private set; }

    private PlayerMovement playerMovement;
    private Dash dash;
    private TowerSkill towerSkill;
    private PlayerInput playerInput;
    private ManaSystem manaSystem;
    private LifeSystem lifeSystem;
    private IWeapon weapon;
    private Vector2 movementInput;
    private bool invokeInput;
    private bool attackInput;
    private bool dashInput;
    private bool switchTowerInput;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }
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

        if (Settings.GetUserSettings())
        {
            lifeSystem.maxLife = SettingsAllies.GetLife("Maeve");
            lifeSystem.currentLife = SettingsAllies.GetLife("Maeve");
            weapon.SetDamage(SettingsAllies.GetDamage("Maeve"));
        }

        lifeSystem.DeathEvent += OnDeath;
        damageable.DamageEvent += OnDamage;
    }
    private void OnEnable()
    {
        AttackInput.action.performed += Attack;
        InvokeInput.action.performed += PerformInvoke;
        DashInput.action.performed += PerformDash;
        SwitchTowerInput.action.performed += PerformSwitchTower;
    }
    private void OnDisable()
    {
        AttackInput.action.performed -= Attack;
        InvokeInput.action.performed -= PerformInvoke;
        DashInput.action.performed -= PerformDash;
        SwitchTowerInput.action.performed -= PerformSwitchTower;
    }
    private void Update()
    {
        if (lifeSystem.IsDead!) return;

        movementInput = MovementInput.action.ReadValue<Vector2>();
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
        if (damageable != null)
        {
            damageable.DamageEvent -= OnDamage;
        }
    }
    public void Attack(InputAction.CallbackContext obj)
    {
        weapon.Attack();
    }
    public void PerformAttack()
    {
        weapon.PerformAttack();
    }
    public void DisableAttack()
    {
        weapon.DisableAttack();
    }
    public void PerformDash(InputAction.CallbackContext obj)
    {
        if (dash.isAvailable())
        {
            dash.StartDashing(movementInput);
        }
    }
    public void PerformInvoke(InputAction.CallbackContext obj)
    {
        if (manaSystem.currentMana - SummonCost >= 0)
        {
            manaSystem.UseMana(SummonCost);
            Vector2 aux = playerMovement.GetFacingDirection();
            Vector3 aux2 = new Vector3(aux.x, aux.y, 0);
            towerSkill.Invoke(transform.position + aux2);
        }
    }
    public void PerformSwitchTower(InputAction.CallbackContext obj)
    {
        towerSkill.SelectNextTower();
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
