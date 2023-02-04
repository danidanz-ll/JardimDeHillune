using System;

public interface IDamageable
{
    void TakeDamage(float damage);
    event Action DamageEvent;
    event EventHandler<float> DamageValueEvent;
}
