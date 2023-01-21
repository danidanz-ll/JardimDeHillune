using System.Collections;
using UnityEngine;

public class DestroyToTouch : MonoBehaviour
{
    [SerializeField] public bool IsCharacter = true;
    [SerializeField] public float LifeTime = 0.5f;

    private GameObject ParentGameObject;
    private void Awake()
    {
        ParentGameObject = gameObject.transform.parent.gameObject;
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
                            StartCoroutine(DestroyProjectile());
                        }
                    }
                }
                else
                {
                    if ((collisionGameObject.tag == "Player" || collisionGameObject.tag == "Ally" || collisionGameObject.tag == "Objective") & characterController != null)
                    {
                        if (!characterController.CharacterIsDead())
                        {
                            StartCoroutine(DestroyProjectile());
                        }
                    }
                }
            }
        }
    }
    public IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(ParentGameObject);
    }
}
