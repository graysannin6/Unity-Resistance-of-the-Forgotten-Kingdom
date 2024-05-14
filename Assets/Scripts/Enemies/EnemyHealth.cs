using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private float knockbackthrust = 15f;
    private int currentHealth;
    private KnockBack knockBack;
    private Flash flash;
    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();
    }

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy took damage. Current health: " + currentHealth);
        knockBack.Knockback(Player.Instance.transform, knockbackthrust);
        StartCoroutine(flash.FlashWhite());
        StartCoroutine(CheckDetectDeath());
    }

    private IEnumerator CheckDetectDeath()
    {
        yield return new WaitForSeconds(flash.GetFlashDuration());
        DetectDeath();
    }

    private void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


}
