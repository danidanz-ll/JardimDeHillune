using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    [SerializeField] public bool isEnemy = false;
    [SerializeField][Min(0)] public float damage = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        GameObject collisionGameObject = collision.gameObject;
        if (damageable != null)
        {
            if (isEnemy)
            {
                if (collisionGameObject.tag != "Enemy")
                {
                    damageable.TakeDamage(damage);
                }
            }
            else
            {
                if (collisionGameObject.tag == "Enemy")
                {
                    damageable.TakeDamage(damage);
                }
            }
        }
    }
}
