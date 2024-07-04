using System;
using System.Collections;
using UnityEngine;

public class HorsemanFSM : FiniteStateMachine
{
    public enum FSMState
    {
        None,
        Chase,
        Attack,
        Teleport,
        Charge,
        Dead,
    }

    public FSMState curState = FSMState.Chase;
    private float curSpeed = 5f;
    private float curRotSpeed = 200f;
    private int health = 100;
    private bool bDead = false;

    private Transform playerTransform;
    private Vector3 destPos;

    private float elapsedTime = 0f;
    private float teleportCooldown = 5f;
    private float chargeCooldown = 10f;

    private float teleportDistance = 5f;
    private float chargeSpeedMultiplier = 3f;
    private float chargeDuration = 2f;

    private Rigidbody2D rb;

    protected override void Initialize()
    {
        GameObject objPlayer = FindObjectOfType<Player>().gameObject;
        playerTransform = objPlayer.transform;

        rb = GetComponent<Rigidbody2D>();

        if (!playerTransform)
        {
            Debug.Log("Houston we have a problem!!");
        }
    }

    protected override void FSMUpdate()
    {
        switch (curState)
        {
            case FSMState.Chase:
                UpdateChaseState();
                break;
            case FSMState.Attack:
                UpdateAttackState();
                break;
            case FSMState.Teleport:
                UpdateTeleportState();
                break;
            case FSMState.Charge:
                UpdateChargeState();
                break;
            case FSMState.Dead:
                UpdateDeadState();
                break;
        }
        elapsedTime += Time.deltaTime;

        if (health < 0)
        {
            curState = FSMState.Dead;
        }
    }
    private void UpdateChaseState()
    {
        destPos = playerTransform.position;
        float dist = Vector3.Distance(transform.position, playerTransform.position);
        //

        MoveTowards(destPos);
    }

    private void UpdateAttackState()
    {
        throw new NotImplementedException();
    }

    private void UpdateTeleportState()
    {
        throw new NotImplementedException();
    }

    private void UpdateChargeState()
    {
        StartCoroutine(ChargeCourutine());
    }

    private IEnumerator ChargeCourutine()
    {
        float originalSpeed = curSpeed;
        curSpeed *= chargeSpeedMultiplier;

        yield return new WaitForSeconds(chargeDuration);

        curSpeed = originalSpeed;
        curState = FSMState.Chase;
    }

    private void UpdateDeadState()
    {
        throw new NotImplementedException();
    }

    private void MoveTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        rb.velocity = direction * curSpeed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

}
