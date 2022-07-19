using System;
using UnityEngine;

public class OnDamage : MonoBehaviour, IDamageable
{
    public event Action DamageEvent;

    public void TakeDamage(float damage)
    {
        //IsDead = true;
        //DeathEvent.Invoke();
    }
}
