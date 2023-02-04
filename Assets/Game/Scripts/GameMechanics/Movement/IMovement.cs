using System.Collections;
using UnityEngine;

public interface IMovement
{
    bool isRunning();
    bool isFreezing();
    Vector2 GetFacingDirection();
    bool isLookingToRight();
    float GetCurrentVelocityNormalized();
    void SetMovement(Vector2 direction);
    void Freeze();
    void Unfreeze();
    void SetTemporarySlowdown(float time);
    IEnumerator WaitForNormalSpeed(float time);
}
