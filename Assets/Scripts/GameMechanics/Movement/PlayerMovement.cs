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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public bool isRunning()
    {
        return running;
    }
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

    public void SetMovement(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            running = false;
        }
        else
        {
            running = true;
        }
        rb.AddForce(GetForceVector(direction.x, direction.y));
        currentVelocity = rb.velocity;
    }
    private Vector2 GetForceVector(float x, float y)
    {
        return new Vector2(GetForceProjection(x, rb.velocity.x), GetForceProjection(y, rb.velocity.y));
    }
    private float GetForceProjection(float input, float currentVelocityProjection)
    {
        float targetSpeed = input * moveSpeed;
        float speedDif = targetSpeed - currentVelocityProjection;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        return Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
    }
}
