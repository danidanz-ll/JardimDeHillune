using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    [SerializeField] private bool isEnemy = false;
    [SerializeField][Min(0)] private float damage = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        GameObject collisionGameObject = collision.GetComponent<Collider>().gameObject;
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
                damageable.TakeDamage(damage);
            }
        }
    }
}
