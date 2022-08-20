using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    [SerializeField] private bool isEnemy = false;
    [SerializeField][Min(0)] private float damage = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        GameObject collisionGameObject = collision.collider.gameObject;
        if (damageable != null)
        {
            if (collisionGameObject.tag != "Enemy")
            {
                damageable.TakeDamage(damage);
            }
        }
    }
}
