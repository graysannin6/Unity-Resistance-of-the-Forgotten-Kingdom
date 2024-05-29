using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemyRadius = 1.2f;
    public float speed = 3.0f;
    public float force = 50.0f;
    public float minimumDistToAvoid = 5.0f;
    public float targetReachedRadius = 0.5f;
    public float avoidanceRadius = 1.5f;

    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    private Vector3 targetPoint;
    private SpriteRenderer spriteRenderer;
    private ObjectPool objectPool;
    private Push pushed;
    private Flash flash;
    private Animator animator;
    private bool isDead = false;
    private Rigidbody2D rb;

    void Start()
    {
        targetPoint = Vector3.zero;
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectPool = FindObjectOfType<ObjectPool>();
        pushed = GetComponent<Push>();
        currentHealth = maxHealth;
        flash = GetComponent<Flash>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDead)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (pushed != null && pushed.GettingKnockedBack)
        {
            return;
        }

        GameObject player = GameObject.FindObjectOfType<Player>().gameObject;
        if (player != null)
        {
            targetPoint = player.transform.position;
        }

        Vector3 dir = (targetPoint - transform.position).normalized;

        Vector2 avoidanceForce = CalculateAvoidanceForce();
        dir += (Vector3)avoidanceForce;
        dir = dir.normalized;

        if (Vector3.Distance(targetPoint, transform.position) < targetReachedRadius)
            return;

        rb.velocity = dir * speed;

        if (player != null)
        {
            if (player.transform.position.x < transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    private Vector2 CalculateAvoidanceForce()
    {
        Vector2 avoidanceForce = Vector2.zero;

        // Avoid obstacles
        int layerMask = 1 << 8;
        RaycastHit2D obstacleHit = Physics2D.CircleCast(transform.position, enemyRadius, rb.velocity, minimumDistToAvoid, layerMask);
        if (obstacleHit.collider != null)
        {
            Vector2 hitNormal = obstacleHit.normal;
            avoidanceForce += hitNormal * force;
        }

        // Avoid other enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, avoidanceRadius);
        foreach (var enemyHit in hitEnemies)
        {
            if (enemyHit.gameObject != this.gameObject && enemyHit.CompareTag("Enemy"))
            {
                Vector2 hitNormal = (Vector2)(transform.position - enemyHit.transform.position).normalized;
                avoidanceForce += hitNormal * force;
            }
        }

        return avoidanceForce.normalized;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyRadius);
    }

    public void TakeDamage(int damage, Transform damageSource)
    {
        currentHealth -= damage;
        StartCoroutine(flash.FlashWhite());
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            pushed?.Knockback(damageSource, force);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sword"))
        {
            TakeDamage(1, collision.transform);
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        isDead = false;
        animator.ResetTrigger("Die");
        animator.Play("Idle");
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        StartCoroutine(ReturnToPoolAfterDeath());
    }

    private IEnumerator ReturnToPoolAfterDeath()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        objectPool.ReturnToPool(gameObject);
    }

}
