using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [Header("Sword Attributes")]
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private PolygonCollider2D weaponCollider;

    [Header("Attack Settings")]
    [SerializeField, Tooltip("Cooldown time between attacks in seconds.")]
    private float SwordAttackCD = 0.5f;
    [SerializeField] private WeaponInfo weaponInfo;




    private Animator animator;
    private GameObject slashAnim;

    private bool canAttack = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        MouseFollowWithOffset();

    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        transform.root.tag = "Sword";
        weaponCollider.enabled = true;
        canAttack = false;
        //slashAnim = Instantiate(slashAnimPrefab, attackPoint.position, Quaternion.identity);
        //slashAnim.transform.parent = this.transform.parent;
    }


    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(Player.Instance.transform.position);
        Vector3 toMouse = mousePos - playerScreenPoint;

        float angle = Mathf.Atan2(toMouse.y, toMouse.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            angle = Mathf.Atan2(toMouse.y, -toMouse.x) * Mathf.Rad2Deg;
            float clampedAngle = Mathf.Clamp(angle, -25, 25);
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, clampedAngle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            float clampedAngle = Mathf.Clamp(angle, -25, 25);
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, clampedAngle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -0, 0);
        }
    }





    public void OnAnimationComplete()
    {
        canAttack = true;
    }

    public void DoneAttackingAnimEvent()
    {
        weaponCollider.enabled = false;
        transform.root.tag = "Untagged";
    }

    public void SwingUpFlipAnimEvent()
    {

        //slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        /*if (playerMovement.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }*/
    }

    public void SwingDownFlipAnimEvent()
    {

        //slashAnim.transform.rotation = Quaternion.Euler(0, 0, 0);

        /*if (playerMovement.FacingLeft)
        {
            SpriteRenderer spriteRenderer = slashAnim.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = true;
            }
        }*/
    }

}

