using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{

    [SerializeField] private GameObject projectilePrefab;
    [Tooltip("The speed at which the projectile moves")]
    [SerializeField] private float projectileMoveSpeed;
    [Tooltip("The number of bursts to shoot")]
    [SerializeField] private float burstCount;
    [Tooltip("The number of projectiles to shoot per burst")]
    [SerializeField] private int projectilesPerBurst;
    [Tooltip("The angle spread of the projectiles")]
    [SerializeField][Range(0, 359)] private float angleSpread;
    [Tooltip("The distance from the shooter to spawn the projectile")]
    [SerializeField] private float startingDistance = 0.1f;
    [Tooltip("The time between bursts")]
    [SerializeField] private float timeBetweenBursts;
    [Tooltip("The time to rest after shooting")]
    [SerializeField] private float restTime = 1f;

    private bool isShooting = false;

    private void OnValidate()
    {
        if (projectilesPerBurst < 1) { projectilesPerBurst = 1; }
        if (burstCount < 1) { burstCount = 1; }
        if (timeBetweenBursts < 0.1f) { timeBetweenBursts = 0.1f; }
        if (restTime < 0.1f) { restTime = 0.1f; }
        if (startingDistance < 0.1f) { startingDistance = 0.1f; }
        if (angleSpread == 0) { angleSpread = 1f; }
        if (projectileMoveSpeed <= 0f) { projectileMoveSpeed = 0.1f; }
    }

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
        float startAngle, currentAngle, angleStep;
        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep);

        for (int i = 0; i < burstCount; i++)
        {

            for (int j = 0; j < projectilesPerBurst; j++)
            {
                Vector2 pos = FindBulletSpawnPosition(currentAngle);

                GameObject newProjectile = Instantiate(projectilePrefab, pos, Quaternion.identity);
                newProjectile.transform.right = newProjectile.transform.position - transform.position;

                if (newProjectile.TryGetComponent(out Projectile projectile))
                {
                    projectile.UpdateMoveSpeed(projectileMoveSpeed);
                }

                currentAngle += angleStep;
            }
            currentAngle = startAngle;
            yield return new WaitForSeconds(timeBetweenBursts);
            TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep);
        }

        yield return new WaitForSeconds(restTime);
        isShooting = false;
    }

    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep)
    {
        Vector2 targetDirection = Player.Instance.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        float endAngle = targetAngle;
        currentAngle = startAngle;
        float halfSpreadAngle = 0f;
        angleStep = 0f;
        if (angleSpread != 0)
        {
            angleStep = angleSpread / (projectilesPerBurst - 1);
            halfSpreadAngle = angleSpread / 2;
            startAngle = targetAngle - halfSpreadAngle;
            endAngle = targetAngle + halfSpreadAngle;
            currentAngle = startAngle;
        }
    }

    private Vector2 FindBulletSpawnPosition(float currentAngle)
    {
        float x = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        Vector2 position = new Vector2(x, y);

        return position;
    }

    public void SetBurstPattern(int pattern)
    {
        switch (pattern)
        {
            case 1:
                // Easy pattern
                projectileMoveSpeed = 5f;
                burstCount = 2;
                projectilesPerBurst = 5;
                angleSpread = 30f;
                timeBetweenBursts = 1f;
                restTime = 2f;
                startingDistance = 3f;
                break;
            case 2:
                // Medium pattern
                projectileMoveSpeed = 7f;
                burstCount = 3;
                projectilesPerBurst = 7;
                angleSpread = 60f;
                timeBetweenBursts = 0.75f;
                restTime = 1.5f;
                startingDistance = 3f;
                break;
            case 3:
                // Hard pattern
                projectileMoveSpeed = 10f;
                burstCount = 5;
                projectilesPerBurst = 10;
                angleSpread = 90f;
                timeBetweenBursts = 0.5f;
                restTime = 1f;
                startingDistance = 3f;
                break;
        }
    }
}
