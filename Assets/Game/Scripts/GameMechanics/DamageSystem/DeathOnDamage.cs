using System;
using UnityEngine;

public class DeathOnDamage : MonoBehaviour, IMortal
{
    public event Action DeathEvent;
    public bool IsDead { get; private set; } = false;
    public void Die()
    {
        IsDead = true;
        DeathEvent.Invoke();
    }
}
