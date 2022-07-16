using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("Stats")]
    [SerializeField] private float damage;
    [SerializeField] private float playerVisionRay;

    private Rigidbody2D rb;
    private Transform playerPosition;
    private Transform objectivePosition;
    private SpriteRenderer sr;
    private bool isAttackingPlayer = false;
    private bool isAttackingObjective = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        objectivePosition = GameObject.FindGameObjectWithTag("Objective").transform;
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (!isAttackingObjective && !isAttackingPlayer)
        {
            var target = GetTarget();
            FollowTarget(target);
            SetDirectionSprite(target);
        }
        else
        {
            GetComponent<LifeSystem>().TakeDamage(1);
            SetAnimationRunning(false);
            rb.velocity = Vector2.zero;
        }
    }
    private Transform GetTarget()
    {
        var entities = GetEntitiesOnVision();
        Transform target = objectivePosition;

        if (entities.Count > 0)
        {
            float minDistance = float.MaxValue;
            foreach (var entity in entities)
            {
                var distance = CalculateDistance(entity);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    target = entity;
                }
            }
        }
        return target;
    }
    private List<Transform> GetEntitiesOnVision()
    {
        List<Transform> entities = new List<Transform>();
        
        if (IsPlayerInVisionArea())
        {
            entities.Add(playerPosition);
        }
        
        return entities;
    }
    private float CalculateDistance(Transform entity)
    {
        return Vector2.Distance(transform.position, entity.position);
    }
    private bool IsPlayerInVisionArea()
    {
        return Vector2.Distance(transform.position, playerPosition.position) <= playerVisionRay;
    }
    private bool IsOnRightSide(Transform entity)
    {
        return entity.position.x > transform.position.x;
    }
    private void SetDirectionSprite(Transform entity)
    {
        if (IsOnRightSide(entity))
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }
    }
    private void SetAnimationRunning(bool play)
    {
        animator.SetBool("isRunning", play);
    }
    private void FollowTarget(Transform target)
    {
        SetAnimationRunning(true);
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isAttackingObjective)
        {
            isAttackingPlayer = true;
            if (collision.gameObject.GetComponent<LifeSystem>().TakeDamage(damage))
            {
                Debug.Log("Player is dead!");
            }
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
