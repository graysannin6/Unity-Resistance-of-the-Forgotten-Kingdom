using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    private PlayerControls playerControls;
    private Vector2 movementInput;

    private PlayerAnimations playerAnimations;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerAnimations = GetComponent<PlayerAnimations>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void Playerinput()
    {
        movementInput = playerControls.Movement.Move.ReadValue<Vector2>();
        playerAnimations.GetAnimator().SetFloat("moveX", movementInput.x);
        playerAnimations.GetAnimator().SetFloat("moveY", movementInput.y);
    }
    public Vector2 GetMovementInput()
    {
        return movementInput;
    }
}
