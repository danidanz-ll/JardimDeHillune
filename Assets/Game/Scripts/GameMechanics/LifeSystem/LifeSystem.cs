using System;
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

    public IDamageable damageable { get; private set; }
    
    public event Action DeathEvent;
    public event Action RessurectEvent;
    public event EventHandler<GameObject> DeathGameObjectEvent;

    public bool IsHurting { get; private set; } = false;
    public bool IsInvencible { get; private set; } = false;
    public bool IsDead { get; private set; } = false;

    private void Start()
    {
        damageable = GetComponent<IDamageable>();
        currentLife = maxLife;
        if (HealtBar != null)
        {
            HealtBar.maxValue = maxLife;
            HealtBar.value = currentLife;
        }
        damageable.DamageValueEvent += TakeDamageEvent;
        damageable.DamageEvent += DamageEventFunction;
    }
    private void Update()
    {
        if (!IsDead)
        {
            UpdateHealthBar();
            if (currentLife <= 0)
            {
                IsDead = true;
                Die();
            }
        }
    }
    private void OnDestroy()
    {
        if (damageable != null)
        {
            damageable.DamageValueEvent -= TakeDamageEvent;
            damageable.DamageEvent -= DamageEventFunction;
        }
    }
    private void DamageEventFunction()
    {

    }
    public void RegenerateLife(float points)
    {
        if (currentLife + points <= maxLife)
        {
            currentLife += points;
        }
    }
    public void SetFullLife()
    {
        currentLife = maxLife;
    }
    public void TakeDamageEvent(object sender, float damage)
    {
        TakeDamage(damage);
    }
    public void TakeDamage(float damage)
    {
        if (!IsInvencible)
        {
            currentLife -= damage;
        }
    }
    public void UpdateHealthBar()
    {
        if (HealtBar != null)
        {
            HealtBar.value = currentLife;
        }
    }
    public void Die()
    {
        IsDead = true;
        DeathEvent.Invoke();
        try
        {
            DeathGameObjectEvent.Invoke(this, this.gameObject);
        } catch(Exception e)
        {
            if (gameObject.tag == "Enemy")
            {
                throw e;
            }
        }
    }
}
