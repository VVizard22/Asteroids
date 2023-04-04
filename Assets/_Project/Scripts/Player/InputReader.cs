using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [SerializeField]
    private InputDataHolderSO inputDataHolderSO;
    private GameControls gameControl;

    private void Awake()
    {
        gameControl = new GameControls();
    }

    private void Start()
    {
        gameControl.Player.HorizontalMovement.performed += HorizontalMovement_performed;
        gameControl.Player.HorizontalMovement.canceled += HorizontalMovement_performed;

        gameControl.Player.Impulse.performed += PlayerImpulse_performed;
        gameControl.Player.Impulse.canceled += PlayerImpulse_canceled;

        gameControl.Player.Shoot.performed += Shoot_performed;
        gameControl.Player.Shoot.canceled += Shoot_Finished;
        
        gameControl.Enable();
    }

    private void Shoot_Finished(InputAction.CallbackContext obj)
    {
        inputDataHolderSO.CancelShoot();
    }

    private void Shoot_performed(InputAction.CallbackContext context)
    {
        inputDataHolderSO.StartShoot();
    }

    private void PlayerImpulse_performed(InputAction.CallbackContext context)
    {
        inputDataHolderSO.UpdateData(true);
    }

    private void PlayerImpulse_canceled(InputAction.CallbackContext context)
    {
        inputDataHolderSO.UpdateData(false);
    }

    private void HorizontalMovement_performed(InputAction.CallbackContext context)
    {
        float horizontalMovementValue = context.ReadValue<float>();

        inputDataHolderSO.UpdateData(horizontalMovementValue);
    }


}
