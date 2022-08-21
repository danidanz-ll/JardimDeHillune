using System.Collections;
using UnityEngine;

public class TowerController : MonoBehaviour, ICharacterController
{
    [Header("Weapon")]
    [SerializeField] GameObject weaponObject;
    [Header("Death settings")]
    [SerializeField][Min(0)] private float TimeToDisappearAfterDeath = 0;

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
    private IEnumerator DisappearAfterDeath()
    {
        //enabled = false;
        yield return new WaitForSeconds(TimeToDisappearAfterDeath);
    }
}
