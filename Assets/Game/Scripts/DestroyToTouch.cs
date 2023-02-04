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
                else if (gameObject.tag == "Enemy" || gameObject.tag == "Boss")
                {
                    if (collisionGameObject.tag == "Player" || collisionGameObject.tag == "Ally" || collisionGameObject.tag == "Objective")
                    {
                        if (characterController != null)
                        {
                            if (!characterController.CharacterIsDead())
                            {
                                animator.SetTrigger("Destroy");
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("Não é um aliado nem um inimigo para ser destruído");
                }
            }
        }
    }
    public void DestroyParent()
    {
        Destroy(ParentGameObject);
    }
}
