using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(IDamageable))]
[RequireComponent(typeof(IMortal))]
public class PlayerMovement : MonoBehaviour, IMovement
{
    [Header("Stats")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float decceleration;
    [SerializeField] private float velPower;
    [SerializeField] private float SpeedDecreaseFactor = 0.5f;
    [SerializeField] private float CuttingSpeed = 0f;

    private IDamageable damageable;
    private IMortal deathOnDamage;

    private Rigidbody2D rb;
    public bool IsFreeze { get; private set; } = false;
    private bool lookingRight = true;
    private bool running = false;
    private Vector2 currentVelocity;
    private Vector2 lookingDirection;
    private Vector2 lastForce;
    private Vector2 lastAccelerateSignal;

    private bool IsAccelerating = false;
    private bool IsBraking = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<IDamageable>();
        deathOnDamage = GetComponent<IMortal>();
    }
    private void Update()
    {
        if (rb.velocity.x != 0 || rb.velocity.y != 0)
        {
            var currentVelocity = GetCurrentVelocityNormalized();
            if (currentVelocity >= -CuttingSpeed && currentVelocity < 0)
            {
                Freeze();
                Unfreeze();
            }
            else
            {
                running = true;
            }
        }
        else
        {
            running = false;
        }
    }
    private void OnDestroy()
    {
    }
    public bool isRunning()
    {
        return running;
    }
    public bool isFreezing()
    {
        return IsFreeze;
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
        return lookingDirection;
    }
    public float GetCurrentVelocityNormalized()
    {
        float velocity;
        if (IsBraking)
        {
            velocity = rb.velocity.sqrMagnitude * -1.0f;
        }
        else
        {
            velocity = rb.velocity.sqrMagnitude;
        }
        return velocity;
    }

    public void SetMovement(Vector2 direction)
    {
        if (direction.sqrMagnitude == 0)
        {
            IsAccelerating = false;
            IsBraking = true;
        }
        else
        {
            IsAccelerating = true;
            IsBraking = false;
        }

        var force = GetForceVector(direction.x, direction.y);
        rb.AddForce(force);
        lastForce = force;
        currentVelocity = rb.velocity;
        lookingDirection = direction;
        lastAccelerateSignal = new Vector2(Mathf.Sign(force.x), Mathf.Sign(force.y));
    }
    public void Freeze()
    {
        rb.AddForce(-rb.velocity);
        rb.velocity = Vector2.zero;
        currentVelocity = Vector2.zero;
        running = false;
        IsFreeze = true;
    }
    public void Unfreeze()
    {
        IsFreeze = false;
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
    public void SetMass(float mass)
    {
        rb.mass = mass;
    }
    public void SetBodyType(RigidbodyType2D type)
    {
        rb.bodyType = type;
    }
    public void SetTemporarySlowdown(float time)
    {
        StartCoroutine(WaitForNormalSpeed(time));
    }
    public IEnumerator WaitForNormalSpeed(float time)
    {
        float moveSpeedDefault = moveSpeed;
        moveSpeed -= moveSpeedDefault * SpeedDecreaseFactor;
        yield return new WaitForSeconds(time);
        moveSpeed = moveSpeedDefault;
    }
}
