using System;
using UnityEngine;

public interface IMortal
{
    bool IsDead { get; }
    event Action DeathEvent;
    event EventHandler<GameObject> DeathGameObjectEvent;
    event Action RessurectEvent;
}
