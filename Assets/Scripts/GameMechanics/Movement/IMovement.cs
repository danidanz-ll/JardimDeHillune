using UnityEngine;

public interface IMovement
{
    bool isRunning();
    float GetCurrentVelocityNormalized();
    void SetMovement(Vector2 direction);
}
