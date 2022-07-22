using UnityEngine;

public class TriggerDamagePlayer : MonoBehaviour
{
    [SerializeField]
    [Min(0)]
    private float damage = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }
}
