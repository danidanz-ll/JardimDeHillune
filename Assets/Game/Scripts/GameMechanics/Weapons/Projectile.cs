using System.Collections;
using UnityEngine;

public class Projectile : TriggerDamage
{
    public float LifeTime = 30.0f;
    public float MoveSpeed = 10.0f;
    public string Origin = "";
    private Rigidbody2D rb;
    public Vector3 direction;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        gameObject.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        float angle = Vector3.Angle(Vector3.up, rb.velocity);
        spriteRenderer.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle*2);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null )
        {
            GameObject collisionGameObject = collision.gameObject;
            if (Origin == "Player" || Origin == "Ally" || Origin == "Objective")
            {
                if (collisionGameObject.tag == "Enemy")
                {
                    LifeTime = 0.5f;
                    StartCoroutine(DestroyProjectile());
                }
            }
            else
            {
                if (Origin == "Enemy")
                {
                    LifeTime = 0.5f;
                    StartCoroutine(DestroyProjectile());
                }
            }
        }
    }
    public void Fire()
    {
        rb.velocity = direction * MoveSpeed * Time.deltaTime;
        float angle = Vector3.Angle(Vector3.up, rb.velocity);
        spriteRenderer.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
        StartCoroutine(DestroyProjectile());
    }
    public IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(gameObject);
    }
}
