using System;

public interface IMortal
{
    void Die();
    bool IsDead { get; }
    event Action DeathEvent;
}
