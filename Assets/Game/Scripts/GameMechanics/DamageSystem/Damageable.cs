using System;
using System.Collections;
using UnityEngine;

public class Damageable : MonoBehaviour, IDamageable
{
    [SerializeField][Range(0.0f, 100.0f)] public float AttackEscape;
    public event Action DamageEvent;
    public event EventHandler<float> DamageValueEvent;

    public bool IsHurting { get; private set; } = false;
    public bool IsInvincible { get; private set; } = false;

    private LifeSystem lifeSystem;

    private void Start()
    {
        lifeSystem = GetComponent<LifeSystem>();
    }
    public void TakeDamage(float damage)
    {
        if (!IsInvincible && !lifeSystem.IsDead)
        {
            if (UnityEngine.Random.Range(0, 100) >= AttackEscape)
            {
                IsHurting = true;
                DamageEvent.Invoke();
                DamageValueEvent.Invoke(this, damage);
            }
        }
    }
    public void StopHurting()
    {
        IsHurting = false;
    }
    public void SetInvincible()
    {
        IsInvincible = true;
    }
    public void UnsetInvincible()
    {
        IsInvincible = false;
    }
}
