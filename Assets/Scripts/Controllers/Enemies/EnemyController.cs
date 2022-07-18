using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float damage;
    [SerializeField] private float playerVisionRay;

    private EnemyMovement enemyMovement;
    private bool isAttackingPlayer = false;
    private bool isAttackingObjective = false;
    private Collider2D lastCollision;
    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }
    void Update()
    {
        //lastCollision.gameObject.GetComponent<LifeSystem>().TakeDamage(damage);
    }
    public void SetMovement(Vector2 direction)
    {
        enemyMovement.SetMovement(direction.normalized);
    }
    public float GetMovementSpeed()
    {
        return enemyMovement.moveSpeed;
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
