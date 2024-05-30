using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool FacingLeft { get { return facingLeft; } }
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private float dashCD = 0.25f;
    [SerializeField] private TrailRenderer trailRenderer;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool facingLeft = false;
    private bool isDashing = false;
    private GameObject shadow;
    private PlayerInputs playerInputs;
    private float startingMoveSpeed;
    public static PlayerMovement Instance;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        shadow = gameObject.transform.GetChild(1).gameObject;
        playerInputs = GetComponent<PlayerInputs>();
    }

    private void Start()
    {
        playerInputs.GetPlayerControls().Combat.Dash.performed += _ => Dash();
        startingMoveSpeed = moveSpeed;
    }

    public void Move(Vector2 movementInput)
    {
        rb.MovePosition(rb.position + movementInput * (moveSpeed * Time.fixedDeltaTime));
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
        if (!isDashing)
        {
            isDashing = true;
            moveSpeed *= dashSpeed;
            trailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }

    }

    private IEnumerator EndDashRoutine()
    {
        float dashDuration = 0.2f;
        yield return new WaitForSeconds(dashDuration);
        moveSpeed = startingMoveSpeed;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
}
