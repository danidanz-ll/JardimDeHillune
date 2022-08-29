using UnityEngine;

public interface IAIController
{
    void Attack();
    void Attack(GameObject target);
    void SetMovement(Vector2 direction);
    Vector3 GetPosition();
    float GetMovementSpeed();
}