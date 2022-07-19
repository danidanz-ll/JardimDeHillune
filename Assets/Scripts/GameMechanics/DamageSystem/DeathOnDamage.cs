using System;
using UnityEngine;

public class DeathOnDamage : MonoBehaviour
{
    public bool IsDead { get; private set; }

    public event Action DeathEvent;
    public event Action DamageEvent;

    private void Awake()
    {
        IsDead = false;
    }

    public void SetDeath(float damage)
    {
        IsDead = true;
        DeathEvent.Invoke();
    }
}
