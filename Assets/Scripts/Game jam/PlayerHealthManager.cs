using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] private int maxHealth = 8;
    [SerializeField] private float knockbackAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;


    private int currentHealth;
    private bool canTakeDamage = true;

    private KnockBack knockBack;
    private Flash flash;

    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        flash = GetComponent<Flash>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        ShooterEnemy shooterEnemy = collision.gameObject.GetComponent<ShooterEnemy>();
        Boss boss = collision.gameObject.GetComponent<Boss>();
        if (enemy || shooterEnemy || boss)
        {
            TakeDamage(1, collision.transform);

        }
    }

    public void TakeDamage(int damage, Transform hitTransform)
    {
        if (!canTakeDamage)
        {
            return;
        }
        knockBack.Knockback(hitTransform, knockbackAmount);
        StartCoroutine(flash.FlashWhite());
        canTakeDamage = false;
        currentHealth -= damage;
        StartCoroutine(InvincibilityFrames());
    }

    private IEnumerator InvincibilityFrames()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

}
