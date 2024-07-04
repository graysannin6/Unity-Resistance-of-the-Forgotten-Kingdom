using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : BaseEnemy
{
    public delegate void BossDeathDelegate();
    public event BossDeathDelegate OnBossDeath;

    private Vector3 designatedPosition = new Vector3(4.54f, -21f, 0f);
    private bool reachedDesignatedPosition = false;

    private Shooter shooter;

    protected override void Start()
    {
        base.Start();
        shooter = GetComponent<Shooter>();
        maxHealth = 1;
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
        UpdateBurstPattern();
        shooter.Attack();
        animator.SetTrigger("Fire");
    }

    private void UpdateBurstPattern()
    {
        float healthFraction = (float)currentHealth / maxHealth;
        if (healthFraction > 0.66f)
        {
            shooter.SetBurstPattern(1);
        }
        else if (healthFraction > 0.33f)
        {
            shooter.SetBurstPattern(2);
        }
        else
        {
            shooter.SetBurstPattern(3);
        }
    }

    protected override void Die()
    {
        base.Die();
        GetComponent<PickUpSpawner>().DropPickUp(true);
        OnBossDeath?.Invoke();
        AudioManager.instance.StopBossOneMusic();
        AudioManager.instance.PlayMainMusic();
        UpgradePanelController.instance.AddEnemiesKilled(1);
    }

    protected override IEnumerator ReturnToPoolAfterDeath()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        objectPool.ReturnToPool(gameObject);
    }
}
