using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    [SerializeField]
    [Min(0)] 
    public float Damage = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            GameObject collisionGameObject = collision.gameObject;
            
            if (collisionGameObject.tag == "Objective" && !(gameObject.tag == "Player" || gameObject.tag == "Ally"))
            {
                damageable.TakeDamage(Damage);
                return;
            }

            ICharacterController characterController = collision.GetComponent<ICharacterController>();

            if (gameObject.tag == "Player" || gameObject.tag == "Ally")
            {
                if (collisionGameObject.tag == "Enemy" && characterController != null)
                {
                    if (!characterController.CharacterIsDead())
                    {
                        damageable.TakeDamage(Damage);
                    }
                }
            } else
            {
                if ((collisionGameObject.tag == "Player" || collisionGameObject.tag == "Ally" || collisionGameObject.tag == "Objective") && characterController != null)
                {
                    if (!characterController.CharacterIsDead())
                    {
                        damageable.TakeDamage(Damage);
                    }
                }
            }
        }
    }
}
