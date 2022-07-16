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

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;
    private TrailRenderer trailRenderer;
    private SpriteRenderer sr;
    
    private Vector2 dashingDir;
    private bool isDashing = false;
    private bool canDash = true;

    private float moveInputX;
    private float moveInputY;
    private bool dashInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        moveInputX = Input.GetAxisRaw("Horizontal");
        moveInputY = Input.GetAxisRaw("Vertical");
        dashInput = Input.GetButtonDown("Dash") || Input.GetButtonUp("Dash");
    }
    private void FixedUpdate()
    {
        if (animator.GetBool("isHurting"))
        {
            rb.velocity = Vector2.zero;
            return;
        }
        #region Dash
        if (dashInput && canDash)
        {
            Debug.Log("Dashing");
            animator.SetBool("isDashing", false);
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
        if (moveInputX == 0 && moveInputY == 0)
        {
            //Debug.Log("Parado");
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);
            if (moveInputX > 0)
            {
                //Debug.Log("Andando para direita");
                sr.flipX = false;
            }
            else if (moveInputX < 0)
            {
                //Debug.Log("Andando para esquerda");
                sr.flipX = true;
            }

            if (moveInputY > 0)
            {
                //Debug.Log("Andando para cima");
                //transform.transform.rotation = new Quaternion(0, 90, 0, 0);
            }
            else if (moveInputY < 0)
            {
                //Debug.Log("Andando para baixo");
                //transform.transform.rotation = new Quaternion(0, -90, 0, 0);
            }
            else
            {
                //transform.transform.rotation = new Quaternion(0, 0, 0, 0);
            }

            //playerAnimationController.PlayAnimation("playerRun");
        }

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
        Debug.Log("Dash em carregamento");
        yield return new WaitForSeconds(dashingCooldown);
        Debug.Log("Dash pronto");
        canDash = true;
    }
}
