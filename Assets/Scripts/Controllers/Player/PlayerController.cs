using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent (typeof(PlayerInput))]
[RequireComponent(typeof(Dash))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(IDamageable))]
public class PlayerController : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] 
    private Animator animator;

    [Header("Damage")]
    [SerializeField]
    IDamageable damageable;

    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    private Dash dash;
    private PlayerInput playerInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        dash = GetComponent<Dash>();
        playerInput = GetComponent<PlayerInput>();
        damageable = GetComponent<IDamageable>();

        damageable.DeathEvent += OnDeath;
    }

    private void Update()
    {
        if (animator.GetBool("isHurting"))
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Vector2 movementInupt = playerInput.GetMovementInput();

        #region Dash
        if (playerInput.IsJumpButtonDown() && dash.isAvailable())
        {
            dash.StartDashing(movementInupt);
        }

        if (dash.isRunning())
        {
            dash.ContinueDashing();
            return;
        }
        #endregion

        #region Run
        playerMovement.SetMovement(movementInupt);
        #endregion
    }
    private void OnDestroy()
    {
        if (damageable != null)
        {
            damageable.DeathEvent -= OnDeath;
        }
    }
    private void OnDeath()
    {
        playerMovement.StopMovement();
        enabled = false;
    }
}
