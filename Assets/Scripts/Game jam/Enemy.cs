using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemyRadius = 1.2f;
    public float speed = 3.0f;
    public float force = 50.0f;
    public float minimumDistToAvoid = 5.0f;
    public float targetReachedRadius = 0.5f;

    private float curSpeed;
    private Vector3 targetPoint;
    private SpriteRenderer spriteRenderer;
    private ObjectPool objectPool;

    void Start()
    {
        targetPoint = Vector3.zero;
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectPool = FindObjectOfType<ObjectPool>();
    }

    void Update()
    {
        GameObject player = GameObject.FindObjectOfType<Player>().gameObject;
        if (player != null)
        {
            targetPoint = player.transform.position;
        }

        Vector3 dir = (targetPoint - transform.position).normalized;

        AvoidObstacles(ref dir);
        AvoidEnemies(ref dir);

        if (Vector3.Distance(targetPoint, transform.position) < targetReachedRadius)
            return;

        curSpeed = speed * Time.deltaTime;

        // Move the enemy towards the target
        transform.position += dir * curSpeed;

        if (player != null)
        {
            if (player.transform.position.x < transform.position.x)
            {
                spriteRenderer.flipX = true; // Flip sprite to face left
            }
            else
            {
                spriteRenderer.flipX = false; // Face right
            }
        }
    }

    public void AvoidObstacles(ref Vector3 dir)
    {
        int layerMask = 1 << 8;

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, enemyRadius, dir, minimumDistToAvoid, layerMask);
        if (hit.collider != null)
        {
            Vector2 hitNormal = hit.normal;
            Vector3 hitNormal3D = new Vector3(hitNormal.x, hitNormal.y, 0);
            dir = (dir + hitNormal3D * force).normalized;
        }
    }

    public void AvoidEnemies(ref Vector3 dir)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, enemyRadius);

        foreach (var hit in hitEnemies)
        {
            if (hit.gameObject != this.gameObject && hit.CompareTag("Enemy"))
            {
                Vector3 hitNormal = transform.position - hit.transform.position;
                dir += hitNormal.normalized * force;
            }
        }

        dir = dir.normalized;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyRadius);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sword"))
        {
            // Return the enemy to the pool instead of destroying it
            objectPool.ReturnToPool(gameObject);
        }
    }
}
