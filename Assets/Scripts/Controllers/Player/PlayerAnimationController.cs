using System.Collections;
using UnityEngine;

public static class CharacterMovementAnimationKeys
{
    public const string IsRunning = "isRunning";
    public const string Speed = "Speed";
}
public class PlayerAnimationController : MonoBehaviour
{
    Animator animator;
    PlayerMovement playerMovement;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        animator.SetFloat(CharacterMovementAnimationKeys.Speed, playerMovement.GetCurrentVelocityNormalized());
    }
}
