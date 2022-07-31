using System;
using UnityEngine;

public class TakeOnDamage : MonoBehaviour, IDamageable
{
    public event Action DamageEvent;
    public event EventHandler<float> DamageValueEvent; 
    private bool IsHurting { get; private set; } = false;
    public void TakeDamage(float damage)
    {
        IsHurting = true;
        DamageEvent.Invoke();
        DamageValueEvent.Invoke(this, damage);
    }
}
