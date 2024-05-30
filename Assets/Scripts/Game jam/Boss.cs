using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : BaseEnemy
{
    public delegate void BossDeathDelegate();
    public event BossDeathDelegate OnBossDeath;

    protected override void Start()
    {
        base.Start();
        maxHealth = 10;
        currentHealth = maxHealth;
    }

    protected override void Die()
    {
        base.Die();
        OnBossDeath?.Invoke();
    }

    protected override IEnumerator ReturnToPoolAfterDeath()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
