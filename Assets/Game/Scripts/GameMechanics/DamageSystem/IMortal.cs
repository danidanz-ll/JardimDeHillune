using System;

public interface IMortal
{
    void Die();
    void Resurrect();
    bool IsDead { get; }
    event Action DeathEvent;
    event Action RessurectEvent;
}
