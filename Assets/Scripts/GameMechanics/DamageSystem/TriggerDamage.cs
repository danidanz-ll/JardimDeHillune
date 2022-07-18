using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    [SerializeField]
    [Min(0)]
    private float damage = 0;
    [SerializeField]
    [Range(0, 1)]
    private float attackFieldReason = 0.5f;

    private PolygonCollider2D colliderAttackArea;
    private AIVision aIVision;
    private void Awake()
    {
        colliderAttackArea = GetComponent<PolygonCollider2D>();
        aIVision = GetComponentInParent<AIVision>();
        
        colliderAttackArea.offset = new Vector2(0f, 0f);

        Vector2 direction = aIVision.GetVisionDirection().normalized * (aIVision.visionRange - (aIVision.visionRange / 1.2f));
        Vector2 point1 = Quaternion.Euler(0, 0, aIVision.visionAngle / 2) * direction.normalized * attackFieldReason;
        Vector2 point2 = Quaternion.Euler(0, 0, -aIVision.visionAngle / 2) * direction.normalized * attackFieldReason;
        colliderAttackArea.points = new[] { new Vector2(0f, 0f), point1.normalized, point2.normalized };
    }
    private void Update()
    {
        Vector2 direction = aIVision.GetVisionDirection().normalized * (aIVision.visionRange - (aIVision.visionRange / 1.2f));
        Vector2 point1 = Quaternion.Euler(0, 0, aIVision.visionAngle / 2) * direction.normalized * attackFieldReason;
        Vector2 point2 = Quaternion.Euler(0, 0, -aIVision.visionAngle / 2) * direction.normalized * attackFieldReason;
        colliderAttackArea.points = new[] { new Vector2(0f, 0f), point1, point2 };
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }
}
