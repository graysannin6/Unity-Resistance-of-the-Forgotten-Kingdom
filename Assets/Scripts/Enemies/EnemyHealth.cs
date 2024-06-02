using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 3;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private float knockbackthrust = 15f;
    protected int currentHealth;
    protected Vector3 targetPoint;
    protected SpriteRenderer spriteRenderer;
    protected ObjectPool objectPool;
    protected Push pushed;
    protected Flash flash;
    protected Animator animator;
    public bool isDead = false;
    protected bool isAttacking = false;
    protected Rigidbody2D rb;
    private EnemyAI enemyAI;

    private void Awake()
    {
        targetPoint = Vector3.zero;
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectPool = FindObjectOfType<ObjectPool>();
        pushed = GetComponent<Push>();
        currentHealth = maxHealth;
        flash = GetComponent<Flash>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyAI = GetComponent<EnemyAI>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
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
            pushed.Knockback(damageSource, knockbackthrust);
        }
    }

    public virtual void ResetHealth()
    {
        currentHealth = maxHealth;
        isDead = false;
        enemyAI.state = EnemyAI.State.Chase;
        isAttacking = false;
        animator.ResetTrigger("Die");
        animator.Play("Idle");
        enemyAI.ResetState();
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
        enemyAI.StopAllCoroutines(); // Stop AI coroutines
        enemyAI.StopMovement(); // Stop movement
        objectPool.ReturnToPool(gameObject);
    }


}
