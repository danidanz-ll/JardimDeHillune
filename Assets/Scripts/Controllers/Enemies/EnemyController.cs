using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("Distance")]
    [SerializeField] private float distanceStopRun;

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
        if (Vector2.Distance(transform.position, playerPosition.position) <= distanceStopRun)
        {
            animator.SetBool("isRunning", false);
            rb.velocity = Vector2.zero;
        }
        else
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        animator.SetBool("isRunning", true);

        if (rb.velocity.x > 0)
        {
            sr.flipX = false;
        }
        else if (rb.velocity.y < 0)
        {
            sr.flipX = true;
        }

        transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, moveSpeed * Time.deltaTime);
    }
}
