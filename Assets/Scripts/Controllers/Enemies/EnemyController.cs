using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("Distance")]
    [SerializeField] private float visionArea;

    private Rigidbody2D rb;
    private Transform playerPosition;
    private SpriteRenderer sr;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (IsPlayerInVisionArea())
        {
            animator.SetBool("isRunning", false);
            rb.velocity = Vector2.zero;
        }
        else
        {
            FollowPlayer();
        }
    }
    private bool IsPlayerInVisionArea()
    {
        return Vector2.Distance(transform.position, playerPosition.position) <= visionArea;
    }
    private bool IsPlayerOnRightSide()
    {
        return playerPosition.position.x > transform.position.x;
    }
    private void FollowPlayer()
    {
        animator.SetBool("isRunning", true);
        transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, moveSpeed * Time.deltaTime);

        if (IsPlayerOnRightSide())
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }

    }
}
