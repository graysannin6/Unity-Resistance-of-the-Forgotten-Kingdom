using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public bool FacingLeft { get { return facingLeft; } }
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private float dashCD = 3f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private TrailRenderer trailRenderer;
    public Image frontDashBar;
    public Image backDashBar;


    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool facingLeft = false;
    private bool isDashing = false;
    private float dashTimer;
    private GameObject shadow;
    private PlayerInputs playerInputs;
    private float startingMoveSpeed;
    public static PlayerMovement Instance;
    private Collider2D playerCollider;
    private KnockBack knockBack;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        shadow = gameObject.transform.GetChild(1).gameObject;
        playerInputs = GetComponent<PlayerInputs>();
        playerCollider = GetComponent<Collider2D>();
        knockBack = GetComponent<KnockBack>();
    }

    private void Start()
    {
        playerInputs.GetPlayerControls().Combat.Dash.performed += _ => Dash();
        startingMoveSpeed = moveSpeed;
        dashTimer = dashCD;
        ActiveInventory.Instance.EquipStartingWeapon();
        UpdateDashUI();
    }

    private void Update()
    {
        if (dashTimer < dashCD)
        {
            dashTimer += Time.deltaTime;
            UpdateDashUI();
        }
    }

    public void Move(Vector2 movementInput)
    {
        if (knockBack.GettingKnockedBack || PlayerHealthManager.Instance.IsDead)
        {
            return;
        }

        rb.MovePosition(rb.position + movementInput * (moveSpeed * Time.fixedDeltaTime));
        Debug.Log(moveSpeed);

    }

    public void AdjustPlayerFacingDirection(Vector2 movementInput)
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePosition.x < playerPosition.x)
        {
            spriteRenderer.flipX = true;
            shadow.transform.localPosition = new Vector3(-0.1f, 0f, 0);
            facingLeft = true;
        }
        else
        {
            spriteRenderer.flipX = false;
            shadow.transform.localPosition = new Vector3(0.1f, 0f, 0f);
            facingLeft = false;
        }
    }

    private void Dash()
    {
        if (!isDashing && dashTimer >= dashCD)
        {
            isDashing = true;
            startingMoveSpeed = moveSpeed;
            moveSpeed *= dashSpeed;
            trailRenderer.emitting = true;
            playerCollider.enabled = false;
            dashTimer = 0f;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        yield return new WaitForSeconds(dashDuration);
        moveSpeed = startingMoveSpeed;
        trailRenderer.emitting = false;
        playerCollider.enabled = true;
        yield return new WaitForSeconds(dashCD - dashDuration);
        isDashing = false;
    }

    private void UpdateDashUI()
    {
        float fillAmount = Mathf.Clamp01(dashTimer / dashCD);
        frontDashBar.fillAmount = fillAmount;
        backDashBar.fillAmount = fillAmount;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public void SetMoveSpeed(float newMoveSpeed)
    {
        moveSpeed = newMoveSpeed;
    }
}
