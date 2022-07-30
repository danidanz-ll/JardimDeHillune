using System;
using UnityEngine;

public class TakeOnDamage : MonoBehaviour, IDamageable
{
    public event Action DamageEvent;

    private bool IsHurting = false;

    public bool IsTakingDamage()
    {
        return IsHurting;
    }

    public void TakeDamage(float damage)
    {
        DamageEvent.Invoke();
    }
}
