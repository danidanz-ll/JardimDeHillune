using System;
using UnityEngine;

public interface IMortal
{
    void Die();
    void Resurrect();
    bool IsDead { get; }
    event Action DeathEvent;
    event EventHandler<GameObject> DeathGameObjectEvent;
    event Action RessurectEvent;
}
