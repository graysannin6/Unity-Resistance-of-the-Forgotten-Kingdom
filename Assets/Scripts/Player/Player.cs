using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    private PlayerMovement playerMovement;
    private PlayerInputs playerInputs;

    private void Awake()
    {
        Instance = this;
        playerMovement = GetComponent<PlayerMovement>();
        playerInputs = GetComponent<PlayerInputs>();
    }

    private void Update()
    {
        playerInputs.Playerinput();
    }

    private void FixedUpdate()
    {
        playerMovement.AdjustPlayerFacingDirection(playerInputs.GetMovementInput());
        playerMovement.Move(playerInputs.GetMovementInput());
    }

}
