public interface IWeapon
{
    void Attack();
    bool IsAttacking();
    float GetAttackingTime();
    float GetWaitToFreezeTime();
}
