using System;

public interface IMortal
{
    void Die();
    event Action DeathEvent;
}
