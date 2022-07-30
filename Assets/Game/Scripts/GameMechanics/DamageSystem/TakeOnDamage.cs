using System;
using UnityEngine;

public class TakeOnDamage : MonoBehaviour, IDamageable
{
    public event Action DamageEvent;
    private bool IsHurting { get; private set; } = false;
    public void TakeDamage(float damage)
    {
        IsHurting = true;
        DamageEvent.Invoke();
    }
}
