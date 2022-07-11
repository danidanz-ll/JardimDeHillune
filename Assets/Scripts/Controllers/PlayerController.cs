using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Dashing")]
    [SerializeField] private float dashVelocity;
    [SerializeField] private float dashingTime;
    [SerializeField] private float dashingCooldown;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float decceleration;
    [SerializeField] private float velPower;

    private Rigidbody2D rb;
    private TrailRenderer trailRenderer;
    
    private Vector2 dashingDir;
    private bool isDashing = false;
    private bool canDash = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void FixedUpdate()
    {
        var moveInputX = Input.GetAxisRaw("Horizontal");
        var moveInputY = Input.GetAxisRaw("Vertical");
        var dashInput = Input.GetButtonDown("Dash");

        #region Dash
        if (dashInput && canDash)
        {
            isDashing = true;
            canDash = false;
            trailRenderer.emitting = true;
            dashingDir = new Vector2(moveInputX, moveInputY);
            if (dashingDir == Vector2.zero)
            {
                dashingDir = new Vector2(transform.localScale.x, 0);
            }
            StartCoroutine(StopDashing());
        }
        
        if (isDashing)
        {
            rb.velocity = dashingDir.normalized * dashVelocity;
            return;
        }
        #endregion
        #region Run
        float targetSpeedX = moveInputX * (isDashing ? dashVelocity : moveSpeed);
        float speedDifX = targetSpeedX - rb.velocity.x;
        float accelRateX = (Mathf.Abs(targetSpeedX) > 0.01f) ? acceleration : decceleration;
        float movementX = Mathf.Pow(Mathf.Abs(speedDifX) * accelRateX, velPower) * Mathf.Sign(speedDifX);

        float targetSpeedY = moveInputY * (isDashing ? dashVelocity : moveSpeed);
        float speedDifY = targetSpeedY - rb.velocity.y;
        float accelRateY = (Mathf.Abs(targetSpeedY) > 0.01f) ? acceleration : decceleration;
        float movementY = Mathf.Pow(Mathf.Abs(speedDifY) * accelRateY, velPower) * Mathf.Sign(speedDifY);
        
        Vector2 vectorFinal = new Vector2(movementX, movementY);
        rb.AddForce(vectorFinal);
        #endregion
    }
    public IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        isDashing = false;
        StartCoroutine(DashCooldown());
    }

    public IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
