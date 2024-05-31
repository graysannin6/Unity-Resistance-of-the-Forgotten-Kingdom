using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{

    public MonoBehaviour CurrentActiveWeapon { get; private set; }

    private PlayerControls playerControls;
    private float timeBetweenAttacks;

    private bool attackButtonPressed, isAttacking = false;

    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();

    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();

        AttackCooldown();

    }

    private void Update()
    {
        Attack();
    }

    public void Newweapon(MonoBehaviour weapon)
    {
        CurrentActiveWeapon = weapon;
        AttackCooldown();
        timeBetweenAttacks = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;

        //isAttacking = false;
        //attackButtonPressed = false;
    }

    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
        //isAttacking = false;
        //attackButtonPressed = false;
    }

    private void AttackCooldown()
    {
        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksRoutine());
    }

    private IEnumerator TimeBetweenAttacksRoutine()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
    }

    private void StartAttacking()
    {
        attackButtonPressed = true;
    }

    private void StopAttacking()
    {
        attackButtonPressed = false;
    }

    private void Attack()
    {
        if (!attackButtonPressed || isAttacking || CurrentActiveWeapon == null) return;

        AttackCooldown();

        (CurrentActiveWeapon as IWeapon).Attack();
    }

}
