using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(IDamageable))]
[RequireComponent(typeof(IMortal))]
public class LifeSystem : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public float maxLife;
    [SerializeField] public float currentLife;
    [SerializeField] private Slider HealtBar;
    [SerializeField] private float InvencibleTimeDamage;
    [SerializeField] private float TimeDamage;

    public IDamageable damageable { get; private set; }
    public IMortal deathOnDamage { get; private set; }

    public bool IsHurting { get; private set; } = false;
    public bool IsInvencible { get; private set; } = false;

    private void Start()
    {
        damageable = GetComponent<IDamageable>();
        deathOnDamage = GetComponent<IMortal>();
        currentLife = maxLife;
        if (HealtBar != null)
        {
            HealtBar.maxValue = maxLife;
            HealtBar.value = currentLife;
        }
        damageable.DamageValueEvent += TakeDamageEvent;
    }
    private void OnDestroy()
    {
        if (damageable != null)
        {
            damageable.DamageValueEvent -= TakeDamageEvent;
        }
    }
    public void TakeDamageEvent(object sender, float damage)
    {
        TakeDamage(damage);
    }
    public void TakeDamage(float damage)
    {
        if (!IsInvencible)
        {
            StartCoroutine(PlayHurtingAnimation());
            StartCoroutine(TimeInvencible());
        
            currentLife -= damage;
            UpdateHealthBar();
            if (currentLife <= 0)
            {
                Debug.Log("Dead!");
                deathOnDamage.Die();
            }
        }
    }
    public void UpdateHealthBar()
    {
        if (HealtBar != null)
        {
            HealtBar.value = currentLife;
        }
    }
    
    public IEnumerator PlayHurtingAnimation()
    {
        IsHurting = true;
        yield return new WaitForSeconds(TimeDamage);
        IsHurting = false;
    }
    public IEnumerator TimeInvencible()
    {
        IsInvencible = true;
        yield return new WaitForSeconds(InvencibleTimeDamage);
        IsInvencible = false;
    }
}
