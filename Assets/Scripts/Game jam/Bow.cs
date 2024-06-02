using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private GameObject arrow;

    public float delay;
    private float timer = 0f;
    private bool isCountingDown = false;

    private Animator animator;
    readonly int fireHash = Animator.StringToHash("Fire");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        delay = weaponInfo.weaponCooldown;
    }

    void Update()
    {
        if (isCountingDown)
        {
            timer += Time.deltaTime;
            if (timer >= delay)
            {
                Reactivate();
            }
        }
    }

    public void Attack()
    {
        animator.SetTrigger(fireHash);
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
        newArrow.GetComponent<Projectile>().UpdateProjectileRange(weaponInfo.weaponRange);
        StartDeactivationProcess();
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    public void StartDeactivationProcess()
    {
        DeactivateAndStartTimer();
    }

    private void DeactivateAndStartTimer()
    {
        arrow.SetActive(false);
        timer = 0f;
        isCountingDown = true;
    }

    private void Reactivate()
    {
        arrow.SetActive(true);
        isCountingDown = false;
    }
}
