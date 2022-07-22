using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
[RequireComponent (typeof(Rigidbody2D))]
public class Dash : MonoBehaviour
{
    [Header("Dashing")]
    [SerializeField]
    private float dashVelocity;
    [SerializeField]
    private float dashingTime;
    [SerializeField]
    private float dashingCooldown;
    [SerializeField]
    private TrailRenderer trailRendererDash;


    private Vector2 dashingDir;
    private bool isDashing = false;
    private bool canDash = true;

    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public bool isAvailable()
    {
        return canDash && !isDashing;
    }
    public bool isRunning()
    {
        return isDashing;
    }
    public void StartDashing(Vector2 direction)
    {
        isDashing = true;
        canDash = false;
        trailRendererDash.emitting = true;

        if (direction == Vector2.zero)
        {
            dashingDir = new Vector2(transform.localScale.x, 0);
        }
        else
        {
            dashingDir = new Vector2(direction.x, direction.y);
        }

        StartCoroutine(StopDashing());
    }
    public void ContinueDashing()
    {
        rb.velocity = dashingDir.normalized * dashVelocity;
    }
    public IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        rb.velocity = Vector2.zero;
        trailRendererDash.emitting = false;
        isDashing = false;
        StartCoroutine(DashCooldown());
    }

    public IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
