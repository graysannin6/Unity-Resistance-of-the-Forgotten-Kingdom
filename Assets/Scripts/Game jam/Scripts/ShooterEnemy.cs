using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : BaseEnemy
{
    public float shootingRange = 5.0f;
    public float shootInterval = 1.5f;
    public GameObject projectilePrefab;

    private float lastShotTime;


    protected override void Start()
    {
        base.Start();
        lastShotTime = 0;
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
            rb.velocity = Vector2.zero;
        }
        else if (distanceToTarget <= shootingRange)
        {
            rb.velocity = Vector2.zero;
            ShootAtPlayer(player);
        }
        else
        {
            rb.velocity = dir * speed;
        }

        if (player != null)
        {
            spriteRenderer.flipX = player.transform.position.x < transform.position.x;
        }
    }

    private void ShootAtPlayer(GameObject player)
    {
        if (Time.time - lastShotTime > shootInterval)
        {
            Vector2 targetDirection = (player.transform.position - transform.position).normalized;
            GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            newProjectile.transform.right = targetDirection;

            lastShotTime = Time.time;
        }
    }

    public override void ResetHealth()
    {
        base.ResetHealth();
        lastShotTime = 0;
    }

}
