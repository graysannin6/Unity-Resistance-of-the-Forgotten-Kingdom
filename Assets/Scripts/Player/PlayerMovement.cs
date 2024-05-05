using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }
    [SerializeField] private float moveSpeed = 1f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool facingLeft = false;
    [SerializeField] private GameObject shadow;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        shadow = gameObject.transform.GetChild(1).gameObject;
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
            FacingLeft = true;
        }
        else
        {
            spriteRenderer.flipX = false;
            shadow.transform.localPosition = new Vector3(0.1f, 0f, 0f);
            FacingLeft = false;
        }
    }
}
