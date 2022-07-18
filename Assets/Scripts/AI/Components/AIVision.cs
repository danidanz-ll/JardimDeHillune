using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class AIVision : MonoBehaviour
{
    [Range(0.5f, 10.0f)]
    public float visionRange = 5;
    [Range(0, 360)]
    public float visionAngle = 30;

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
            Debug.Log("Objeto nulo");
            return false;
        }

        if (Vector2.Distance(transform.position, target.transform.position) > visionRange)
        {
            Debug.Log("Objeto fora do raio");
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

        Vector3 visionDirection = GetVisionDirection();
        //Gizmos.DrawLine(transform.position, transform.position + visionDirection * visionRange);
        Gizmos.DrawLine(transform.position, transform.position + (Quaternion.Euler(0, 0, visionAngle/2) * visionDirection * visionRange));
        Gizmos.DrawLine(transform.position, transform.position + (Quaternion.Euler(0, 0, -visionAngle / 2) * visionDirection * visionRange));
    }
    private Vector2 GetVisionDirection()
    {
        if (enemyMovement != null)
        {
            return enemyMovement.GetFacingDirection();
        }
        else
        {
            return new Vector2(1.0f, 1.0f);
        }
    }
}
