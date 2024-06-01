using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float enemyRadius = 1.3f;
    public float speed = 3.0f;
    public float force = 170.0f;
    public float minimumDistToAvoid = 3.55f;
    public float targetReachedRadius = 0.5f;
    public float avoidanceRadius = 1.5f;

    [SerializeField] protected int maxHealth = 3;
    protected int currentHealth;

    protected Vector3 targetPoint;
    protected SpriteRenderer spriteRenderer;
    protected ObjectPool objectPool;
    protected Push pushed;
    protected Flash flash;
    protected Animator animator;
    protected bool isDead = false;
    protected bool isAttacking = false;
    protected Rigidbody2D rb;

    protected virtual void Start()
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

    protected virtual void Update()
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
        float distanceToTarget = Vector3.Distance(targetPoint, transform.position);

        if (distanceToTarget < targetReachedRadius)
        {
            if (!isAttacking)
            {
                Attack();
            }
        }
        else
        {
            if (isAttacking)
            {
                Idle();
            }
            rb.velocity = dir * speed;
        }

        rb.velocity = dir * speed;

        if (player != null)
        {
            spriteRenderer.flipX = player.transform.position.x < transform.position.x;
        }
    }

    protected virtual Vector2 CalculateAvoidanceForce()
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

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, avoidanceRadius);
    }

    public virtual void TakeDamage(int damage, Transform damageSource, bool applyKnockback)
    {
        currentHealth -= damage;
        StartCoroutine(flash.FlashWhite());
        if (currentHealth <= 0)
        {
            Die();
        }
        else if (applyKnockback && pushed != null)
        {
            pushed.Knockback(damageSource, force);
        }
    }


    public virtual void ResetHealth()
    {
        currentHealth = maxHealth;
        isDead = false;
        isAttacking = false;
        animator.ResetTrigger("Die");
        animator.ResetTrigger("Attack");
        animator.Play("Idle");
    }

    protected virtual void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        StartCoroutine(ReturnToPoolAfterDeath());
    }

    protected virtual IEnumerator ReturnToPoolAfterDeath()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        objectPool.ReturnToPool(gameObject);
    }

    protected virtual void Attack()
    {
        rb.velocity = Vector2.zero;
        animator.SetBool("IsAttacking", true);
        isAttacking = true;
    }

    protected virtual void Idle()
    {
        animator.SetBool("IsAttacking", false);
        isAttacking = false;
    }

    public void TriggerTakeHitAnimation()
    {
        animator.SetTrigger("TakeHit");
    }

    public bool GetIsDead()
    {
        return isDead;
    }
}
