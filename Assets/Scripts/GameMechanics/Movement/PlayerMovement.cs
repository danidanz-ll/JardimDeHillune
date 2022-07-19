using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour, IMovement
{
    [Header("Movement")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float decceleration;
    [SerializeField]
    private float velPower;

    private Rigidbody2D rb;
    private bool running = false;
    private Vector2 currentVelocity;
    private Vector2 lookingDirection;
    private Vector2 lastForce;
    private Vector2 lastAccelerateSignal;
    private bool IsAcceleration = false;
    private bool lookingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (rb.velocity.x != 0 || rb.velocity.y != 0)
        {
            running = true;
        }
        else
        {
            running = false;
        }
    }
    public bool isRunning()
    {
        return running;
    }
    public bool isLookingToRight()
    {
        if (GetFacingDirection().x > 0)
        {
            lookingRight = true;
        }
        else if (GetFacingDirection().x < 0)
        {
            lookingRight = false;
        }
        return lookingRight;
    }
    public Vector2 GetFacingDirection()
    {
        return lookingDirection.normalized;
    }
    public float GetCurrentVelocityNormalized()
    {
        float velocity;
        if (Vector2.Angle(lastForce, rb.velocity) == 0.0f)
        {
            velocity = currentVelocity.sqrMagnitude;
        }
        else if (Vector2.Angle(lastForce, rb.velocity) >= 170.0f && Vector2.Angle(lastForce, rb.velocity) <= 190.0f)
        {
            velocity = currentVelocity.sqrMagnitude * -1.0f;
        }
        else
        {
            velocity = currentVelocity.sqrMagnitude;
        }

        return velocity;

        /*
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
        */
    }

    public void SetMovement(Vector2 direction)
    {
        var force = GetForceVector(direction.x, direction.y);
        rb.AddForce(force);
        lastForce = force;
        currentVelocity = rb.velocity;
        lookingDirection = direction;
        lastAccelerateSignal = new Vector2(Mathf.Sign(force.x), Mathf.Sign(force.y));
    }
    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
        running = false;
    }
    private Vector2 GetForceVector(float x, float y)
    {
        return new Vector2(GetForceProjection(x, rb.velocity.x), GetForceProjection(y, rb.velocity.y));
    }
    private float GetForceProjection(float input, float currentVelocityProjection)
    {
        float targetSpeed = input * moveSpeed;
        float speedDif = targetSpeed - currentVelocityProjection;
        float accelRate;
        if (Mathf.Abs(targetSpeed) > 0.01f)
        {
            accelRate = acceleration;
        }
        else
        {
            accelRate = decceleration;
        }
        return Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
    }
}
