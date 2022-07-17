using System;

public interface IDamageable
{
    void TakeDamage(int damage);
    event Action DamageEvent;
}
