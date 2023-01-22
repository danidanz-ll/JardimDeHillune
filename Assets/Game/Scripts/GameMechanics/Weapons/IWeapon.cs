using UnityEngine;
using System.Collections;
using System;

public interface IWeapon
{
    event Action AttackEvent;
    void Attack();
    void Attack(Vector2 direction);
    bool IsAttacking();
    IEnumerator StartAttackCooldown();
    bool IsAttackInCooldown();
    void SetDirectionWeapon(bool right);
    void PerformAttack();
    void DisableAttack();
}
