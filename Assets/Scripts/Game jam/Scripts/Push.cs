using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    public bool GettingKnockedBack { get; private set; }
    [SerializeField] private float knockbackDuration = 0.2f;
    [SerializeField] private float knockbackSpeed = 10f;

    private Vector3 knockbackDirection;
    private float knockbackEndTime;

    private void Update()
    {
        if (GettingKnockedBack)
        {
            transform.position += knockbackDirection * knockbackSpeed * Time.deltaTime;

            if (Time.time >= knockbackEndTime)
            {
                GettingKnockedBack = false;
            }
        }
    }

    public void Knockback(Transform damageSource, float force)
    {
        GettingKnockedBack = true;
        knockbackDirection = (transform.position - damageSource.position).normalized * force;
        knockbackEndTime = Time.time + knockbackDuration;
    }
}
