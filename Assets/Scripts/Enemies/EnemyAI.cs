using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float stopChaseDistance = 1.5f;
    private Transform playerTransform;
    [SerializeField] private float attackCooldown = 2f;

    private bool canAttack = true;
    private IEnemy enemy;

    public float enemyRadius = 1.3f;
    public float force = 170.0f;
    public float minimumDistToAvoid = 3.55f;
    public float avoidanceRadius = 1.5f;

    private Rigidbody2D rb;

    public enum State
    {
        Chase,
        Attack,
    }

    public State state;
    private EnemyPathfinding pathfinding;

    private void Awake()
    {
        pathfinding = GetComponent<EnemyPathfinding>();
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
        enemy = GetComponent<IEnemy>();
        rb = GetComponent<Rigidbody2D>();
        state = State.Chase;
    }

    private void Update()
    {
        if (GetComponent<EnemyHealth>().isDead) return;
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        switch (state)
        {
            case State.Chase:
                HandleChaseState(distanceToPlayer);
                Debug.Log("Chase");
                break;
            case State.Attack:
                HandleAttackState(distanceToPlayer);
                Debug.Log("Attack");
                break;
        }
    }
    public void ResetState()
    {
        state = State.Chase;
        canAttack = true;
    }

    private void HandleChaseState(float distanceToPlayer)
    {
        if (distanceToPlayer > stopChaseDistance)
        {
            Vector2 avoidanceForce = CalculateAvoidanceForce();
            Vector2 targetPosition = playerTransform.position;
            Vector2 direction = (targetPosition - (Vector2)transform.position + avoidanceForce).normalized;
            pathfinding.MoveTo((Vector2)playerTransform.position + direction);
        }
        else
        {
            pathfinding.StopMovement();
            state = State.Attack;
        }
    }

    private void HandleAttackState(float distanceToPlayer)
    {
        if (distanceToPlayer > stopChaseDistance)
        {
            state = State.Chase;
        }
        else
        {
            pathfinding.StopMovement();
            if (canAttack)
            {
                canAttack = false;
                enemy.Attack();
                StartCoroutine(AttackCooldown());
            }
        }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private Vector2 CalculateAvoidanceForce()
    {
        Vector2 avoidanceForce = Vector2.zero;

        int layerMask = 1 << 8;
        RaycastHit2D obstacleHit = Physics2D.CircleCast(transform.position, enemyRadius, rb.velocity, minimumDistToAvoid, layerMask);
        if (obstacleHit.collider != null)
        {
            Vector2 hitNormal = obstacleHit.normal;
            avoidanceForce += hitNormal * force;
        }

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

    public void StopMovement()
    {
        pathfinding.StopMovement();
    }

}
