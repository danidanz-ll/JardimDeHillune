using System;

public interface IDamageable
{
    void TakeDamage(float damage);
    void Die();
    void Resurrect();
    event Action DamageEvent;
    event Action DeathEvent;
    event Action RessurectEvent;
    event EventHandler<float> DamageValueEvent;
    bool IsDead { get; }
}
