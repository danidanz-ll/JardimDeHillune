using System;
using UnityEngine;

public class DeathOnDamage : MonoBehaviour, IDamageable
{
    public bool IsDead { get; private set; }

    public event Action DeathEvent;

    private void Awake()
    {
        IsDead = false;
    }

    public void TakeDamage(float damage)
    {
        IsDead = true;
        DeathEvent.Invoke();
    }
}
