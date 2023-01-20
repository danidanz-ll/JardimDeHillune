using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    [SerializeField] public bool isEnemy = false;
    [SerializeField][Min(0)] public float damage = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            GameObject collisionGameObject = collision.gameObject;
            if (gameObject.tag == "Player" || gameObject.tag == "Ally" || gameObject.tag == "Objective")
            {
                if (collisionGameObject.tag == "Enemy")
                {
                    damageable.TakeDamage(damage);
                }
            } else
            {
                if (collisionGameObject.tag == "Player" || collisionGameObject.tag == "Ally" || collisionGameObject.tag == "Objective")
                {
                    damageable.TakeDamage(damage);
                }
            }
        }
    }
}
