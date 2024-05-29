using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public delegate void BossDeathDelegate();
    public event BossDeathDelegate OnBossDeath;

    private int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (OnBossDeath != null)
        {
            OnBossDeath.Invoke();
        }
        Destroy(gameObject);
    }
}
