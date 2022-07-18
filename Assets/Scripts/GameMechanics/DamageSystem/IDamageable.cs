using System;

public interface IDamageable
{
    void TakeDamage(float damage);
    event Action DeathEvent;
    bool IsDead { get; }
}
