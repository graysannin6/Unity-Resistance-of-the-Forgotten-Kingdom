using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform attackPoint;
    private PlayerControls playerControls;
    private Animator animator;
    private bool canAttack = true;
    private Player player;

    private PlayerMovement playerMovement;
    private ActiveWeapon activeWeapon;

    private GameObject slashAnim;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        animator = GetComponent<Animator>();
        playerControls = new PlayerControls();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Combat.Attack.started += _ => HandleAttackInput();
    }

    private void OnDisable()
    {
        playerControls.Disable();
        playerControls.Combat.Attack.started -= _ => HandleAttackInput();
    }

    private void HandleAttackInput()
    {
        if (canAttack)
        {
            Attack();
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        canAttack = false;
        slashAnim = Instantiate(slashAnimPrefab, attackPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
    }


    public void OnAnimationComplete()
    {
        canAttack = true;
    }


    void Update()
    {
        MouseFollowWithOffset();
    }

    public void SwingUpFlipAnim()
    {

        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (playerMovement.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnim()
    {
        if (slashAnim != null)
        {
            slashAnim.transform.rotation = Quaternion.Euler(0, 0, 0);

            if (playerMovement.FacingLeft)
            {
                SpriteRenderer spriteRenderer = slashAnim.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.flipX = true;
                }
            }
        }
        else
        {
            Debug.LogWarning("Attempted to access a destroyed or uninitialized slash animation GameObject.");
        }
    }


    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(player.transform.position);
        Vector3 toMouse = mousePos - playerScreenPoint;

        float angle = Mathf.Atan2(toMouse.y, toMouse.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            angle = Mathf.Atan2(toMouse.y, -toMouse.x) * Mathf.Rad2Deg;
            float clampedAngle = Mathf.Clamp(angle, -25, 25);
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, clampedAngle);
        }
        else
        {
            float clampedAngle = Mathf.Clamp(angle, -25, 25);
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, clampedAngle);
        }
    }



}

