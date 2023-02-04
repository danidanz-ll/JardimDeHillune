using System.Collections;
using UnityEngine;

public class Projectile : TriggerDamage
{
    public float LifeTime = 30.0f;
    public float MoveSpeed = 0;
    public string Origin = "";
    private Rigidbody2D rb;
    public Vector3 direction;
    private SpriteRenderer spriteRenderer;
    private DestroyToTouch DestroyToTouchComponent;
    
    private void Awake()
    {
        gameObject.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        DestroyToTouchComponent = transform.GetChild(0).gameObject.GetComponent<DestroyToTouch>();
    }
    private void Update()
    {
        float angle = Vector3.Angle(Vector3.up, rb.velocity);
        spriteRenderer.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle*2);
    }
    public void Fire()
    {
        rb.velocity = direction * MoveSpeed * Time.deltaTime;
        float angle = Vector3.Angle(Vector3.up, rb.velocity);
        spriteRenderer.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle*2);
        StartCoroutine(DestroyProjectile());
    }
    public IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(gameObject);
    }
    public void DestroyOnContact()
    {
        Destroy(this.gameObject);
    }
}
