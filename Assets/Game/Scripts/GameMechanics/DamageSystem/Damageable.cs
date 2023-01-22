using System;
using System.Collections;
using UnityEngine;

public class Damageable : MonoBehaviour, IDamageable, IMortal
{
    [SerializeField][Range(0.0f, 100.0f)] public float AttackEscape;
    public event Action DamageEvent;
    public event Action DeathEvent;
    public event Action RessurectEvent;
    public event EventHandler<float> DamageValueEvent;
    public event EventHandler<GameObject> DeathGameObjectEvent;

    public bool IsHurting { get; private set; } = false;
    public bool IsDead { get; private set; } = false;
    public bool IsInvincible { get; private set; } = false;
    public void TakeDamage(float damage)
    {
        if (!IsInvincible || IsDead)
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
    public void Die()
    {
        IsDead = true;
        DeathEvent.Invoke();
        try
        {
            DeathGameObjectEvent.Invoke(this, this.gameObject);
        }
        catch
        {
            Debug.Log("Um mob sem spawner atribuído foi morto!");
        }
    }
    public void Resurrect()
    {
        IsDead = false;
        RessurectEvent.Invoke();
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
