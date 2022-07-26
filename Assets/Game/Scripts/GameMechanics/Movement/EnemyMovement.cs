using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour, IMovement
{
    [Header("Movement")]
    [SerializeField]
    public float moveSpeed;

    private Rigidbody2D rb;
    private bool running = false;
    private bool lookingToRight = true;
    private bool isFreeze = false;
    private Vector2 currentVelocity;
    private Vector2 facingDirection = new(0.5f, 0.5f);
    public float GetCurrentVelocityNormalized()
    {
        var x = currentVelocity.x;
        var y = currentVelocity.y;

        if (x >= 0.5f || x <= -0.5f)
        {
            return currentVelocity.normalized.x;
        }
        else if (y >= 0.5f || y <= -0.5f)
        {
            return currentVelocity.normalized.y;
        }
        else
        {
            return 0;
        }
    }
    public bool isLookingToRight()
    {
        return lookingToRight;
    }
    private bool IsOnRightSide(Vector2 velocity)
    {
        if (velocity.normalized.x >= 0 || velocity.normalized.y >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool isRunning()
    {
        return running;
    }
    public Vector2 GetFacingDirection()
    {
        return facingDirection;
    }
    public void SetMovement(Vector2 direction)
    {
        rb.velocity = direction.normalized * moveSpeed;
        currentVelocity = rb.velocity;
        if (direction != Vector2.zero)
        {
            facingDirection = direction.normalized;
        }

        if (IsOnRightSide(direction))
        {
            lookingToRight = true;
        }
        else
        {
            lookingToRight = false;
        }
    }
    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
        running = false;
    }
    public void FreezeMovement(float timeWait, float timeFreezing)
    {
        rb.velocity = Vector2.zero;
        running = false;
        StartCoroutine(WaitToFreezing(timeWait, timeFreezing));
    }
    private IEnumerator WaitToFreezing(float timeWait, float timeFreezing)
    {
        yield return new WaitForSeconds(timeWait);
        StartCoroutine(WaitFreezing(timeFreezing));
    }
    private IEnumerator WaitFreezing(float time)
    {
        isFreeze = true;
        yield return new WaitForSeconds(time);
        isFreeze = false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public bool isFreezing()
    {
        return isFreeze;
    }
}
