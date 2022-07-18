using UnityEngine;

public static class EnemyMovementAnimationKeys
{
    public const string IsRunning = "isRunning";
    public const string Speed = "Speed";
}
public class EnemyAnimationController : MonoBehaviour
{
    Animator animator;
    EnemyMovement enemyMovement;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
    }
    private void Update()
    {
        animator.SetFloat(EnemyMovementAnimationKeys.Speed, enemyMovement.GetCurrentVelocityNormalized());
    }
}
