using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public bool GettingKnockedBack { get; private set; }
    [SerializeField] private float knockbackDuration = 0.2f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Knockback(Transform damageSource, float force)
    {
        GettingKnockedBack = true;
        Vector2 direction = (transform.position - damageSource.position).normalized * force * rb.mass;
        rb.AddForce(direction, ForceMode2D.Impulse);
        StartCoroutine(KnockbackTimer());
    }

    private IEnumerator KnockbackTimer()
    {
        yield return new WaitForSeconds(knockbackDuration);
        rb.velocity = Vector2.zero;
        GettingKnockedBack = false;
    }
}
