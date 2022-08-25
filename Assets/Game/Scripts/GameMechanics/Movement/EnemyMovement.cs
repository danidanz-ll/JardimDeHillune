using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour, IMovement
{
    [Header("Stats")]
    [SerializeField] public float moveSpeed;
    [SerializeField] private float SpeedDecreaseFactor = 0.5f;

    private IDamageable damageable;
    private IMortal deathOnDamage;

    private Rigidbody2D rb;
    private bool running = false;
    private bool lookingToRight = true;
    private bool isFreeze = false;
    private Vector2 currentVelocity;
    private Vector2 facingDirection = new(0.5f, 0.5f);
    private void Start()
    {
        damageable = GetComponent<IDamageable>();
        deathOnDamage = GetComponent<IMortal>();
        damageable.DamageEvent += StopMovement;
        deathOnDamage.DeathEvent += StopMovement;
        rb = GetComponent<Rigidbody2D>();
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
    public float GetCurrentVelocityNormalized()
    {
        var x = currentVelocity.x;
        var y = currentVelocity.y;

        return currentVelocity.sqrMagnitude;
    }
    public bool isLookingToRight()
    {
        if (GetFacingDirection().x > 0)
        {
            lookingToRight = true;
        }
        else if (GetFacingDirection().x < 0)
        {
            lookingToRight = false;
        }
        return lookingToRight;
    }
    private bool IsOnRightSide(Vector2 velocity)
    {
        if (velocity.x >= 0 || velocity.y >= 0)
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
        rb.velocity = direction * moveSpeed * Time.deltaTime;
        currentVelocity = direction * moveSpeed * Time.deltaTime;
        if (direction != Vector2.zero)
        {
            facingDirection = direction;
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
    public bool isFreezing()
    {
        return isFreeze;
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
