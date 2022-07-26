using System.Collections;

public interface IWeapon
{
    void Attack();
    bool IsAttacking();
    float GetAttackingTime();
    float GetWaitToFreezeTime();
    IEnumerator StartAttackCooldown();
    bool IsAttackInCooldown();
}
