using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleEffect;
    private WeaponInfo weaponInfo;
    private Vector3 startPos;
    private Vector2 direction;
    private Rigidbody2D rb;
    private GameObject player;

    private Collider2D fireballCollider;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        fireballCollider = GetComponent<Collider2D>();
        player = FindAnyObjectByType<Player>().gameObject;

        if (player != null)
        {
            Collider2D playerCollider = player.GetComponent<Collider2D>();
            if (playerCollider != null)
            {
                Physics2D.IgnoreCollision(fireballCollider, playerCollider);
            }
        }
    }

    private void Start()
    {
        startPos = transform.position;
        LaserFaceMouse();
    }

    private void FixedUpdate()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    public void UpdateWeaponInfo(WeaponInfo weaponInfo)
    {
        this.weaponInfo = weaponInfo;
    }

    private void MoveProjectile()
    {
        rb.velocity = direction * moveSpeed;
    }

    private void DetectFireDistance()
    {
        if (Vector3.Distance(startPos, transform.position) > weaponInfo.weaponRange)
        {
            Destroy(gameObject);
        }
    }

    private void LaserFaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        direction = (mousePosition - transform.position).normalized;
        transform.right = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BaseEnemy enemy = collision.gameObject.GetComponent<BaseEnemy>();
        if (enemy != null)
        {
            enemy.TriggerTakeHitAnimation();
            InstantiateParticleEffect(enemy.transform.position);
            enemy?.TakeDamage(weaponInfo.weaponDamage, transform, false);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Walls"))
        {
            Destroy(gameObject);
        }
    }

    private void InstantiateParticleEffect(Vector3 position)
    {
        if (particleEffect != null)
        {
            GameObject effect = Instantiate(particleEffect, position, Quaternion.identity);
            Destroy(effect, 1f);
        }
    }

}
