using System.Collections;
using UnityEngine;

public class DestroyToTouch : MonoBehaviour
{
    [SerializeField] public bool IsCharacter = true;

    private GameObject ParentGameObject;
    protected Animator animator;
    private void Awake()
    {
        ParentGameObject = gameObject.transform.parent.gameObject;
        animator = ParentGameObject.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsCharacter)
        {
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (damageable != null)
            {
                GameObject collisionGameObject = collision.gameObject;
                ICharacterController characterController = collision.GetComponent<ICharacterController>();

                if (gameObject.tag == "Player" || gameObject.tag == "Ally" || gameObject.tag == "Objective")
                {
                    if (collisionGameObject.tag == "Enemy" && characterController != null)
                    {
                        if (!characterController.CharacterIsDead())
                        {
                            animator.SetTrigger("Destroy");
                        }
                    }
                }
                else
                {
                    if ((collisionGameObject.tag == "Player" || collisionGameObject.tag == "Ally" || collisionGameObject.tag == "Objective") & characterController != null)
                    {
                        if (!characterController.CharacterIsDead())
                        {
                            animator.SetTrigger("Destroy");
                        }
                    }
                }
            }
        }
    }
    public void DestroyParent()
    {
        Destroy(ParentGameObject);
    }
}
