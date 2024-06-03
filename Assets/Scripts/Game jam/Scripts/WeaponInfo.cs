using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "New Weapon")]
public class WeaponInfo : ScriptableObject
{
    public GameObject weaponPrefab;
    public float weaponCooldown;
    public float weaponDamage;
    public float weaponRange;

    [Header("Initial Values")]
    public float initialWeaponCooldown;
    public float initialWeaponDamage;
    public float initialWeaponRange;

    private void OnEnable()
    {
        weaponCooldown = initialWeaponCooldown;
        weaponDamage = initialWeaponDamage;
        weaponRange = initialWeaponRange;
    }
}
