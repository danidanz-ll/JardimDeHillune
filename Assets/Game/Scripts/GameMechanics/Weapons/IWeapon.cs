using UnityEngine;
using System.Collections;
using System;

public interface IWeapon
{
    event Action AttackEvent;
    void Attack();
    void Attack(Vector2 direction);
    bool IsAttacking();
    float GetAttackingTime();
    IEnumerator StartAttackCooldown();
    bool IsAttackInCooldown();
    float GetAttackTime();
    void SetDirectionWeapon(bool right);
}
