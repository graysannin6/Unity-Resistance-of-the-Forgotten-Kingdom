using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleEffect;
    [SerializeField] private bool isEnemyProjectile = false;


    [SerializeField] private float projectileRange = 10f;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    public void UpdateProjectileRange(float projectileRange)
    {
        this.projectileRange = projectileRange;
    }

    public void UpdateMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BaseEnemy enemy = collision.gameObject.GetComponent<BaseEnemy>();
        PlayerHealthManager player = collision.gameObject.GetComponent<PlayerHealthManager>();

        if (player && isEnemyProjectile)
        {
            player.TakeDamage(1, transform);
            Destroy(gameObject);
        }

        if (enemy != null && !isEnemyProjectile)
        {

            enemy.TriggerTakeHitAnimation();
            Instantiate(particleEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void DetectFireDistance()
    {
        if (Vector3.Distance(startPos, transform.position) > projectileRange)
        {
            Destroy(gameObject);
        }
    }

}
