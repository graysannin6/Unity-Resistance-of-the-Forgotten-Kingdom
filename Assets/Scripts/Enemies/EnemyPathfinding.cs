using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Push knockBack;
    private EnemyHealth enemyHealth;

    private void Awake()
    {
        knockBack = GetComponent<Push>();
        rb = GetComponent<Rigidbody2D>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void FixedUpdate()
    {
        if (knockBack.GettingKnockedBack || enemyHealth.isDead)
        {
            return;
        }
        rb.MovePosition(rb.position + moveDirection * (speed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - rb.position).normalized;
        moveDirection = direction;
    }

    public void StopMovement()
    {
        moveDirection = Vector2.zero;
    }

}
