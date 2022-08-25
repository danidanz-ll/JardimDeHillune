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

    private IDamageable damageable;
    private IMortal deathOnDamage;

    private Rigidbody2D rb;
    private bool isFreeze = false;
    private bool lookingRight = true;
    private bool running = false;
    private Vector2 currentVelocity;
    private Vector2 lookingDirection;
    private Vector2 lastForce;
    private Vector2 lastAccelerateSignal;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<IDamageable>();
        deathOnDamage = GetComponent<IMortal>();
        damageable.DamageEvent += StopMovement;
        deathOnDamage.DeathEvent += StopMovement;
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
    private void OnDestroy()
    {
        if (damageable != null)
        {
            damageable.DamageEvent -= StopMovement;
        }
        if (deathOnDamage != null)
        {
            deathOnDamage.DeathEvent -= StopMovement;
        }
    }
    public bool isRunning()
    {
        return running;
    }
    public bool isFreezing()
    {
        return isFreeze;
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

        if (Vector2.Angle(lastForce, rb.velocity) == 0.0f || (currentVelocity.sqrMagnitude >= 99.0f && currentVelocity.sqrMagnitude <= 101.0f) || (currentVelocity.sqrMagnitude >= 199.0f))
        {
            velocity = currentVelocity.sqrMagnitude;
        }
        else if (Vector2.Angle(lastForce, rb.velocity) >= 90.0f && Vector2.Angle(lastForce, rb.velocity) <= 270.0f)
        {
            velocity = currentVelocity.sqrMagnitude * -1.0f;
        }
        else
        {
            velocity = currentVelocity.sqrMagnitude;
        }

        return velocity;
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
    public void FreezeMovement(float timeWait, float timeFreezing)
    {
        rb.velocity = Vector2.zero;
        currentVelocity = Vector2.zero;
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
