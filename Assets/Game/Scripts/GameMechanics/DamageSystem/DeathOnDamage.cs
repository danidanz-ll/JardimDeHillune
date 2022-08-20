using System;
using UnityEngine;

public class DeathOnDamage : MonoBehaviour, IMortal
{
    public event Action DeathEvent;
    public event Action RessurectEvent;
    public bool IsDead { get; private set; } = false;
    public void Die()
    {
        IsDead = true;
        DeathEvent.Invoke();
    }
    public void Resurrect()
    {
        IsDead = false;
        RessurectEvent.Invoke();
    }
}
