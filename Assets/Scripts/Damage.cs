using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private int damage;

    private void Start()
    {
        MonoBehaviour currentActiveWeapon = ActiveWeapon.Instance.CurrentActiveWeapon;
        damage = (currentActiveWeapon as IWeapon).GetWeaponInfo().weaponDamage;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        BaseEnemy enemy = other.gameObject.GetComponent<BaseEnemy>();
        enemy?.TakeDamage(damage, transform, true);
    }
}
