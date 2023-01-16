using System.Collections;
using UnityEngine;

[RequireComponent(typeof(IMortal))]
[RequireComponent(typeof(LifeSystem))]
public class TowerController : MonoBehaviour, ICharacterController, IAIController
{
    [Header("Stats")]
    [SerializeField][Range(0.5f, 10.0f)] public float VisionRange;
    [Header("Weapon")]
    [SerializeField] GameObject weaponObject;

    private LifeSystem lifeSystem;
    private IDamageable damageable;
    public IMortal mortal { get; private set; }
    private IWeapon weapon;
    private void Awake()
    {
        lifeSystem = GetComponent<LifeSystem>();
        damageable = GetComponent<IDamageable>();
        mortal = GetComponent<IMortal>();

        if (weaponObject != null)
        {
            weapon = weaponObject.GetComponent<IWeapon>();
        }
    }
    void Start()
    {
        mortal.DeathEvent += OnDeath;
        mortal.RessurectEvent += Resurrect;
    }
    private void OnDestroy()
    {
        if (mortal != null)
        {
            mortal.DeathEvent -= OnDeath;
            mortal.RessurectEvent -= Resurrect;
        }
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
        weapon.Attack(target.transform.position.normalized - gameObject.transform.position.normalized);
    }
    private void OnDeath()
    {
        //StartCoroutine(DisappearAfterDeath());
    }
    private void Resurrect()
    {
        lifeSystem.SetFullLife();
    }
    public bool CharacterIsDead()
    {
        return mortal.IsDead;
    }
    private void SetMovement()
    {

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
}
