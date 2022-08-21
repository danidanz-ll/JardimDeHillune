using System.Collections;
using UnityEngine;

public class Projectile : TriggerDamage
{
    public float LifeTime = 30.0f;
    public float MoveSpeed = 10.0f;
    private Rigidbody2D rb;
    private Vector2 direction;
    private void Awake()
    {
        gameObject.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    public void Fire()
    {
        rb.velocity = direction * MoveSpeed * Time.deltaTime;
        StartCoroutine(DestroyProjectile());
    }
    public IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(gameObject);
    }
}
