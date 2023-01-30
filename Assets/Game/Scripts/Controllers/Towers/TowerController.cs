using System.Collections;
using UnityEngine;

[RequireComponent(typeof(IMortal))]
[RequireComponent(typeof(LifeSystem))]
public class TowerController : MonoBehaviour, ICharacterController, IAIController
{
    [Header("Stats")]
    [SerializeField]
    [Range(0.5f, 10.0f)] 
    public float VisionRange;
    
    [Header("Settings")]
    [SerializeField] 
    public bool IsMelee = true;

    [SerializeField]
    public string TowerName;

    [Header("Weapon")]
    [SerializeField] 
    GameObject weaponObject;

    public bool IsPLantGrew { get; private set; } = false;
    private LifeSystem lifeSystem;
    private IDamageable damageable;
    private SpriteRenderer spriteRenderer;
    public IMortal mortal { get; private set; }
    private IWeapon weapon;

    private GameObject Body;
    private GameObject Canvas;
    private void Awake()
    {
        lifeSystem = GetComponent<LifeSystem>();
        damageable = GetComponent<IDamageable>();
        mortal = GetComponent<IMortal>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (weaponObject != null)
        {
            weapon = weaponObject.GetComponent<IWeapon>();
        }

        if (Settings.GetUserSettings())
        {
            lifeSystem.maxLife = SettingsAllies.GetLife(TowerName);
            lifeSystem.currentLife = SettingsAllies.GetLife(TowerName);
            weapon.SetDamage(SettingsAllies.GetDamage(TowerName));
        }
    }
    void Start()
    {
        lifeSystem.DeathEvent += OnDeath;
        lifeSystem.RessurectEvent += Resurrect;
        Body = gameObject.transform.GetChild(1).gameObject;
        Canvas = gameObject.transform.GetChild(2).gameObject;
    }
    private void OnDestroy()
    {
        if (lifeSystem != null)
        {
            lifeSystem.DeathEvent -= OnDeath;
            lifeSystem.RessurectEvent -= Resurrect;
        }
    }
    public void Born()
    {
        Body.SetActive(false);
        Canvas.SetActive(false);
        IsPLantGrew = false;
    }
    public void SetFullBirth()
    {
        Body.SetActive(true);
        Canvas.SetActive(true);
        IsPLantGrew = true;
    }
    public void Attack()
    {
        weapon.Attack();
    }
    public void Attack(GameObject target)
    {
        if(target == null)
        {
            return;
        }
        weapon.Attack(target.transform.position - gameObject.transform.position);
    }
    private void OnDeath()
    {
        Body.SetActive(false);
        Canvas.SetActive(false);
        Destroy(gameObject, 5f);
    }
    private void Resurrect()
    {
        lifeSystem.SetFullLife();
    }
    public bool CharacterIsDead()
    {
        return lifeSystem.IsDead;
    }
    private void SetMovement()
    {
        throw new System.NotImplementedException();
    }
    public Vector3 GetCurrentPosition()
    {
        return transform.position;
    }
    private float GetMovementSpeed()
    {
        return 0.0f;
    }

    public void SetMovement(Vector2 direction)
    {
        throw new System.NotImplementedException();
    }
    float IAIController.GetMovementSpeed()
    {
        throw new System.NotImplementedException();
    }
    public void SetLookDirection(GameObject target)
    {
        Vector2 toTarget = gameObject.transform.position - target.transform.position;
        if (IsOnRightSide(toTarget))
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
    private bool IsOnRightSide(Vector2 target)
    {
        if (target.x >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
