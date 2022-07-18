using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] public Animator animator;

    [Header("Stats")]
    [SerializeField] private float damage;
    [SerializeField] private float playerVisionRay;

    private Rigidbody2D rb;
    public EnemyMovement enemyMovement;
    private SpriteRenderer sr;
    private bool isAttackingPlayer = false;
    private bool isAttackingObjective = false;
    private Collider2D lastCollision;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        enemyMovement = GetComponent<EnemyMovement>();
    }
    void Update()
    {
        /*
        if (!isAttackingObjective && !isAttackingPlayer)
        {
            var target = GetTarget();
            FollowTarget(target);
            SetDirectionSprite(target);
        }
        else
        {
            rb.velocity = Vector2.zero;
            lastCollision.gameObject.GetComponent<LifeSystem>().TakeDamage(damage);
        }
        */
        if (enemyMovement.isLookingToRight())
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        lastCollision = collision;
        if (collision.gameObject.CompareTag("Player") && !isAttackingObjective)
        {
            isAttackingPlayer = true;
            //if (collision.gameObject.GetComponent<LifeSystem>().TakeDamage(damage))
            //{
            //    Debug.Log("Player is dead!");
            //}
        }
        else if (collision.gameObject.CompareTag("Objective") && !isAttackingPlayer)
        {
            isAttackingObjective = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttackingPlayer = false;
        }
        else if (collision.gameObject.CompareTag("Objective"))
        {
            isAttackingObjective = false;
        }
    }
}
