using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileMoveSpeed;
    [SerializeField] private float burstCount;
    [SerializeField] private float timeBetweenBursts;
    [SerializeField] private float restTime = 1f;

    private bool isShooting = false;

    public void Attack()
    {

        if (!isShooting)
        {
            StartCoroutine(ShootBurst());
        }

    }

    private IEnumerator ShootBurst()
    {
        isShooting = true;

        for (int i = 0; i < burstCount; i++)
        {
            Vector2 targetDirection = Player.Instance.transform.position - transform.position;

            GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            newProjectile.transform.right = targetDirection;

            if (newProjectile.TryGetComponent(out Projectile projectile))
            {
                projectile.UpdateMoveSpeed(projectileMoveSpeed);
            }

            yield return new WaitForSeconds(timeBetweenBursts);
        }

        yield return new WaitForSeconds(restTime);
        isShooting = false;
    }
}
