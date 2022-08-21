using UnityEngine;
using System.Collections;

public interface IWeapon
{
    void Attack();
    void Attack(Vector2 direction);
    bool IsAttacking();
    float GetAttackingTime();
    float GetWaitToFreezeTime();
    IEnumerator StartAttackCooldown();
    bool IsAttackInCooldown();
}
