using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : BaseEnemy
{
    public delegate void BossDeathDelegate();
    public event BossDeathDelegate OnBossDeath;

    private Vector3 designatedPosition = new Vector3(4.54f, -21f, 0f);
    private bool reachedDesignatedPosition = false;

    private IEnemy enemy;

    protected override void Start()
    {
        enemy = GetComponent<IEnemy>();
        base.Start();
        maxHealth = 10;
        currentHealth = maxHealth;
    }

    protected override void Update()
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

        if (!reachedDesignatedPosition)
        {
            MoveToDesignatedPosition();
        }
        else
        {
            PerformDesignatedActions();
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = designatedPosition.x < transform.position.x;
        }
    }

    private void MoveToDesignatedPosition()
    {
        Vector3 dir = (designatedPosition - transform.position).normalized;

        Vector2 avoidanceForce = CalculateAvoidanceForce();
        dir += (Vector3)avoidanceForce;
        dir = dir.normalized;

        float distanceToTarget = Vector3.Distance(designatedPosition, transform.position);

        if (distanceToTarget < targetReachedRadius)
        {
            rb.velocity = Vector2.zero;
            reachedDesignatedPosition = true;
        }
        else
        {
            rb.velocity = dir * speed;
        }
    }

    private void PerformDesignatedActions()
    {
        enemy.Attack();
    }

    protected override void Die()
    {
        base.Die();
        OnBossDeath?.Invoke();
    }

    protected override IEnumerator ReturnToPoolAfterDeath()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        objectPool.ReturnToPool(gameObject);
    }
}
