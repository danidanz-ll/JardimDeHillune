using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class AIVision : MonoBehaviour
{
    [Range(0.5f, 10.0f)] public float visionRange = 5.0f;
    [Range(0.5f, 10.0f)] public float visionAttack = 5.0f;
    [Range(0, 360)] public float visionAngle = 30.0f;

    private EnemyMovement enemyMovement;
    private Vector2 toTarget;

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }
    public bool IsVisible(GameObject target)
    {
        if (target == null)
        {
            return false;
        }

        if (Vector2.Distance(transform.position, target.transform.position) > visionRange)
        {
            return false;
        }

        toTarget = target.transform.position - transform.position;
        Vector2 visionDirection = GetVisionDirection();

        if (Vector2.Angle(visionDirection, toTarget) > visionAngle / 2 && enemyMovement != null)
        {
            return false;
        }

        return true;
    }
    public bool IsDamageble(GameObject target)
    {
        if (target == null)
        {
            return false;
        }

        if (Vector3.Distance(transform.position, target.transform.position) > visionAttack)
        {
            return false;
        }
        
        toTarget = target.transform.position - transform.position;
        Vector2 visionDirection = GetVisionDirection();
        if (Vector2.Angle(visionDirection, toTarget) > visionAngle / 2)
        {
            return false;
        }

        return true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, visionRange);
        Gizmos.DrawWireSphere(transform.position, visionAttack);

        Vector3 visionDirection = GetVisionDirection();
        //Gizmos.DrawLine(transform.position, transform.position + visionDirection * visionRange);
        Gizmos.DrawLine(transform.position, transform.position + (Quaternion.Euler(0, 0, visionAngle/2) * visionDirection * visionRange));
        Gizmos.DrawLine(transform.position, transform.position + (Quaternion.Euler(0, 0, -visionAngle / 2) * visionDirection * visionRange));
    }
    public Vector2 GetVisionDirection()
    {
        if (enemyMovement != null)
        {
            return enemyMovement.GetFacingDirection();
        }
        else
        {
            return new Vector2(0.5f, 0.5f);
        }
    }
}
